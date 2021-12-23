using System.Collections.Generic;

namespace DeveloperTest.DomainModel
{
    /// <summary>
    /// Used in TextController for POST response as DTO to return result from TextEngine
    /// </summary>
    public class TextResponse
    {
        /// <summary>
        /// Number of distinct unique words found in input text
        /// </summary>
        public int DistinctUniqueWords { get; set; }
        /// <summary>
        /// List of watch list words found in input text
        /// </summary>
        public List<string> WatchListWords { get; set; }
    }
}
