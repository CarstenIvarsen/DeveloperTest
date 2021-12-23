using System.ComponentModel.DataAnnotations;

namespace DeveloperTest.DomainModel
{
    /// <summary>
    /// Used in TextController POST request as DTO for required information
    /// </summary>
    public class TextRequest
    {
        /// <summary>
        /// The required text to search for distinct unique words and watch list words
        /// </summary>
        [Required]
        public string Text { get; set; }
    }
}
