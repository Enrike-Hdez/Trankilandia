using Microsoft.Win32;
using System.IO.Compression;
using System.Windows;
using System.IO;


namespace Reader
{
    public class ZipReader : ComicReader
    {
        // Abriendo el archivo zip, cbz
        public void OpenCBZ(OpenFileDialog openfile)
        {
            position = 0;

            List<ZipArchiveEntry> images = new List<ZipArchiveEntry>();

            // Guardando la ruta del archivo
            filePath = openfile.FileName;

            // Preparando el archivo .cbz para leerlo
            var archive = ZipFile.OpenRead(filePath);

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
            Stream stream = actualPageZip.Open();
        }
    }
}