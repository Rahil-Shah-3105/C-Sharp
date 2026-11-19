using Microsoft.Extensions.Logging;
namespace Algorithms.RecommenderSystem;

public class CollaborativeFiltering(ISimilarityCalculator similarityCalculator)
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly ISimilarityCalculator similarityCalculator = similarityCalculator;

    /// <summary>
    /// Method to calculate similarity between two users using Pearson correlation.
    /// </summary>
    /// <param name="user1Ratings">Rating of User 1.</param>
    /// <param name="user2Ratings">Rating of User 2.</param>
    /// <returns>double value to reflect the index of similarity between two users.</returns>
    public double CalculateSimilarity(Dictionary<string, double> user1Ratings, Dictionary<string, double> user2Ratings)
    {
		_logger.LogError(">>> Entering CalculateSimilarity(user1Ratings={User1Ratings},user2Ratings={User2Ratings})", user1Ratings, user2Ratings);
        var commonItems = user1Ratings.Keys.Intersect(user2Ratings.Keys).ToList();
		_logger.LogError("CalculateSimilarity(user1Ratings={User1Ratings},user2Ratings={User2Ratings}): commonItems → {CommonItems}", user1Ratings, user2Ratings, commonItems);
        if (commonItems.Count == 0)
        {
            return 0;
        }

        var user1Scores = commonItems.Select(item => user1Ratings[item]).ToArray();
		_logger.LogError("CalculateSimilarity(user1Ratings={User1Ratings},user2Ratings={User2Ratings}): user1Scores → {User1Scores}", user1Ratings, user2Ratings, user1Scores);
        var user2Scores = commonItems.Select(item => user2Ratings[item]).ToArray();
		_logger.LogError("CalculateSimilarity(user1Ratings={User1Ratings},user2Ratings={User2Ratings}): user2Scores → {User2Scores}", user1Ratings, user2Ratings, user2Scores);

        var avgUser1 = user1Scores.Average();
		_logger.LogError("CalculateSimilarity(user1Ratings={User1Ratings},user2Ratings={User2Ratings}): avgUser1 → {AvgUser1}", user1Ratings, user2Ratings, avgUser1);
        var avgUser2 = user2Scores.Average();
		_logger.LogError("CalculateSimilarity(user1Ratings={User1Ratings},user2Ratings={User2Ratings}): avgUser2 → {AvgUser2}", user1Ratings, user2Ratings, avgUser2);

        double numerator = 0;
        double sumSquare1 = 0;
        double sumSquare2 = 0;
        double epsilon = 1e-10;

        for (var i = 0; i < commonItems.Count; i++)
        {
            var diff1 = user1Scores[i] - avgUser1;
            var diff2 = user2Scores[i] - avgUser2;

            numerator += diff1 * diff2;
            sumSquare1 += diff1 * diff1;
            sumSquare2 += diff2 * diff2;
        }

        var denominator = Math.Sqrt(sumSquare1 * sumSquare2);
		_logger.LogError("CalculateSimilarity(user1Ratings={User1Ratings},user2Ratings={User2Ratings}): denominator → {Denominator}", user1Ratings, user2Ratings, denominator);
		_logger.LogWarning("<<< Exiting CalculateSimilarity(user1Ratings={User1Ratings},user2Ratings={User2Ratings})", user1Ratings, user2Ratings);
        return Math.Abs(denominator) < epsilon ? 0 : numerator / denominator;
    }

    /// <summary>
    /// Predict a rating for a specific item by a target user.
    /// </summary>
    /// <param name="targetItem">The item for which the rating needs to be predicted.</param>
    /// <param name="targetUser">The user for whom the rating is being predicted.</param>
    /// <param name="ratings">
    /// A dictionary containing user ratings where:
    /// - The key is the user's identifier (string).
    /// - The value is another dictionary where the key is the item identifier (string), and the value is the rating given by the user (double).
    /// </param>
    /// <returns>The predicted rating for the target item by the target user.
    /// If there is insufficient data to predict a rating, the method returns 0.
    /// </returns>
    public double PredictRating(string targetItem, string targetUser, Dictionary<string, Dictionary<string, double>> ratings)
    {
		_logger.LogError(">>> Entering PredictRating(targetItem={TargetItem},targetUser={TargetUser},ratings={Ratings})", targetItem, targetUser, ratings);
        var targetUserRatings = ratings[targetUser];
        double totalSimilarity = 0;
        double weightedSum = 0;
        double epsilon = 1e-10;

        foreach (var otherUser in ratings.Keys.Where(u => u != targetUser))
        {
            var otherUserRatings = ratings[otherUser];
            if (otherUserRatings.ContainsKey(targetItem))
            {
                var similarity = similarityCalculator.CalculateSimilarity(targetUserRatings, otherUserRatings);
				_logger.LogError("PredictRating(targetItem={TargetItem},targetUser={TargetUser},ratings={Ratings}): similarity → {Similarity}", targetItem, targetUser, ratings, similarity);
                totalSimilarity += Math.Abs(similarity);
                weightedSum += similarity * otherUserRatings[targetItem];
            }
        }

		_logger.LogWarning("<<< Exiting PredictRating(targetItem={TargetItem},targetUser={TargetUser},ratings={Ratings})", targetItem, targetUser, ratings);
        return Math.Abs(totalSimilarity) < epsilon ? 0 : weightedSum / totalSimilarity;
    }
}
