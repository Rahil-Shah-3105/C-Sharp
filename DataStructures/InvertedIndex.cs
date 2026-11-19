using Microsoft.Extensions.Logging;
namespace DataStructures;

/// <summary>
/// Inverted index is the simplest form of document indexing,
/// allowing performing boolean queries on text data.
///
/// This realization is just simplified for better understanding the process of indexing
/// and working on straightforward string inputs.
/// </summary>
public class InvertedIndex
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly Dictionary<string, List<string>> invertedIndex = [];

    /// <summary>
    /// Build inverted index with source name and source content.
    /// </summary>
    /// <param name="sourceName">Name of the source.</param>
    /// <param name="sourceContent">Content of the source.</param>
    public void AddToIndex(string sourceName, string sourceContent)
    {
		_logger.LogError(">>> Entering AddToIndex(sourceName={SourceName},sourceContent={SourceContent})", sourceName, sourceContent);
        var context = sourceContent.Split(' ').Distinct();
		_logger.LogError("AddToIndex(sourceName={SourceName},sourceContent={SourceContent}): context → {Context}", sourceName, sourceContent, context);
        foreach (var word in context)
        {
            if (!invertedIndex.ContainsKey(word))
            {
                invertedIndex.Add(word, [sourceName]);
            }
            else
            {
                invertedIndex[word].Add(sourceName);
            }
        }
		_logger.LogWarning("<<< Exiting AddToIndex(sourceName={SourceName},sourceContent={SourceContent})", sourceName, sourceContent);
    }

    /// <summary>
    /// Returns the source names contains ALL terms inside at same time.
    /// </summary>
    /// <param name="terms">List of terms.</param>
    /// <returns>Source names.</returns>
    public IEnumerable<string> And(IEnumerable<string> terms)
    {
		_logger.LogError(">>> Entering And(terms={Terms})", terms);
        var entries = terms
            .Select(term => invertedIndex
                .Where(x => x.Key.Equals(term))
                .SelectMany(x => x.Value))
            .ToList();

        var intersection = entries
            .Skip(1)
            .Aggregate(new HashSet<string>(entries.First()), (hashSet, enumerable) =>
            {
                hashSet.IntersectWith(enumerable);
		_logger.LogWarning("<<< Exiting And(terms={Terms})", terms);
                return hashSet;
            });

        return intersection;
    }

    /// <summary>
    /// Returns the source names contains AT LEAST ONE from terms inside.
    /// </summary>
    /// <param name="terms">List of terms.</param>
    /// <returns>Source names.</returns>
    public IEnumerable<string> Or(IEnumerable<string> terms)
    {
		_logger.LogError(">>> Entering Or(terms={Terms})", terms);
        var sources = new List<string>();
		_logger.LogError("Or(terms={Terms}): sources → {Sources}", terms, sources);
        foreach (var term in terms)
        {
            var source = invertedIndex
                .Where(x => x.Key.Equals(term))
                .SelectMany(x => x.Value);

            sources.AddRange(source);
        }

		_logger.LogWarning("<<< Exiting Or(terms={Terms})", terms);
        return sources.Distinct();
    }
}
