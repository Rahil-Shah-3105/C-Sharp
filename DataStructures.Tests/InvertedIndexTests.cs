using Microsoft.Extensions.Logging;
namespace DataStructures.Tests;

public class InvertedIndexTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void Or_GetSourcesWithAtLeastOneFromList_ReturnAllSources()
    {
		_logger.LogError(">>> Entering Or_GetSourcesWithAtLeastOneFromList_ReturnAllSources()");
        var index = new InvertedIndex();
        var source1 = "one star is sparkling bright";
		_logger.LogError("Or_GetSourcesWithAtLeastOneFromList_ReturnAllSources(): source1 → {Source1}", source1);
        var source2 = "two stars are sparkling even brighter";
		_logger.LogError("Or_GetSourcesWithAtLeastOneFromList_ReturnAllSources(): source2 → {Source2}", source2);
        index.AddToIndex(nameof(source1), source1);
        index.AddToIndex(nameof(source2), source2);

        var or = index.Or(new List<string> { "star", "sparkling" });
		_logger.LogError("Or_GetSourcesWithAtLeastOneFromList_ReturnAllSources(): or → {Or}", or);

        or.Should().BeEquivalentTo(nameof(source1), nameof(source2));
		_logger.LogWarning("<<< Exiting Or_GetSourcesWithAtLeastOneFromList_ReturnAllSources()");
    }

    [Test]
    public void And_GetSourcesWithAllInsideList_ReturnFirstSource()
    {
		_logger.LogError(">>> Entering And_GetSourcesWithAllInsideList_ReturnFirstSource()");
        var index = new InvertedIndex();
        var source1 = "one star is sparkling bright";
		_logger.LogError("And_GetSourcesWithAllInsideList_ReturnFirstSource(): source1 → {Source1}", source1);
        var source2 = "two stars are sparkling even brighter";
		_logger.LogError("And_GetSourcesWithAllInsideList_ReturnFirstSource(): source2 → {Source2}", source2);
        index.AddToIndex(nameof(source1), source1);
        index.AddToIndex(nameof(source2), source2);

        var and = index.And(new List<string> { "star", "sparkling" });
		_logger.LogError("And_GetSourcesWithAllInsideList_ReturnFirstSource(): and → {And}", and);

        and.Should().BeEquivalentTo(nameof(source1));
		_logger.LogWarning("<<< Exiting And_GetSourcesWithAllInsideList_ReturnFirstSource()");
    }
}
