using Microsoft.Win32;
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

        // Variables de clase
        private int position = 0;
        private int totalPages = 0;
        private ZipArchive archive;
        List<ZipArchiveEntry> sortedImages = new List<ZipArchiveEntry>();



        // Método main
        public MainWindow()
        {
            InitializeComponent();
        }



        //Métodos privados

        //Cargar el BitMap para cargar la página actual
        private void LoadInDisplay(List<ZipArchiveEntry> sortedImages, int position)
        {
            var actualPage = sortedImages[position];

            // Creando un stream a partir de los datos de la imagen seleccionada
            using (Stream stream = actualPage.Open())
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

        // Botones
        // Botón para abrir un archivo .cbz
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Abrir cuadro de diálogo para seleccionar un archivo .cbz
            OpenFileDialog openfile = new OpenFileDialog();


            if (openfile.ShowDialog() == true)
            {
                List<ZipArchiveEntry> images = new List<ZipArchiveEntry>();

                // Guardando la ruta del archivo
                string filePath = openfile.FileName;

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
                }

                totalPages = images.Count - 1;
        
                // Ordenando la lista de imágenes por su nombre
                sortedImages = images.OrderBy(entry => entry.FullName).ToList();

                // Preparando la página actual
                LoadInDisplay(sortedImages, position);
            }
        }


        // Boton para retrodecer en 1 la página
        private void Button_BackPage(object sender, RoutedEventArgs e)
        {
            if (position !=0)
            {
                position--;

                // Preparando la página actual
                LoadInDisplay(sortedImages, position);
            }
        }

        // Boton para adelantar en 1 la página
        private void Button_NextPage(object sender, RoutedEventArgs e)
        {
            if (position != totalPages)
            {
                position++;

                // Preparando la página actual
                LoadInDisplay(sortedImages, position);
            }
        }
    }
}