using Algorithms.Numeric;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric;

public class AutomorphicNumberTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(6)]
    [TestCase(25)]
    [TestCase(76)]
    [TestCase(376)]
    [TestCase(625)]
    [TestCase(9376)]
    [TestCase(90625)]
    [TestCase(109376)]

    public void TestAutomorphicNumbers(int number)
    {
		_logger.LogDebug(">>> Entering TestAutomorphicNumbers(number={Number})", number);
        Assert.That(AutomorphicNumber.IsAutomorphic(number), Is.True);
		_logger.LogDebug("<<< Exiting TestAutomorphicNumbers(number={Number})", number);
    }

    [TestCase(2)]
    [TestCase(3)]
    [TestCase(7)]
    [TestCase(18)]
    [TestCase(79)]
    [TestCase(356)]
    [TestCase(623)]
    [TestCase(9876)]
    [TestCase(90635)]
    [TestCase(119376)]
    [TestCase(891625)]
    [TestCase(2990625)]
    [TestCase(7209376)]
    [TestCase(12891625)]
    [TestCase(87129396)]
    public void TestNonAutomorphicNumbers(int number)
    {
		_logger.LogDebug(">>> Entering TestNonAutomorphicNumbers(number={Number})", number);
        Assert.That(AutomorphicNumber.IsAutomorphic(number), Is.False);
		_logger.LogDebug("<<< Exiting TestNonAutomorphicNumbers(number={Number})", number);
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void TestInvalidAutomorphicNumbers(int number)
    {
		_logger.LogDebug(">>> Entering TestInvalidAutomorphicNumbers(number={Number})", number);
        Assert.Throws(Is.TypeOf<ArgumentException>()
            .And.Message.EqualTo($"An automorphic number must always be positive."),
            delegate
            {
                AutomorphicNumber.IsAutomorphic(number);
            });
			_logger.LogDebug("<<< Exiting TestInvalidAutomorphicNumbers(number={Number})", number);
    }

    [TestCase(1, 100)]
    public void TestAutomorphicNumberSequence(int lower, int upper)
    {
		_logger.LogDebug(">>> Entering TestAutomorphicNumberSequence(lower={Lower},upper={Upper})", lower, upper);
        List<long> automorphicList = [1, 5, 6, 25, 76];
        Assert.That(AutomorphicNumber.GetAutomorphicNumbers(lower, upper), Is.EqualTo(automorphicList));
		_logger.LogDebug("<<< Exiting TestAutomorphicNumberSequence(lower={Lower},upper={Upper})", lower, upper);
    }

    [TestCase(8, 12)]
    public void TestNoAutomorphicNumberInTheSequence(int lower, int upper)
    {
		_logger.LogDebug(">>> Entering TestNoAutomorphicNumberInTheSequence(lower={Lower},upper={Upper})", lower, upper);
        List<long> automorphicList = [];
        Assert.That(AutomorphicNumber.GetAutomorphicNumbers(lower, upper), Is.EqualTo(automorphicList));
		_logger.LogDebug("<<< Exiting TestNoAutomorphicNumberInTheSequence(lower={Lower},upper={Upper})", lower, upper);
    }

    [TestCase(25, 25)]
    public void TestAutomorphicNumberSequenceSameBounds(int lower, int upper)
    {
		_logger.LogDebug(">>> Entering TestAutomorphicNumberSequenceSameBounds(lower={Lower},upper={Upper})", lower, upper);
        List<long> automorphicList = [25];
        Assert.That(AutomorphicNumber.GetAutomorphicNumbers(lower, upper), Is.EqualTo(automorphicList));
		_logger.LogDebug("<<< Exiting TestAutomorphicNumberSequenceSameBounds(lower={Lower},upper={Upper})", lower, upper);
    }

    [TestCase(-1, 1)]
    [TestCase(0, 1)]
    public void TestAutomorphicNumberSequenceInvalidLowerBound(int lower, int upper)
    {
		_logger.LogDebug(">>> Entering TestAutomorphicNumberSequenceInvalidLowerBound(lower={Lower},upper={Upper})", lower, upper);
        Assert.Throws(Is.TypeOf<ArgumentException>()
            .And.Message.EqualTo($"Lower bound must be greater than 0."),
            delegate
            {
                AutomorphicNumber.GetAutomorphicNumbers(lower, upper);
            });
			_logger.LogDebug("<<< Exiting TestAutomorphicNumberSequenceInvalidLowerBound(lower={Lower},upper={Upper})", lower, upper);
    }

    [TestCase(1, -1)]
    [TestCase(10, -1)]
    public void TestAutomorphicNumberSequenceInvalidUpperBound(int lower, int upper)
    {
		_logger.LogDebug(">>> Entering TestAutomorphicNumberSequenceInvalidUpperBound(lower={Lower},upper={Upper})", lower, upper);
        Assert.Throws(Is.TypeOf<ArgumentException>()
            .And.Message.EqualTo($"Upper bound must be greater than 0."),
            delegate
            {
                AutomorphicNumber.GetAutomorphicNumbers(lower, upper);
            });
			_logger.LogDebug("<<< Exiting TestAutomorphicNumberSequenceInvalidUpperBound(lower={Lower},upper={Upper})", lower, upper);
    }

    [TestCase(25, 2)]
    public void TestAutomorphicNumberSequenceReversedBounds(int lower, int upper)
    {
		_logger.LogDebug(">>> Entering TestAutomorphicNumberSequenceReversedBounds(lower={Lower},upper={Upper})", lower, upper);
        Assert.Throws(Is.TypeOf<ArgumentException>()
            .And.Message.EqualTo($"The lower bound must be less than or equal to the upper bound."),
            delegate
            {
                AutomorphicNumber.GetAutomorphicNumbers(lower, upper);
            });
			_logger.LogDebug("<<< Exiting TestAutomorphicNumberSequenceReversedBounds(lower={Lower},upper={Upper})", lower, upper);
    }
}
