using System;

namespace Trankilandia.Models.Reading
{

    public class ReadingProgress
    {
        public TimeSpan ReadingTime { get; set; }
        public DateTime LastReadDate { get; set; }
        public string ReadingStatus { get; set; }
        public int Progress { get; set; }

    }
}
