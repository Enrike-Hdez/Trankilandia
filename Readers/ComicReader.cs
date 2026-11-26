using SharpCompress.Archives;
using System.IO;
using System.IO.Compression;
using System.Windows.Media.Imaging;

namespace Reader
{
    public class ComicReader
    {
        // Variables
        public string format = ".cbz";
        public int position = 0;
        public int totalPages = 0;
        public string filePath = "";
        public Stream stream;
        public List<ZipArchiveEntry> sortedImagesZip = new List<ZipArchiveEntry>();
        public List<IArchiveEntry> sortedImagesRar = new List<IArchiveEntry>();


        // Métodos
        //Cargar el BitMap para cargar la página actual
        public BitmapImage LoadInDisplay()
        {
            if (format == ".cbz" || format == ".zip")
                stream = sortedImagesZip[position].Open();
            else if (format == ".cbr" || format == ".rar")
                stream = sortedImagesRar[position].OpenEntryStream();
            else
                throw new InvalidOperationException($"Formato no compatible: {format}");


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
                return bitmap;
            }
        }
    }
}