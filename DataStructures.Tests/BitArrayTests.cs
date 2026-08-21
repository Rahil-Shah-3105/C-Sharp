using Microsoft.Extensions.Logging;
namespace DataStructures.Tests;

/// <summary>
///     This class contains some tests for the class BitArray.
/// </summary>
public static class BitArrayTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void TestIndexer()
    {
		_logger.LogDebug(">>> Entering TestIndexer()");
        // Arrange
        var testObj = new BitArray(5);
		_logger.LogDebug("TestIndexer(): testObj → {TestObj}", testObj);

        // Act
        testObj.Compile(24);

        // Assert
        Assert.That(testObj[0], Is.True);
        Assert.That(testObj[1], Is.True);
        Assert.That(testObj[3], Is.False);
		_logger.LogDebug("<<< Exiting TestIndexer()");
    }

    [TestCase(19, 3)]
    public static void TestNumberOfOneBits(int number, int expected)
    {
		_logger.LogDebug(">>> Entering TestNumberOfOneBits(number={Number},expected={Expected})", number, expected);
        // Arrange
        var testObj = new BitArray(5);
		_logger.LogDebug("TestNumberOfOneBits(number={Number},expected={Expected}): testObj → {TestObj}", number, expected, testObj);

        // Act
        testObj.Compile(number);

        // Assert
        Assert.That(expected, Is.EqualTo(testObj.NumberOfOneBits()));
		_logger.LogDebug("<<< Exiting TestNumberOfOneBits(number={Number},expected={Expected})", number, expected);
    }

    [TestCase(26, 2)]
    public static void TestNumberOfZeroBits(int number, int expected)
    {
		_logger.LogDebug(">>> Entering TestNumberOfZeroBits(number={Number},expected={Expected})", number, expected);
        // Arrange
        var testObj = new BitArray(5);
		_logger.LogDebug("TestNumberOfZeroBits(number={Number},expected={Expected}): testObj → {TestObj}", number, expected, testObj);

        // Act
        testObj.Compile(number);

        // Assert
        Assert.That(expected, Is.EqualTo(testObj.NumberOfZeroBits()));
		_logger.LogDebug("<<< Exiting TestNumberOfZeroBits(number={Number},expected={Expected})", number, expected);
    }

    [TestCase(33, 33)]
    public static void TestToInt64(int number, int expected)
    {
		_logger.LogDebug(">>> Entering TestToInt64(number={Number},expected={Expected})", number, expected);
        // Arrange
        var testObj = new BitArray(6);
		_logger.LogDebug("TestToInt64(number={Number},expected={Expected}): testObj → {TestObj}", number, expected, testObj);

        // Act
        testObj.Compile(number);

        // Assert
        Assert.That(expected, Is.EqualTo(testObj.ToInt64()));
		_logger.LogDebug("<<< Exiting TestToInt64(number={Number},expected={Expected})", number, expected);
    }

    [Test]
    public static void TestToInt32MaxValue()
    {
		_logger.LogDebug(">>> Entering TestToInt32MaxValue()");
        // Arrange
        var testObj = new BitArray(33);
		_logger.LogDebug("TestToInt32MaxValue(): testObj → {TestObj}", testObj);

        // Act

        // Assert
		_logger.LogDebug("<<< Exiting TestToInt32MaxValue()");
        _ = Assert.Throws<InvalidOperationException>(() => testObj.ToInt32());
    }

    [Test]
    public static void TestToInt64MaxValue()
    {
		_logger.LogDebug(">>> Entering TestToInt64MaxValue()");
        // Arrange
        var testObj = new BitArray(65);
		_logger.LogDebug("TestToInt64MaxValue(): testObj → {TestObj}", testObj);

        // Act

        // Assert
		_logger.LogDebug("<<< Exiting TestToInt64MaxValue()");
        _ = Assert.Throws<InvalidOperationException>(() => testObj.ToInt64());
    }

    [TestCase("110")]
    public static void TestResetField(string sequence)
    {
		_logger.LogDebug(">>> Entering TestResetField(sequence={Sequence})", sequence);
        // Arrange
        var testObj = new BitArray(sequence);
		_logger.LogDebug("TestResetField(sequence={Sequence}): testObj → {TestObj}", sequence, testObj);

        // Act
        testObj.ResetField();

        // Assert
        Assert.That(0, Is.EqualTo(testObj.ToInt64()));
		_logger.LogDebug("<<< Exiting TestResetField(sequence={Sequence})", sequence);
    }

    [TestCase("101001", 63)]
    public static void TestSetAll(string sequence, int expected)
    {
		_logger.LogDebug(">>> Entering TestSetAll(sequence={Sequence},expected={Expected})", sequence, expected);
        // Arrange
        var testObj = new BitArray(sequence);
		_logger.LogDebug("TestSetAll(sequence={Sequence},expected={Expected}): testObj → {TestObj}", sequence, expected, testObj);

        // Act
        testObj.SetAll(true);

        // Assert
        Assert.That(expected, Is.EqualTo(testObj.ToInt64()));
		_logger.LogDebug("<<< Exiting TestSetAll(sequence={Sequence},expected={Expected})", sequence, expected);
    }

    [Test]
    public static void TestCloneEquals()
    {
		_logger.LogDebug(">>> Entering TestCloneEquals()");
        // Arrange
        var testObj1 = new BitArray("110");
		_logger.LogDebug("TestCloneEquals(): testObj1 → {TestObj1}", testObj1);

        // Act
        var testObj2 = (BitArray)testObj1.Clone();
		_logger.LogDebug("TestCloneEquals(): testObj2 → {TestObj2}", testObj2);

        // Assert
        Assert.That(testObj1.Equals(testObj2), Is.True);
		_logger.LogDebug("<<< Exiting TestCloneEquals()");
    }

    [Test]
    public static void TestCloneNotEquals()
    {
		_logger.LogDebug(">>> Entering TestCloneNotEquals()");
        // Arrange
        var testObj1 = new BitArray("101");
		_logger.LogDebug("TestCloneNotEquals(): testObj1 → {TestObj1}", testObj1);
        var testObj2 = new BitArray(15);
		_logger.LogDebug("TestCloneNotEquals(): testObj2 → {TestObj2}", testObj2);
        var testObj3 = new BitArray(3);
		_logger.LogDebug("TestCloneNotEquals(): testObj3 → {TestObj3}", testObj3);

        // Act
        testObj3.Reset();

        // Assert
        testObj1.Equals(testObj2).Should().BeFalse();
        testObj1.Equals(testObj3).Should().BeFalse();
		_logger.LogDebug("<<< Exiting TestCloneNotEquals()");
    }

    [Test]
    public static void TestHasCode()
    {
		_logger.LogDebug(">>> Entering TestHasCode()");
        // Arrange
        const int num = 5;
        var testObj = new BitArray(3);
		_logger.LogDebug("TestHasCode(): testObj → {TestObj}", testObj);

        // Act
        testObj.Compile(num);
        var result = testObj.GetHashCode();
		_logger.LogDebug("TestHasCode(): result → {Result}", result);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(5, Is.EqualTo(result));
		_logger.LogDebug("<<< Exiting TestHasCode()");
    }

    [Test]
    public static void TestMoveNextCurrent()
    {
		_logger.LogDebug(">>> Entering TestMoveNextCurrent()");
        var testObj1 = new BitArray("1111010");

		_logger.LogDebug("TestMoveNextCurrent(): testObj1 → {TestObj1}", testObj1);
        var counterOnes = 0;
        var counterZeros = 0;

        foreach (var bit in testObj1)
        {
            if (bit)
            {
                counterOnes++;
            }
            else
            {
                counterZeros++;
            }
        }

        Assert.That(counterOnes, Is.EqualTo(5));
        Assert.That(counterZeros, Is.EqualTo(2));
		_logger.LogDebug("<<< Exiting TestMoveNextCurrent()");
    }

    [Test]
    public static void IEnumerable_IterationWorks()
    {
		_logger.LogDebug(">>> Entering IEnumerable_IterationWorks()");
        var arr = new BitArray("010101010101010101");
        var current = 0;
		_logger.LogDebug("IEnumerable_IterationWorks(): arr → {Arr}", arr);
        foreach (var b in arr)
        {
            b.Should().Be(arr[current]);
            current++;
        }
		_logger.LogDebug("<<< Exiting IEnumerable_IterationWorks()");
    }

    [Test]
    public static void Equals_NullIsNotEqualToNotNull()
    {
		_logger.LogDebug(">>> Entering Equals_NullIsNotEqualToNotNull()");
        var arr1 = new BitArray("010101010101010101");
        BitArray? arr2 = null;
		_logger.LogDebug("Equals_NullIsNotEqualToNotNull(): arr1 → {Arr1}", arr1);
        arr1.Equals(arr2).Should().BeFalse();
		_logger.LogDebug("<<< Exiting Equals_NullIsNotEqualToNotNull()");
    }

    #region COMPILE TESTS

    [TestCase("00100", "00100")]
    [TestCase("01101", "01101")]
    [TestCase("100", "00100")]
    public static void TestCompileToString(string sequence, string expectedSequence)
    {
		_logger.LogDebug(">>> Entering TestCompileToString(sequence={Sequence},expectedSequence={ExpectedSequence})", sequence, expectedSequence);
        // Arrange
        var testObj = new BitArray(5);
		_logger.LogDebug("TestCompileToString(sequence={Sequence},expectedSequence={ExpectedSequence}): testObj → {TestObj}", sequence, expectedSequence, testObj);

        // Act
        testObj.Compile(sequence);

        // Assert
        Assert.That(expectedSequence, Is.EqualTo(testObj.ToString()));
		_logger.LogDebug("<<< Exiting TestCompileToString(sequence={Sequence},expectedSequence={ExpectedSequence})", sequence, expectedSequence);
    }

    [TestCase("klgml", 5)]
    [TestCase("klgml", 3)]
    public static void TestCompileToStringThorwsException(string sequence, int arrLen)
    {
		_logger.LogDebug(">>> Entering TestCompileToStringThorwsException(sequence={Sequence},arrLen={ArrLen})", sequence, arrLen);
        // Arrange
        var testObj = new BitArray(arrLen);
		_logger.LogDebug("TestCompileToStringThorwsException(sequence={Sequence},arrLen={ArrLen}): testObj → {TestObj}", sequence, arrLen, testObj);

        // Act
        void Act() => testObj.Compile(sequence);

        // Assert
        Assert.Throws<ArgumentException>(Act);
		_logger.LogDebug("<<< Exiting TestCompileToStringThorwsException(sequence={Sequence},arrLen={ArrLen})", sequence, arrLen);
    }

    [TestCase(15, "01111")]
    [TestCase(17, "10001")]
    [TestCase(4, "00100")]
    public static void TestCompileLong(int number, string expected)
    {
		_logger.LogDebug(">>> Entering TestCompileLong(number={Number},expected={Expected})", number, expected);
        // Arrange
        var testObj = new BitArray(5);
		_logger.LogDebug("TestCompileLong(number={Number},expected={Expected}): testObj → {TestObj}", number, expected, testObj);

        // Act
        testObj.Compile((long)number);

        // Assert
        Assert.That(expected, Is.EqualTo(testObj.ToString()));
		_logger.LogDebug("<<< Exiting TestCompileLong(number={Number},expected={Expected})", number, expected);
    }

    [TestCase(46, 3)]
    [TestCase(-46, 5)]
    public static void TestCompileLongThrowsException(int number, int arrLen)
    {
		_logger.LogDebug(">>> Entering TestCompileLongThrowsException(number={Number},arrLen={ArrLen})", number, arrLen);
        // Arrange
        var testObj = new BitArray(arrLen);
		_logger.LogDebug("TestCompileLongThrowsException(number={Number},arrLen={ArrLen}): testObj → {TestObj}", number, arrLen, testObj);

        // Act
        void Act() => testObj.Compile((long)number);

        // Assert
        Assert.Throws<ArgumentException>(Act);
		_logger.LogDebug("<<< Exiting TestCompileLongThrowsException(number={Number},arrLen={ArrLen})", number, arrLen);
    }

    [TestCase(17, "10001")]
    [TestCase(25, "11001")]
    [TestCase(4, "00100")]
    public static void TestCompileInteger(int number, string expected)
    {
		_logger.LogDebug(">>> Entering TestCompileInteger(number={Number},expected={Expected})", number, expected);
        // Arrange
        var testObj = new BitArray(5);
		_logger.LogDebug("TestCompileInteger(number={Number},expected={Expected}): testObj → {TestObj}", number, expected, testObj);

        // Act
        testObj.Compile(number);

        // Assert
        Assert.That(expected, Is.EqualTo(testObj.ToString()));
		_logger.LogDebug("<<< Exiting TestCompileInteger(number={Number},expected={Expected})", number, expected);
    }

    [TestCase(-8, 5)]
    [TestCase(18, 3)]
    public static void TestCompileIntegerThrowsException(int number, int arrayLength)
    {
		_logger.LogDebug(">>> Entering TestCompileIntegerThrowsException(number={Number},arrayLength={ArrayLength})", number, arrayLength);
        // Arrange
        var testObj = new BitArray(arrayLength);
		_logger.LogDebug("TestCompileIntegerThrowsException(number={Number},arrayLength={ArrayLength}): testObj → {TestObj}", number, arrayLength, testObj);

        // Act
        void Act() => testObj.Compile(number);

        // Assert
        Assert.Throws<ArgumentException>(Act);
		_logger.LogDebug("<<< Exiting TestCompileIntegerThrowsException(number={Number},arrayLength={ArrayLength})", number, arrayLength);
    }

    #endregion COMPILE TESTS

    #region CONSTRUCTOR TESTS

    [TestCase("00100", 4)]
    public static void TestConstructor(string sequence, int expected)
    {
		_logger.LogDebug(">>> Entering TestConstructor(sequence={Sequence},expected={Expected})", sequence, expected);
        // Arrange
        var testObj1 = new BitArray(sequence);
		_logger.LogDebug("TestConstructor(sequence={Sequence},expected={Expected}): testObj1 → {TestObj1}", sequence, expected, testObj1);

        // Act

        // Assert
        Assert.That(expected, Is.EqualTo(testObj1.ToInt64()));
		_logger.LogDebug("<<< Exiting TestConstructor(sequence={Sequence},expected={Expected})", sequence, expected);
    }

    [TestCase(new[] { true, false, true }, 5)]
    public static void TestConstructorBoolArray(bool[] sequence, int expected)
    {
		_logger.LogDebug(">>> Entering TestConstructorBoolArray(sequence={Sequence},expected={Expected})", sequence, expected);
        // Arrange
        var testObj3 = new BitArray(sequence);
		_logger.LogDebug("TestConstructorBoolArray(sequence={Sequence},expected={Expected}): testObj3 → {TestObj3}", sequence, expected, testObj3);

        // Act

        // Assert
        Assert.That(expected, Is.EqualTo(testObj3.ToInt64()));
		_logger.LogDebug("<<< Exiting TestConstructorBoolArray(sequence={Sequence},expected={Expected})", sequence, expected);
    }

    [TestCase("000120")]
    [TestCase("")]
    public static void TestConstructorThrowsException(string sequence)
    {
		_logger.LogDebug(">>> Entering TestConstructorThrowsException(sequence={Sequence})", sequence);
        // Arrange

        // Act
        Action act = () => new BitArray(sequence);
		_logger.LogDebug("TestConstructorThrowsException(sequence={Sequence}): act → {Act}", sequence, act);

        // Assert
        act.Should().Throw<ArgumentException>();
		_logger.LogDebug("<<< Exiting TestConstructorThrowsException(sequence={Sequence})", sequence);
    }

    #endregion CONSTRUCTOR TESTS

    #region OPERATOR TESTS

    [TestCase(17, 17, "10001")]
    [TestCase(25, 31, "11001")]
    public static void TestOperatorAnd(int tObj1, int tObj2, string expected)
    {
		_logger.LogDebug(">>> Entering TestOperatorAnd(tObj1={TObj1},tObj2={TObj2},expected={Expected})", tObj1, tObj2, expected);
        // Arrange
        var testObj1 = new BitArray(5);
		_logger.LogDebug("TestOperatorAnd(tObj1={TObj1},tObj2={TObj2},expected={Expected}): testObj1 → {TestObj1}", tObj1, tObj2, expected, testObj1);
        var testObj2 = new BitArray(5);
		_logger.LogDebug("TestOperatorAnd(tObj1={TObj1},tObj2={TObj2},expected={Expected}): testObj2 → {TestObj2}", tObj1, tObj2, expected, testObj2);

        // Act
        testObj1.Compile(tObj1);
        testObj2.Compile(tObj2);

        var result = testObj1 & testObj2;
		_logger.LogDebug("TestOperatorAnd(tObj1={TObj1},tObj2={TObj2},expected={Expected}): result → {Result}", tObj1, tObj2, expected, result);

        // Assert
        Assert.That(expected, Is.EqualTo(result.ToString()));
		_logger.LogDebug("<<< Exiting TestOperatorAnd(tObj1={TObj1},tObj2={TObj2},expected={Expected})", tObj1, tObj2, expected);
    }

    [TestCase(1, 1, 1, 1, "0")]
    [TestCase(5, 3, 8, 4, "1101")]
    [TestCase(9, 4, 4, 3, "1101")]
    public static void TestOperatorXorAndDiffSizes(int t1, int s1, int t2, int s2, string expected)
    {
		_logger.LogDebug(">>> Entering TestOperatorXorAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected})", t1, s1, t2, s2, expected);
        // Arrange
        var testObj1 = new BitArray(s1);
		_logger.LogDebug("TestOperatorXorAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected}): testObj1 → {TestObj1}", t1, s1, t2, s2, expected, testObj1);
        var testObj2 = new BitArray(s2);
		_logger.LogDebug("TestOperatorXorAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected}): testObj2 → {TestObj2}", t1, s1, t2, s2, expected, testObj2);

        // Act
        testObj1.Compile(t1);
        testObj2.Compile(t2);
        var result = testObj1 ^ testObj2;
		_logger.LogDebug("TestOperatorXorAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected}): result → {Result}", t1, s1, t2, s2, expected, result);

        // Assert
        Assert.That(expected, Is.EqualTo(result.ToString()));
		_logger.LogDebug("<<< Exiting TestOperatorXorAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected})", t1, s1, t2, s2, expected);
    }

    [TestCase(9, 4, 4, 3, "1101")]
    [TestCase(1, 1, 1, 1, "1")]
    [TestCase(5, 3, 8, 4, "1101")]
    public static void TestOperatorOrAndDiffSizes(int t1, int s1, int t2, int s2, string expected)
    {
		_logger.LogDebug(">>> Entering TestOperatorOrAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected})", t1, s1, t2, s2, expected);
        // Arrange
        var testObj1 = new BitArray(s1);
		_logger.LogDebug("TestOperatorOrAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected}): testObj1 → {TestObj1}", t1, s1, t2, s2, expected, testObj1);
        var testObj2 = new BitArray(s2);
		_logger.LogDebug("TestOperatorOrAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected}): testObj2 → {TestObj2}", t1, s1, t2, s2, expected, testObj2);

        // Act
        testObj1.Compile(t1);
        testObj2.Compile(t2);
        var result = testObj1 | testObj2;
		_logger.LogDebug("TestOperatorOrAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected}): result → {Result}", t1, s1, t2, s2, expected, result);

        // Assert
        Assert.That(expected, Is.EqualTo(result.ToString()));
		_logger.LogDebug("<<< Exiting TestOperatorOrAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected})", t1, s1, t2, s2, expected);
    }

    [TestCase(1, 1, 1, 1, "1")]
    [TestCase(5, 3, 8, 4, "0000")]
    [TestCase(9, 4, 4, 3, "0000")]
    public static void TestOperatorAndAndDiffSizes(int t1, int s1, int t2, int s2, string expected)
    {
		_logger.LogDebug(">>> Entering TestOperatorAndAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected})", t1, s1, t2, s2, expected);
        // Arrange
        var testObj1 = new BitArray(s1);
		_logger.LogDebug("TestOperatorAndAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected}): testObj1 → {TestObj1}", t1, s1, t2, s2, expected, testObj1);
        var testObj2 = new BitArray(s2);
		_logger.LogDebug("TestOperatorAndAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected}): testObj2 → {TestObj2}", t1, s1, t2, s2, expected, testObj2);

        // Act
        testObj1.Compile(t1);
        testObj2.Compile(t2);
        var result = testObj1 & testObj2;
		_logger.LogDebug("TestOperatorAndAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected}): result → {Result}", t1, s1, t2, s2, expected, result);

        // Assert
        Assert.That(expected, Is.EqualTo(result.ToString()));
		_logger.LogDebug("<<< Exiting TestOperatorAndAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected})", t1, s1, t2, s2, expected);
    }

    [TestCase(25, 30, "11111")]
    public static void TestOperatorOr(int tObj1, int tObj2, string expected)
    {
		_logger.LogDebug(">>> Entering TestOperatorOr(tObj1={TObj1},tObj2={TObj2},expected={Expected})", tObj1, tObj2, expected);
        // Arrange
        var testObj1 = new BitArray(5);
		_logger.LogDebug("TestOperatorOr(tObj1={TObj1},tObj2={TObj2},expected={Expected}): testObj1 → {TestObj1}", tObj1, tObj2, expected, testObj1);
        var testObj2 = new BitArray(5);
		_logger.LogDebug("TestOperatorOr(tObj1={TObj1},tObj2={TObj2},expected={Expected}): testObj2 → {TestObj2}", tObj1, tObj2, expected, testObj2);

        // Act
        testObj1.Compile(tObj1);
        testObj2.Compile(tObj2);

        var result = testObj1 | testObj2;
		_logger.LogDebug("TestOperatorOr(tObj1={TObj1},tObj2={TObj2},expected={Expected}): result → {Result}", tObj1, tObj2, expected, result);

        // Assert
        Assert.That(expected, Is.EqualTo(result.ToString()));
		_logger.LogDebug("<<< Exiting TestOperatorOr(tObj1={TObj1},tObj2={TObj2},expected={Expected})", tObj1, tObj2, expected);
    }

    [TestCase(16, "01111")]
    public static void TestOperatorNot(int number, string expected)
    {
		_logger.LogDebug(">>> Entering TestOperatorNot(number={Number},expected={Expected})", number, expected);
        // Arrange
        var testObj = new BitArray(5);
		_logger.LogDebug("TestOperatorNot(number={Number},expected={Expected}): testObj → {TestObj}", number, expected, testObj);

        // Act
        testObj.Compile(number);
        testObj = ~testObj;

        // Assert
        Assert.That(expected, Is.EqualTo(testObj.ToString()));
		_logger.LogDebug("<<< Exiting TestOperatorNot(number={Number},expected={Expected})", number, expected);
    }

    [TestCase(25, 30, 7)]
    public static void TestOperatorXor(int testNum, int testNum2, int expected)
    {
		_logger.LogDebug(">>> Entering TestOperatorXor(testNum={TestNum},testNum2={TestNum2},expected={Expected})", testNum, testNum2, expected);
        // Arrange
        var testObj1 = new BitArray(5);
		_logger.LogDebug("TestOperatorXor(testNum={TestNum},testNum2={TestNum2},expected={Expected}): testObj1 → {TestObj1}", testNum, testNum2, expected, testObj1);
        var testObj2 = new BitArray(5);
		_logger.LogDebug("TestOperatorXor(testNum={TestNum},testNum2={TestNum2},expected={Expected}): testObj2 → {TestObj2}", testNum, testNum2, expected, testObj2);

        // Act
        testObj1.Compile(testNum);
        testObj2.Compile(testNum2);

        var result = testObj1 ^ testObj2;
		_logger.LogDebug("TestOperatorXor(testNum={TestNum},testNum2={TestNum2},expected={Expected}): result → {Result}", testNum, testNum2, expected, result);

        // Assert
        Assert.That(expected, Is.EqualTo(result.ToInt32()));
		_logger.LogDebug("<<< Exiting TestOperatorXor(testNum={TestNum},testNum2={TestNum2},expected={Expected})", testNum, testNum2, expected);
    }

    [TestCase(16, "10000000")]
    public static void TestOperatorShiftLeft(int number, string expected)
    {
		_logger.LogDebug(">>> Entering TestOperatorShiftLeft(number={Number},expected={Expected})", number, expected);
        // Arrange
        var testObj = new BitArray(5);
		_logger.LogDebug("TestOperatorShiftLeft(number={Number},expected={Expected}): testObj → {TestObj}", number, expected, testObj);

        // Act
        testObj.Compile(number);
        testObj <<= 3;

        // Assert
        Assert.That(expected, Is.EqualTo(testObj.ToString()));
		_logger.LogDebug("<<< Exiting TestOperatorShiftLeft(number={Number},expected={Expected})", number, expected);
    }

    [TestCase(24, "110")]
    public static void TestOperatorShiftRight(int number, string expected)
    {
		_logger.LogDebug(">>> Entering TestOperatorShiftRight(number={Number},expected={Expected})", number, expected);
        // Arrange
        var testObj = new BitArray(5);
		_logger.LogDebug("TestOperatorShiftRight(number={Number},expected={Expected}): testObj → {TestObj}", number, expected, testObj);

        // Act
        testObj.Compile(number);
        testObj >>= 2;

        // Assert
        Assert.That(expected, Is.EqualTo(testObj.ToString()));
		_logger.LogDebug("<<< Exiting TestOperatorShiftRight(number={Number},expected={Expected})", number, expected);
    }

    #endregion OPERATOR TESTS

    #region COMPARE TESTS

    [Test]
    public static void TestParity()
    {
		_logger.LogDebug(">>> Entering TestParity()");
        // Arrange
        var testObj = new BitArray(5);
		_logger.LogDebug("TestParity(): testObj → {TestObj}", testObj);

        // Act
        testObj.Compile(26);

        // Assert
        Assert.That(testObj.EvenParity(), Is.False);
        Assert.That(testObj.OddParity(), Is.True);
		_logger.LogDebug("<<< Exiting TestParity()");
    }

    [Test]
    public static void TestCompare()
    {
		_logger.LogDebug(">>> Entering TestCompare()");
        // Arrange
        var testObj1 = new BitArray("110");
		_logger.LogDebug("TestCompare(): testObj1 → {TestObj1}", testObj1);
        var testObj2 = new BitArray("110");
		_logger.LogDebug("TestCompare(): testObj2 → {TestObj2}", testObj2);
        var testObj3 = new BitArray("100");
		_logger.LogDebug("TestCompare(): testObj3 → {TestObj3}", testObj3);

        // Act

        // Assert
        Assert.That(testObj1 == testObj2, Is.True);
        Assert.That(testObj1 != testObj3, Is.True);
		_logger.LogDebug("<<< Exiting TestCompare()");
    }

    [Test]
    public static void ArraysOfDifferentLengthsAreNotEqual()
    {
		_logger.LogDebug(">>> Entering ArraysOfDifferentLengthsAreNotEqual()");
        // Arrange
        var testObj1 = new BitArray("110");
		_logger.LogDebug("ArraysOfDifferentLengthsAreNotEqual(): testObj1 → {TestObj1}", testObj1);
        var testObj2 = new BitArray("10101");
		_logger.LogDebug("ArraysOfDifferentLengthsAreNotEqual(): testObj2 → {TestObj2}", testObj2);

        // Act

        // Assert
		_logger.LogDebug("<<< Exiting ArraysOfDifferentLengthsAreNotEqual()");
        Assert.That(testObj1 == testObj2, Is.False);
    }

    #endregion COMPARE TESTS
}
