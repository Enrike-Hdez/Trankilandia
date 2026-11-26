using Microsoft.Win32;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using System.IO;

using System.Windows;
    
namespace Reader
{
    public class RarReader : ComicReader
    {
        // Abriendo el archivo rar, cbr
        public void OpenCBR(OpenFileDialog openfile)
        {
            position = 0;

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
            Stream stream = actualPageRar.OpenEntryStream();
        }
    }
}