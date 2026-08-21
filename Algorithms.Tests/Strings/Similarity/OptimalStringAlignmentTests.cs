using Algorithms.Strings.Similarity;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Strings.Similarity
{
    [TestFixture]
    public class OptimalStringAlignmentTests
    {
		private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        [Test]
        public void Calculate_IdenticalStrings_ReturnsZero()
        {
			_logger.LogDebug(">>> Entering Calculate_IdenticalStrings_ReturnsZero()");
            var result = OptimalStringAlignment.Calculate("example", "example");
			_logger.LogDebug("<<< Exiting Calculate_IdenticalStrings_ReturnsZero()");
			_logger.LogDebug("Calculate_IdenticalStrings_ReturnsZero(): result → {Result}", result);
            result.Should().Be(0.0);
        }

        [Test]
        public void Calculate_FirstStringEmpty_ReturnsLengthOfSecondString()
        {
			_logger.LogDebug(">>> Entering Calculate_FirstStringEmpty_ReturnsLengthOfSecondString()");
            var result = OptimalStringAlignment.Calculate("", "example");
			_logger.LogDebug("<<< Exiting Calculate_FirstStringEmpty_ReturnsLengthOfSecondString()");
			_logger.LogDebug("Calculate_FirstStringEmpty_ReturnsLengthOfSecondString(): result → {Result}", result);
            result.Should().Be("example".Length);
        }

        [Test]
        public void Calculate_SecondStringEmpty_ReturnsLengthOfFirstString()
        {
			_logger.LogDebug(">>> Entering Calculate_SecondStringEmpty_ReturnsLengthOfFirstString()");
            var result = OptimalStringAlignment.Calculate("example", "");
			_logger.LogDebug("<<< Exiting Calculate_SecondStringEmpty_ReturnsLengthOfFirstString()");
			_logger.LogDebug("Calculate_SecondStringEmpty_ReturnsLengthOfFirstString(): result → {Result}", result);
            result.Should().Be("example".Length);
        }

        [Test]
        public void Calculate_BothStringsEmpty_ReturnsZero()
        {
			_logger.LogDebug(">>> Entering Calculate_BothStringsEmpty_ReturnsZero()");
            var result = OptimalStringAlignment.Calculate("", "");
			_logger.LogDebug("<<< Exiting Calculate_BothStringsEmpty_ReturnsZero()");
			_logger.LogDebug("Calculate_BothStringsEmpty_ReturnsZero(): result → {Result}", result);
            result.Should().Be(0.0);
        }

        [Test]
        public void Calculate_OneInsertion_ReturnsOne()
        {
			_logger.LogDebug(">>> Entering Calculate_OneInsertion_ReturnsOne()");
            var result = OptimalStringAlignment.Calculate("example", "examples");
			_logger.LogDebug("<<< Exiting Calculate_OneInsertion_ReturnsOne()");
			_logger.LogDebug("Calculate_OneInsertion_ReturnsOne(): result → {Result}", result);
            result.Should().Be(1.0);
        }

        [Test]
        public void Calculate_OneDeletion_ReturnsOne()
        {
			_logger.LogDebug(">>> Entering Calculate_OneDeletion_ReturnsOne()");
            var result = OptimalStringAlignment.Calculate("examples", "example");
			_logger.LogDebug("<<< Exiting Calculate_OneDeletion_ReturnsOne()");
			_logger.LogDebug("Calculate_OneDeletion_ReturnsOne(): result → {Result}", result);
            result.Should().Be(1.0);
        }

        [Test]
        public void Calculate_OneSubstitution_ReturnsOne()
        {
			_logger.LogDebug(">>> Entering Calculate_OneSubstitution_ReturnsOne()");
            var result = OptimalStringAlignment.Calculate("example", "exbmple");
			_logger.LogDebug("<<< Exiting Calculate_OneSubstitution_ReturnsOne()");
			_logger.LogDebug("Calculate_OneSubstitution_ReturnsOne(): result → {Result}", result);
            result.Should().Be(1.0);
        }

        [Test]
        public void Calculate_OneTransposition_ReturnsOne()
        {
			_logger.LogDebug(">>> Entering Calculate_OneTransposition_ReturnsOne()");
            var result = OptimalStringAlignment.Calculate("example", "exmaple");
			_logger.LogDebug("<<< Exiting Calculate_OneTransposition_ReturnsOne()");
			_logger.LogDebug("Calculate_OneTransposition_ReturnsOne(): result → {Result}", result);
            result.Should().Be(1.0);
        }

        [Test]
        public void Calculate_MultipleOperations_ReturnsCorrectDistance()
        {
			_logger.LogDebug(">>> Entering Calculate_MultipleOperations_ReturnsCorrectDistance()");
            var result = OptimalStringAlignment.Calculate("kitten", "sitting");
			_logger.LogDebug("<<< Exiting Calculate_MultipleOperations_ReturnsCorrectDistance()");
			_logger.LogDebug("Calculate_MultipleOperations_ReturnsCorrectDistance(): result → {Result}", result);
            result.Should().Be(3.0);
        }
    }
}
