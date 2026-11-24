using System;
using Trankilandia.Models.Metadata;
using Trankilandia.Models.Organization;
using Trankilandia.Models.Reading;

namespace Trankilandia.Models
{

    public class LibraryItem
    {
        public ItemMetadata Metadata { get; set; } = new ItemMetadata();

        public ItemOrganization Organization { get; set; } = new ItemOrganization();

        public ReadingProgress Progress { get; set; } = new ReadingProgress();

    }
}