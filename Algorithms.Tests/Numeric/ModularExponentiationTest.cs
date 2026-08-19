using Algorithms.Numeric;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric;

public class ModularExponentiationTest
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(3, 6, 11, 3)]
    [TestCase(5, 3, 13, 8)]
    [TestCase(2, 7, 17, 9)]
    [TestCase(7, 4, 16, 1)]
    [TestCase(7, 2, 11, 5)]
    [TestCase(4, 13, 497, 445)]
    [TestCase(13, 3, 1, 0)]
    public void ModularExponentiationCorrect(int b, int e, int m, int expectedRes)
    {
		_logger.LogDebug(">>> Entering ModularExponentiationCorrect(b={B},e={E},m={M},expectedRes={ExpectedRes})", b, e, m, expectedRes);
        var modularExponentiation = new ModularExponentiation();
        var actualRes = modularExponentiation.ModularPow(b, e, m);
		_logger.LogDebug("ModularExponentiationCorrect(b={B},e={E},m={M},expectedRes={ExpectedRes}): actualRes → {ActualRes}", b, e, m, expectedRes, actualRes);
		_logger.LogDebug("ModularExponentiationCorrect(b={B},e={E},m={M},expectedRes={ExpectedRes}): modularExponentiation → {ModularExponentiation}", b, e, m, expectedRes, modularExponentiation);
        actualRes.Should().Be(expectedRes);
		_logger.LogDebug("<<< Exiting ModularExponentiationCorrect(b={B},e={E},m={M},expectedRes={ExpectedRes})", b, e, m, expectedRes);
    }

    [TestCase(17, 7, -3)]
    [TestCase(11, 3, -5)]
    [TestCase(14, 3, 0)]
    public void ModularExponentiationNegativeMod(int b, int e, int m)
    {
		_logger.LogDebug(">>> Entering ModularExponentiationNegativeMod(b={B},e={E},m={M})", b, e, m);
        var modularExponentiation = new ModularExponentiation();
        Action res = () => modularExponentiation.ModularPow(b, e, m);
		_logger.LogDebug("ModularExponentiationNegativeMod(b={B},e={E},m={M}): res → {Res}", b, e, m, res);
        res.Should().Throw<ArgumentException>()
        .WithMessage(String.Format("{0} is not a positive integer", m));
		_logger.LogDebug("ModularExponentiationNegativeMod(b={B},e={E},m={M}): modularExponentiation → {ModularExponentiation}", b, e, m, modularExponentiation);
		_logger.LogDebug("<<< Exiting ModularExponentiationNegativeMod(b={B},e={E},m={M})", b, e, m);
    }
}
