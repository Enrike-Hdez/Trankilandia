using Microsoft.Win32;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;
using SharpCompress.Common.Zip;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Trankilandia
{
    public partial class MainWindow : Window
    {

        // Variables
        private string format = ".cbz";
        private int position = 0;
        private int totalPages = 0;
        private ZipArchive archive;
        private string filePath;
        private Stream stream;
        List <ZipArchiveEntry> sortedImagesZip = new List<ZipArchiveEntry>();
        List<IArchiveEntry> sortedImagesRar = new List<IArchiveEntry>();


        // Método main
        public MainWindow()
        {
            InitializeComponent();
        }



        //Métodos privados

        //Capturar el formato del archivo
        public string GetFileFormat(string filePath)
        {
            // Obtener la extensión del archivo
            string format = System.IO.Path.GetExtension(filePath);
            // Devolver la extensión sin el punto inicial
            return format.TrimStart('.');
        }

        //Cargar el BitMap para cargar la página actual
        private void LoadInDisplay(Stream stream)
        {     
            using (MemoryStream memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                memoryStream.Position = 0;

                // Creando un BitmapImage a partir del MemoryStream
                BitmapImage bitmap = new BitmapImage();

                // Configurando el BitmapImage para cargar la imagen desde el MemoryStream
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = memoryStream;
                bitmap.EndInit();
                bitmap.Freeze();

                // Generando la imagen en la interfaz
                ComicImage.Source = bitmap;
            }
        }

        private void DisplayCurrentPage()
        {
            Stream pageStream;

            if (format == ".cbz")
                pageStream = sortedImagesZip[position].Open();
            else if (format == ".cbr")
                pageStream = sortedImagesRar[position].OpenEntryStream();
            else
                throw new InvalidOperationException($"Formato no compatible: {format}");

            using (pageStream)
            {
                LoadInDisplay(pageStream);
            }
        }

        // Botones
        // Botón para abrir un archivo .cbz
        private void Button_OpenArchive(object sender, RoutedEventArgs e)
        {
            // Limpiar la posición y la lista de imágenes
            position = 0;

            // Abrir cuadro de diálogo para seleccionar un archivo .cbz
            OpenFileDialog openfile = new OpenFileDialog();

            

            // Si el archivo es .cbz
            if (openfile.ShowDialog() == true) 
            {
                // Capturando el formato
                format = System.IO.Path.GetExtension(openfile.FileName).ToLower();

                if (format.ToLower() == ".cbz")
                {
                    List<ZipArchiveEntry> images = new List<ZipArchiveEntry>();

                    // Guardando la ruta del archivo
                    filePath = openfile.FileName;

                    // Preparando el archivo .cbz para leerlo
                    archive = ZipFile.OpenRead(filePath);



                    //Selección de imágenes
                    foreach (var zipEntry in archive.Entries)
                    {
                        //Extensiones permitidas
                        string extension = System.IO.Path.GetExtension(zipEntry.FullName).ToLower();
                        if (extension != ".jpg" &&
                            extension != ".jpeg" &&
                            extension != ".png" &&
                            extension != ".webp")
                        {
                            continue;
                        }

                        images.Add(zipEntry);

                        // Si no hay imagenes en el archivo de comics
                        if (images.Count == 0)
                        {
                            MessageBox.Show("No se encontraron imágenes en el archivo .cbz", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    totalPages = images.Count - 1;

                    // Ordenando la lista de imágenes por su nombre
                    sortedImagesZip = images.OrderBy(entry => entry.FullName).ToList();

                    // Preparando la página actual
                    var actualPageZip = sortedImagesZip[position];

                    // Creando un stream a partir de los datos de la imagen seleccionada
                    stream = actualPageZip.Open();

                    LoadInDisplay(stream);
                }


                // Si el archivo es .cbr
                else if (format.ToLower() == ".cbr")
                {
                    List<IArchiveEntry> images = new List<IArchiveEntry>();

                    // Guardando la ruta del archivo
                    filePath = openfile.FileName;

                    // Preparando el archivo .cbz para leerlo
                    var archive = RarArchive.OpenArchive(filePath); ;


                    //Selección de imágenes
                    foreach (var rarEntry in archive.Entries)
                    {
                        //Extensiones permitidas
                        string extension = System.IO.Path.GetExtension(rarEntry.Key.ToString()).ToLower();
                        if (extension != ".jpg" &&
                            extension != ".jpeg" &&
                            extension != ".png" &&
                            extension != ".webp")
                        {
                            continue;
                        }

                        images.Add(rarEntry);

                        // Si no hay imagenes en el archivo de comics
                        if (images.Count == 0)
                        {
                            MessageBox.Show("No se encontraron imágenes en el archivo .cbz", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    totalPages = images.Count - 1;

                    // Ordenando la lista de imágenes por su nombre
                    sortedImagesRar = images.OrderBy(entry => entry.Key).ToList();

                    // Preparando la página actual
                    var actualPageRar = sortedImagesRar[position];

                    // Creando un stream a partir de los datos de la imagen seleccionada
                    stream = actualPageRar.OpenEntryStream();

                    LoadInDisplay(stream);
                }

                else MessageBox.Show("El archivo seleccionado no es compatible", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
       



            

   



        // Boton para retrodecer en 1 la página
        private void Button_BackPage(object sender, RoutedEventArgs e)
        {
            if (position !=0)
            {
                position--;

                //Preparando la página actual
                DisplayCurrentPage();
            }
        }

        // Boton para adelantar en 1 la página
        private void Button_NextPage(object sender, RoutedEventArgs e)
        {
            if (position != totalPages)
            {
                position++;

                //Preparando la página actual
                DisplayCurrentPage();
            }
        }
    }
}