using System;

#nullable disable

namespace DeveloperTest.DatabaseLayer
{
    public partial class WatchList
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Word { get; set; }
    }
}
