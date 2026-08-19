using Algorithms.Problems.DynamicProgramming.CoinChange;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Problems.DynamicProgramming.CoinChange;

[TestFixture]
public class GenerateSingleCoinChangesTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void GenerateSingleCoinChangesTests_Success()
    {
		_logger.LogDebug(">>> Entering GenerateSingleCoinChangesTests_Success()");
        DynamicCoinChangeSolver
            .GenerateSingleCoinChanges(6, [1, 2, 3])
            .SequenceEqual(new[] { 3, 4, 5 })
            .Should().BeTrue();

        DynamicCoinChangeSolver
            .GenerateSingleCoinChanges(10, [1, 2, 3, 7, 12, 15, 14])
            .SequenceEqual(new[] { 3, 7, 8, 9 })
            .Should().BeTrue();

        DynamicCoinChangeSolver
            .GenerateSingleCoinChanges(1, [1, 2, 3, 7, 12, 15, 14])
            .SequenceEqual(new[] { 0 })
            .Should().BeTrue();

        DynamicCoinChangeSolver
            .GenerateSingleCoinChanges(2, [1, 2, 3, 7, 12, 15, 14])
            .SequenceEqual(new[] { 0, 1 })
            .Should().BeTrue();
			_logger.LogDebug("<<< Exiting GenerateSingleCoinChangesTests_Success()");
    }

    [Test]
    public void GenerateSingleCoinChangesTests_ShouldThrow_CoinCannotBeLesserOrEqualZero()
    {
		_logger.LogDebug(">>> Entering GenerateSingleCoinChangesTests_ShouldThrow_CoinCannotBeLesserOrEqualZero()");
        const int coin = 0;
        var arr = new[] { 1, 2, 3 };

        Func<int[]> act = () => DynamicCoinChangeSolver.GenerateSingleCoinChanges(coin, arr);

        act.Should().Throw<InvalidOperationException>()
            .WithMessage($"The coin cannot be lesser or equal to zero {nameof(coin)}.");
			_logger.LogDebug("<<< Exiting GenerateSingleCoinChangesTests_ShouldThrow_CoinCannotBeLesserOrEqualZero()");
    }

    [Test]
    public void GenerateSingleCoinChangesTests_ShouldThrow_CoinsArrayCannotBeEmpty()
    {
		_logger.LogDebug(">>> Entering GenerateSingleCoinChangesTests_ShouldThrow_CoinsArrayCannotBeEmpty()");
        const int coin = 10;
        var coinsAsArray = Array.Empty<int>();
		_logger.LogDebug("GenerateSingleCoinChangesTests_ShouldThrow_CoinsArrayCannotBeEmpty(): coinsAsArray → {CoinsAsArray}", coinsAsArray);

        Func<int[]> act = () => DynamicCoinChangeSolver.GenerateSingleCoinChanges(coin, coinsAsArray);

        act.Should().Throw<InvalidOperationException>()
            .WithMessage($"Coins array cannot be empty {nameof(coinsAsArray)}.");
			_logger.LogDebug("<<< Exiting GenerateSingleCoinChangesTests_ShouldThrow_CoinsArrayCannotBeEmpty()");
    }

    [Test]
    public void GenerateSingleCoinChangesTests_ShouldThrow_CoinsArrayMustContainOne()
    {
		_logger.LogDebug(">>> Entering GenerateSingleCoinChangesTests_ShouldThrow_CoinsArrayMustContainOne()");
        const int coin = 10;
        var coinsAsArray = new[] { 2, 3, 4 };

        Func<int[]> act = () => DynamicCoinChangeSolver.GenerateSingleCoinChanges(coin, coinsAsArray);

        act.Should().Throw<InvalidOperationException>()
            .WithMessage($"Coins array must contain coin 1 {nameof(coinsAsArray)}.");
			_logger.LogDebug("<<< Exiting GenerateSingleCoinChangesTests_ShouldThrow_CoinsArrayMustContainOne()");
    }

    [Test]
    public void GenerateSingleCoinChangesTests_ShouldThrow_CoinsArrayCannotContainNegativeValues()
    {
		_logger.LogDebug(">>> Entering GenerateSingleCoinChangesTests_ShouldThrow_CoinsArrayCannotContainNegativeValues()");
        const int coin = 10;
        var coinsAsArray = new[] { 1, 2, -3, 4 };

        Func<int[]> act = () => DynamicCoinChangeSolver.GenerateSingleCoinChanges(coin, coinsAsArray);

        act.Should().Throw<InvalidOperationException>()
            .WithMessage($"{nameof(coinsAsArray)} cannot contain numbers less than or equal to zero");
			_logger.LogDebug("<<< Exiting GenerateSingleCoinChangesTests_ShouldThrow_CoinsArrayCannotContainNegativeValues()");
    }

    [Test]
    public void GenerateSingleCoinChangesTests_ShouldThrow_CoinsArrayCannotContainDuplicates()
    {
		_logger.LogDebug(">>> Entering GenerateSingleCoinChangesTests_ShouldThrow_CoinsArrayCannotContainDuplicates()");
        const int coin = 10;
        var coinsAsArray = new[] { 1, 2, 3, 3, 4 };

        Func<int[]> act = () => DynamicCoinChangeSolver.GenerateSingleCoinChanges(coin, coinsAsArray);

        act.Should().Throw<InvalidOperationException>()
            .WithMessage($"Coins array cannot contain duplicates {nameof(coinsAsArray)}.");
			_logger.LogDebug("<<< Exiting GenerateSingleCoinChangesTests_ShouldThrow_CoinsArrayCannotContainDuplicates()");
    }
}
