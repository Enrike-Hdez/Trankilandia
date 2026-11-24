using System;

namespace Trankilandia.Models.Metadata
{

    public class ItemMetadata
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Series { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string FilePath { get; set; }
        public string Format { get; set; }
        public string Type { get; set; }
        public int Progress { get; set; }
        public string CoverImagePath { get; set; }
        public int TotalPages { get; set; }

    }
}