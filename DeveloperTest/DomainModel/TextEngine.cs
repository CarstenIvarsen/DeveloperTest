using DeveloperTest.DatabaseLayer;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DeveloperTest.DomainModel
{
    /// <summary>
    /// Extracts disctinct number of unique words and watch list words in a specified text.
    /// </summary>
    public class TextEngine
    {
        private readonly ILogger<TextEngine> _logger;
        private readonly DeveloperTestDbContext _dbContext;

        /// <summary>
        /// Constructor used for dependency injection
        /// </summary>
        public TextEngine(ILogger<TextEngine> logger, DeveloperTestDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Extracts all destinct unique words from the specified text.
        /// </summary>
        /// <param name="lowerCaseText">The lowercase text to extract destinct unique words from</param>
        /// <returns>A list of all distinct unique words found in specifed text</returns>
        private List<string> ExtractDistinctUniqueWords(string lowerCaseText)
        {
            // Use regular expression to extract words from text parameter
            var regex = new Regex(@"\w+", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            var matchCollection = regex.Matches(lowerCaseText);
            // Create a list of distinct words from the matches in the regular expression
            var listOfWords = matchCollection.OfType<Match>().Select(x => x.Value).Distinct().ToList();
            return listOfWords;
        }

        /// <summary>
        /// Extracts all watch list words found among the distinct unique words extracted from specified text
        /// </summary>
        /// <param name="distinctUniqueWords">A distinct unique words list extracted from specified text</param>
        /// <returns>A list of extracted watch list words</returns>
        private List<string> ExtractWatchListWords(List<string> distinctUniqueWords)
        {
            // Get list of watchListWords from database
            // TODO: Cache this in memory for 1-5 minutes to avoid roundtrips to database
            var watchListWords = _dbContext.WatchLists.Select(x => x.Word).ToList();
            // Use Linq to find matching watch list words
            var foundWatchListWords = watchListWords.Intersect(distinctUniqueWords).ToList();
            return foundWatchListWords;
        }

        /// <summary>
        /// Creates an instance of TextResponse with default information
        /// </summary>
        private TextResponse DefaultResponse => new()
        {
            DistinctUniqueWords = 0,
            WatchListWords = new()
        };

        /// <summary>
        /// Finds number of distinct unique words and watch list words from a specified text.
        /// Adds the found distinct unique words and watch list to database.
        /// </summary>
        /// <param name="textRequest"></param>
        /// <returns></returns>
        public TextResponse HandleTextRequest(TextRequest textRequest)
        {
            // Test for missing instance of TextRequest or empty text and return a default TextResponse
            if (textRequest == null || string.IsNullOrWhiteSpace(textRequest.Text))
            {
                _logger.LogWarning("TextEngine could not process request since passed parameter is null or contains empty text");
                return DefaultResponse;
            }

            try
            {
                // Make lowercase in order to avoid same occurences with only case difference. Eg. Apple and apple
                var lowerCaseText = textRequest.Text.ToLowerInvariant();
                
                // Get information about disctinct unique words in text
                var distinctUniqueWords = ExtractDistinctUniqueWords(lowerCaseText);
                var numberOfdistinctUniqueWords = distinctUniqueWords.Count;

                // Get information about watch list words in text
                var watchListWords = ExtractWatchListWords(distinctUniqueWords);
                var numberOFwatchListWords = watchListWords.Count;

                // Save the findings to database
                _dbContext.TextEngineResponseLists.Add(new TextEngineResponseList
                {
                    DistinctUniqueWordCount = numberOfdistinctUniqueWords,
                    DistinctUniqueWords = string.Join(", ", distinctUniqueWords),
                    WatchListWordCount = numberOFwatchListWords,
                    WatchListWords = string.Join(", ", watchListWords)                    
                });
                _dbContext.SaveChanges();

                // Return new instance of TextResponse with findings
                return new TextResponse
                {
                    DistinctUniqueWords = numberOfdistinctUniqueWords,
                    WatchListWords = watchListWords
                };
            }
            catch (Exception ex)
            {
                // Log exception and return instance of TextResponse with default information
                _logger.LogError("General error occured in TextEngine", ex);
                return DefaultResponse;
            }
        }

    }
}
