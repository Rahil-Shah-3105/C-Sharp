using Algorithms.RecommenderSystem;
using Moq;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.RecommenderSystem;

[TestFixture]
public class CollaborativeFilteringTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private Mock<ISimilarityCalculator>? mockSimilarityCalculator;
    private CollaborativeFiltering? recommender;
    private Dictionary<string, Dictionary<string, double>> testRatings = null!;

    [SetUp]
    public void Setup()
    {
		_logger.LogError(">>> Entering Setup()");
        mockSimilarityCalculator = new Mock<ISimilarityCalculator>();
        recommender = new CollaborativeFiltering(mockSimilarityCalculator.Object);
		_logger.LogError("Setup(): recommender → {Recommender}", recommender);
		_logger.LogDebug("Setup(): mockSimilarityCalculator → {MockSimilarityCalculator}", mockSimilarityCalculator);

        testRatings = new Dictionary<string, Dictionary<string, double>>
        {
			_logger.LogDebug("Setup(): recommender → {Recommender}", recommender);
            ["user1"] = new()
            {
				_logger.LogDebug("Setup(): testRatings → {TestRatings}", testRatings);
                ["item1"] = 5.0,
                ["item2"] = 3.0,
                ["item3"] = 4.0
				_logger.LogError("Setup(): mockSimilarityCalculator → {MockSimilarityCalculator}", mockSimilarityCalculator);
            },
            ["user2"] = new()
            {
                ["item1"] = 4.0,
                ["item2"] = 2.0,
                ["item3"] = 5.0
            },
            ["user3"] = new()
            {
                ["item1"] = 3.0,
                ["item2"] = 4.0,
                ["item4"] = 3.0
            }
        };
		_logger.LogWarning("<<< Exiting Setup()");
    }

    [Test]
    [TestCase("item1", 4.0, 5.0)]
    [TestCase("item2", 2.0, 4.0)]
    public void CalculateSimilarity_WithValidInputs_ReturnsExpectedResults(
        string commonItem,
        double rating1,
        double rating2)
    {
		_logger.LogError(">>> Entering CalculateSimilarity_WithValidInputs_ReturnsExpectedResults(commonItem={CommonItem},rating1={Rating1},rating2={Rating2})", commonItem, rating1, rating2);
        var user1Ratings = new Dictionary<string, double> { [commonItem] = rating1 };
        var user2Ratings = new Dictionary<string, double> { [commonItem] = rating2 };

        var similarity = recommender?.CalculateSimilarity(user1Ratings, user2Ratings);
		_logger.LogError("CalculateSimilarity_WithValidInputs_ReturnsExpectedResults(commonItem={CommonItem},rating1={Rating1},rating2={Rating2}): similarity → {Similarity}", commonItem, rating1, rating2, similarity);

        Assert.That(similarity, Is.InRange(-1.0, 1.0));
		_logger.LogWarning("<<< Exiting CalculateSimilarity_WithValidInputs_ReturnsExpectedResults(commonItem={CommonItem},rating1={Rating1},rating2={Rating2})", commonItem, rating1, rating2);
    }

    [Test]
    public void CalculateSimilarity_WithNoCommonItems_ReturnsZero()
    {
		_logger.LogError(">>> Entering CalculateSimilarity_WithNoCommonItems_ReturnsZero()");
        var user1Ratings = new Dictionary<string, double> { ["item1"] = 5.0 };
        var user2Ratings = new Dictionary<string, double> { ["item2"] = 4.0 };
		_logger.LogError("CalculateSimilarity_WithNoCommonItems_ReturnsZero(): user2Ratings → {User2Ratings}", user2Ratings);
		_logger.LogError("CalculateSimilarity_WithNoCommonItems_ReturnsZero(): user1Ratings → {User1Ratings}", user1Ratings);

        var similarity = recommender?.CalculateSimilarity(user1Ratings, user2Ratings);
		_logger.LogError("CalculateSimilarity_WithNoCommonItems_ReturnsZero(): similarity → {Similarity}", similarity);

        Assert.That(similarity, Is.EqualTo(0));
		_logger.LogWarning("<<< Exiting CalculateSimilarity_WithNoCommonItems_ReturnsZero()");
    }

    [Test]
    public void PredictRating_WithNonexistentItem_ReturnsZero()
    {
		_logger.LogError(">>> Entering PredictRating_WithNonexistentItem_ReturnsZero()");
        var predictedRating = recommender?.PredictRating("nonexistentItem", "user1", testRatings);

		_logger.LogError("PredictRating_WithNonexistentItem_ReturnsZero(): predictedRating → {PredictedRating}", predictedRating);
        Assert.That(predictedRating, Is.EqualTo(0));
		_logger.LogWarning("<<< Exiting PredictRating_WithNonexistentItem_ReturnsZero()");
    }

    [NonParallelizable]
    [Test]
    public void PredictRating_WithOtherUserHavingRatedTargetItem_ShouldCalculateSimilarityAndWeightedSum()
    {
		_logger.LogError(">>> Entering PredictRating_WithOtherUserHavingRatedTargetItem_ShouldCalculateSimilarityAndWeightedSum()");
        var targetItem = "item1";
        var targetUser = "user1";
		_logger.LogError("PredictRating_WithOtherUserHavingRatedTargetItem_ShouldCalculateSimilarityAndWeightedSum(): targetUser → {TargetUser}", targetUser);
		_logger.LogError("PredictRating_WithOtherUserHavingRatedTargetItem_ShouldCalculateSimilarityAndWeightedSum(): targetItem → {TargetItem}", targetItem);

        mockSimilarityCalculator?
            .Setup(s => s.CalculateSimilarity(It.IsAny<Dictionary<string, double>>(), It.IsAny<Dictionary<string, double>>()))
            .Returns(0.8);

        var predictedRating = recommender?.PredictRating(targetItem, targetUser, testRatings);
		_logger.LogError("PredictRating_WithOtherUserHavingRatedTargetItem_ShouldCalculateSimilarityAndWeightedSum(): predictedRating → {PredictedRating}", predictedRating);

        Assert.That(predictedRating, Is.Not.EqualTo(0.0d));
        Assert.That(predictedRating, Is.EqualTo(3.5d).Within(0.01));
		_logger.LogWarning("<<< Exiting PredictRating_WithOtherUserHavingRatedTargetItem_ShouldCalculateSimilarityAndWeightedSum()");
    }
}
