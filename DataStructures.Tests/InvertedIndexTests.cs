using Microsoft.Extensions.Logging;
namespace DataStructures.Tests;

public class InvertedIndexTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void Or_GetSourcesWithAtLeastOneFromList_ReturnAllSources()
    {
		_logger.LogDebug(">>> Entering Or_GetSourcesWithAtLeastOneFromList_ReturnAllSources()");
        var index = new InvertedIndex();
        var source1 = "one star is sparkling bright";
		_logger.LogDebug("Or_GetSourcesWithAtLeastOneFromList_ReturnAllSources(): source1 → {Source1}", source1);
        var source2 = "two stars are sparkling even brighter";
		_logger.LogDebug("Or_GetSourcesWithAtLeastOneFromList_ReturnAllSources(): source2 → {Source2}", source2);
        index.AddToIndex(nameof(source1), source1);
        index.AddToIndex(nameof(source2), source2);

        var or = index.Or(new List<string> { "star", "sparkling" });
		_logger.LogDebug("Or_GetSourcesWithAtLeastOneFromList_ReturnAllSources(): or → {Or}", or);

        or.Should().BeEquivalentTo(nameof(source1), nameof(source2));
		_logger.LogDebug("<<< Exiting Or_GetSourcesWithAtLeastOneFromList_ReturnAllSources()");
    }

    [Test]
    public void And_GetSourcesWithAllInsideList_ReturnFirstSource()
    {
		_logger.LogDebug(">>> Entering And_GetSourcesWithAllInsideList_ReturnFirstSource()");
        var index = new InvertedIndex();
        var source1 = "one star is sparkling bright";
		_logger.LogDebug("And_GetSourcesWithAllInsideList_ReturnFirstSource(): source1 → {Source1}", source1);
        var source2 = "two stars are sparkling even brighter";
		_logger.LogDebug("And_GetSourcesWithAllInsideList_ReturnFirstSource(): source2 → {Source2}", source2);
        index.AddToIndex(nameof(source1), source1);
        index.AddToIndex(nameof(source2), source2);

        var and = index.And(new List<string> { "star", "sparkling" });
		_logger.LogDebug("And_GetSourcesWithAllInsideList_ReturnFirstSource(): and → {And}", and);

        and.Should().BeEquivalentTo(nameof(source1));
		_logger.LogDebug("<<< Exiting And_GetSourcesWithAllInsideList_ReturnFirstSource()");
    }
}
