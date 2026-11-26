using Microsoft.Win32;
using Reader;
using System.Windows;

namespace Trankilandia
{
    public partial class MainWindow : Window
    {

        // Variables
        private ComicReader comic;
        private ZipReader zipReader = new ZipReader();
        private RarReader rarReader = new RarReader();



        // Método main
        public MainWindow()
        {
            InitializeComponent();
        }


        // Boton para abrir un archivo de comics
        private void Button_OpenArchive(object sender, RoutedEventArgs e)
        {
            var openFile = new OpenFileDialog();

            if (openFile.ShowDialog() != true)
                return;

            // Caputurar el formato
            string format = System.IO.Path.GetExtension(openFile.FileName).ToLowerInvariant();

            // Dependiendo del formato, se abrirá el archivo con el lector correspondiente
            if (format == ".cbz" || format == ".zip")
            {
           
                zipReader.format = format;
                zipReader.OpenCBZ(openFile);
                comic = zipReader;
            }

            else if (format == ".cbr" || format == ".rar")
            {
            
                rarReader.format = format;
                rarReader.OpenCBR(openFile);
                comic = rarReader;
            }

            else
            {
                MessageBox.Show("El archivo seleccionado no es compatible");
                return;
            }

            // Cargando la página actual en la interfaz
            comic.position = 0;
            ComicImage.Source = comic.LoadInDisplay();
        }



        // Boton para retrodecer en 1 la página
        private void Button_BackPage(object sender, RoutedEventArgs e)
        {
            if (comic.position !=0)
            {

                //Preparando la página actual
                comic.position--;
                ComicImage.Source = comic.LoadInDisplay();
            }
        }

        // Boton para adelantar en 1 la página
        private void Button_NextPage(object sender, RoutedEventArgs e)
        {
            if (comic.position != comic.totalPages)
            {

                //Preparando la página actual
                comic.position++;
                ComicImage.Source = comic.LoadInDisplay();
            }
        }
    }
}