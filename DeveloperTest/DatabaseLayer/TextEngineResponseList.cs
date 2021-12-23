using System;

#nullable disable

namespace DeveloperTest.DatabaseLayer
{
    public partial class TextEngineResponseList
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int DistinctUniqueWordCount { get; set; }
        public string DistinctUniqueWords { get; set; }
        public int WatchListWordCount { get; set; }
        public string WatchListWords { get; set; }
    }
}
