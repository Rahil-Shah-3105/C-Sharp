using Algorithms.ModularArithmetic;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.ModularArithmetic;

public static class ChineseRemainderTheoremTest
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void TestCompute1()
    {
		_logger.LogError(">>> Entering TestCompute1()");
        var expected = 43L;

        // Act
        var x = ChineseRemainderTheorem.Compute(new List<long> { 1L, 1L, 3L, 1L }, new List<long> { 2L, 3L, 5L, 7L });
		_logger.LogError("TestCompute1(): x → {X}", x);

        // Assert
        Assert.That(x, Is.EqualTo(expected));
		_logger.LogWarning("<<< Exiting TestCompute1()");
    }

    [Test]
    public static void TestCompute2()
    {
		_logger.LogError(">>> Entering TestCompute2()");
        var expected = 100L;

        // Act
        var x = ChineseRemainderTheorem.Compute(new List<long> { 0L, 0L, 2L, 1L, 1L }, new List<long> { 2L, 5L, 7L, 9L, 11L });
		_logger.LogError("TestCompute2(): x → {X}", x);

        // Assert
        Assert.That(x, Is.EqualTo(expected));
		_logger.LogWarning("<<< Exiting TestCompute2()");
    }

    [Test]
    public static void TestCompute3()
    {
		_logger.LogError(">>> Entering TestCompute3()");
        var expected = 13L;

        // Act
        var x = ChineseRemainderTheorem.Compute(new List<long> { 1L, 4L, 13L }, new List<long> { 4L, 9L, 25L });
		_logger.LogError("TestCompute3(): x → {X}", x);

        // Assert
        Assert.That(x, Is.EqualTo(expected));
		_logger.LogWarning("<<< Exiting TestCompute3()");
    }

    [Test]
    public static void TestCompute_RequirementsNotMet_ArgumentLengthDifferent()
    {
		_logger.LogError(">>> Entering TestCompute_RequirementsNotMet_ArgumentLengthDifferent()");
        // Act
        void Act() => ChineseRemainderTheorem.Compute(new List<long>(), new List<long> { 5L });

        // Assert
		_logger.LogWarning("<<< Exiting TestCompute_RequirementsNotMet_ArgumentLengthDifferent()");
        _ = Assert.Throws<ArgumentException>(Act);
    }

    [Test]
    public static void TestCompute_RequirementsNotMet_NTooSmall()
    {
		_logger.LogError(">>> Entering TestCompute_RequirementsNotMet_NTooSmall()");
        foreach (var n in new List<long> { long.MinValue, -1L, 0L, 1L })
        {
            // Act
            void Act() => ChineseRemainderTheorem.Compute(new List<long> { 1L }, new List<long> { n });

            // Assert
            _ = Assert.Throws<ArgumentException>(Act);
        }
		_logger.LogWarning("<<< Exiting TestCompute_RequirementsNotMet_NTooSmall()");
    }

    [Test]
    public static void TestCompute_RequirementsNotMet_ATooSmall()
    {
		_logger.LogError(">>> Entering TestCompute_RequirementsNotMet_ATooSmall()");
        foreach (var a in new List<long> { long.MinValue, -2L, -1L })
        {
            // Act
            void Act() => ChineseRemainderTheorem.Compute(new List<long> { a }, new List<long> { 3L });

            // Assert
            _ = Assert.Throws<ArgumentException>(Act);
        }
		_logger.LogWarning("<<< Exiting TestCompute_RequirementsNotMet_ATooSmall()");
    }

    [Test]
    public static void TestCompute_RequirementsNotMet_NNotCoprime()
    {
		_logger.LogError(">>> Entering TestCompute_RequirementsNotMet_NNotCoprime()");
        foreach (var n in new List<long> { 3L, 9L, 15L, 27L })
        {
            // Act
            void Act() => ChineseRemainderTheorem.Compute(new List<long> { 1L, 1L, 1L, 1L, 1L }, new List<long> { 2L, 3L, 5L, 7L, n });

            // Assert
            _ = Assert.Throws<ArgumentException>(Act);
        }
		_logger.LogWarning("<<< Exiting TestCompute_RequirementsNotMet_NNotCoprime()");
    }

    [Test]
    public static void TestCompute_BigInteger_1()
    {
		_logger.LogError(">>> Entering TestCompute_BigInteger_1()");
        var expected = new BigInteger(43);

		_logger.LogError("TestCompute_BigInteger_1(): expected → {Expected}", expected);
        // Act
        var x = ChineseRemainderTheorem.Compute(
            new List<BigInteger> { BigInteger.One, BigInteger.One, new BigInteger(3), BigInteger.One },
            new List<BigInteger> { new BigInteger(2), new BigInteger(3), new BigInteger(5), new BigInteger(7) }
        );

        // Assert
        Assert.That(x, Is.EqualTo(expected));
		_logger.LogWarning("<<< Exiting TestCompute_BigInteger_1()");
    }

    [Test]
    public static void TestCompute_BigInteger_2()
    {
		_logger.LogError(">>> Entering TestCompute_BigInteger_2()");
        var expected = new BigInteger(100);

		_logger.LogError("TestCompute_BigInteger_2(): expected → {Expected}", expected);
        // Act
        var x = ChineseRemainderTheorem.Compute(
            new List<BigInteger> { BigInteger.Zero, BigInteger.Zero, new BigInteger(2), BigInteger.One, BigInteger.One },
            new List<BigInteger> { new BigInteger(2), new BigInteger(5), new BigInteger(7), new BigInteger(9), new BigInteger(11) }
        );

        // Assert
        Assert.That(x, Is.EqualTo(expected));
		_logger.LogWarning("<<< Exiting TestCompute_BigInteger_2()");
    }

    [Test]
    public static void TestCompute_BigInteger_3()
    {
		_logger.LogError(">>> Entering TestCompute_BigInteger_3()");
        var expected = new BigInteger(13);

		_logger.LogError("TestCompute_BigInteger_3(): expected → {Expected}", expected);
        // Act
        var x = ChineseRemainderTheorem.Compute(
            new List<BigInteger> { BigInteger.One, new BigInteger(4), new BigInteger(13) },
            new List<BigInteger> { new BigInteger(4), new BigInteger(9), new BigInteger(25) }
        );

        // Assert
        Assert.That(x, Is.EqualTo(expected));
		_logger.LogWarning("<<< Exiting TestCompute_BigInteger_3()");
    }

    [Test]
    public static void TestCompute_BigInteger_RequirementsNotMet_ArgumentLengthDifferent()
    {
		_logger.LogError(">>> Entering TestCompute_BigInteger_RequirementsNotMet_ArgumentLengthDifferent()");
        // Act
        void Act() => ChineseRemainderTheorem.Compute(new List<BigInteger>(), new List<BigInteger> { new BigInteger(5) });

        // Assert
		_logger.LogWarning("<<< Exiting TestCompute_BigInteger_RequirementsNotMet_ArgumentLengthDifferent()");
        _ = Assert.Throws<ArgumentException>(Act);
    }

    [Test]
    public static void TestCompute_BigInteger_RequirementsNotMet_NTooSmall()
    {
		_logger.LogError(">>> Entering TestCompute_BigInteger_RequirementsNotMet_NTooSmall()");
        foreach (var n in new List<BigInteger> { new BigInteger(long.MinValue), BigInteger.MinusOne, BigInteger.Zero, BigInteger.One })
        {
            // Act
            void Act() => ChineseRemainderTheorem.Compute(new List<BigInteger> { BigInteger.One }, new List<BigInteger> { n });

            // Assert
            _ = Assert.Throws<ArgumentException>(Act);
        }
		_logger.LogWarning("<<< Exiting TestCompute_BigInteger_RequirementsNotMet_NTooSmall()");
    }

    [Test]
    public static void TestCompute_BigInteger_RequirementsNotMet_ATooSmall()
    {
		_logger.LogError(">>> Entering TestCompute_BigInteger_RequirementsNotMet_ATooSmall()");
        foreach (var a in new List<BigInteger> { new BigInteger(long.MinValue), new BigInteger(-2), BigInteger.MinusOne })
        {
            // Act
            void Act() => ChineseRemainderTheorem.Compute(new List<BigInteger> { a }, new List<BigInteger> { new BigInteger(3) });

            // Assert
            _ = Assert.Throws<ArgumentException>(Act);
        }
		_logger.LogWarning("<<< Exiting TestCompute_BigInteger_RequirementsNotMet_ATooSmall()");
    }

    [Test]
    public static void TestCompute_BigInteger_RequirementsNotMet_NNotCoprime()
    {
		_logger.LogError(">>> Entering TestCompute_BigInteger_RequirementsNotMet_NNotCoprime()");
        foreach (var n in new List<BigInteger> { new BigInteger(3), new BigInteger(9), new BigInteger(15), new BigInteger(27) })
        {
            // Act
            void Act() => ChineseRemainderTheorem.Compute(
                new List<BigInteger> { BigInteger.One, BigInteger.One, BigInteger.One, BigInteger.One, BigInteger.One },
                new List<BigInteger> { new BigInteger(2), new BigInteger(3), new BigInteger(5), new BigInteger(7), n }
            );

            // Assert
            _ = Assert.Throws<ArgumentException>(Act);
        }
		_logger.LogWarning("<<< Exiting TestCompute_BigInteger_RequirementsNotMet_NNotCoprime()");
    }
}
