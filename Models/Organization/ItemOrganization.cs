using System;

namespace Trankilandia.Models.Organization
{

    public class ItemOrganization
    {
        public List<string> Tags { get; set; }
        public List<string> Genres { get; set; }
        public string Rating { get; set; }
        public bool IsFavorite { get; set; }
        public DateTime DateAdded { get; set; }

    }
}