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
		_logger.LogError(">>> Entering TestIndexer()");
        // Arrange
        var testObj = new BitArray(5);
		_logger.LogError("TestIndexer(): testObj → {TestObj}", testObj);

        // Act
        testObj.Compile(24);

        // Assert
        Assert.That(testObj[0], Is.True);
        Assert.That(testObj[1], Is.True);
        Assert.That(testObj[3], Is.False);
		_logger.LogWarning("<<< Exiting TestIndexer()");
    }

    [TestCase(19, 3)]
    public static void TestNumberOfOneBits(int number, int expected)
    {
		_logger.LogError(">>> Entering TestNumberOfOneBits(number={Number},expected={Expected})", number, expected);
        // Arrange
        var testObj = new BitArray(5);
		_logger.LogError("TestNumberOfOneBits(number={Number},expected={Expected}): testObj → {TestObj}", number, expected, testObj);

        // Act
        testObj.Compile(number);

        // Assert
        Assert.That(expected, Is.EqualTo(testObj.NumberOfOneBits()));
		_logger.LogWarning("<<< Exiting TestNumberOfOneBits(number={Number},expected={Expected})", number, expected);
    }

    [TestCase(26, 2)]
    public static void TestNumberOfZeroBits(int number, int expected)
    {
		_logger.LogError(">>> Entering TestNumberOfZeroBits(number={Number},expected={Expected})", number, expected);
        // Arrange
        var testObj = new BitArray(5);
		_logger.LogError("TestNumberOfZeroBits(number={Number},expected={Expected}): testObj → {TestObj}", number, expected, testObj);

        // Act
        testObj.Compile(number);

        // Assert
        Assert.That(expected, Is.EqualTo(testObj.NumberOfZeroBits()));
		_logger.LogWarning("<<< Exiting TestNumberOfZeroBits(number={Number},expected={Expected})", number, expected);
    }

    [TestCase(33, 33)]
    public static void TestToInt64(int number, int expected)
    {
		_logger.LogError(">>> Entering TestToInt64(number={Number},expected={Expected})", number, expected);
        // Arrange
        var testObj = new BitArray(6);
		_logger.LogError("TestToInt64(number={Number},expected={Expected}): testObj → {TestObj}", number, expected, testObj);

        // Act
        testObj.Compile(number);

        // Assert
        Assert.That(expected, Is.EqualTo(testObj.ToInt64()));
		_logger.LogWarning("<<< Exiting TestToInt64(number={Number},expected={Expected})", number, expected);
    }

    [Test]
    public static void TestToInt32MaxValue()
    {
		_logger.LogError(">>> Entering TestToInt32MaxValue()");
        // Arrange
        var testObj = new BitArray(33);
		_logger.LogError("TestToInt32MaxValue(): testObj → {TestObj}", testObj);

        // Act

        // Assert
		_logger.LogWarning("<<< Exiting TestToInt32MaxValue()");
        _ = Assert.Throws<InvalidOperationException>(() => testObj.ToInt32());
    }

    [Test]
    public static void TestToInt64MaxValue()
    {
		_logger.LogError(">>> Entering TestToInt64MaxValue()");
        // Arrange
        var testObj = new BitArray(65);
		_logger.LogError("TestToInt64MaxValue(): testObj → {TestObj}", testObj);

        // Act

        // Assert
		_logger.LogWarning("<<< Exiting TestToInt64MaxValue()");
        _ = Assert.Throws<InvalidOperationException>(() => testObj.ToInt64());
    }

    [TestCase("110")]
    public static void TestResetField(string sequence)
    {
		_logger.LogError(">>> Entering TestResetField(sequence={Sequence})", sequence);
        // Arrange
        var testObj = new BitArray(sequence);
		_logger.LogError("TestResetField(sequence={Sequence}): testObj → {TestObj}", sequence, testObj);

        // Act
        testObj.ResetField();

        // Assert
        Assert.That(0, Is.EqualTo(testObj.ToInt64()));
		_logger.LogWarning("<<< Exiting TestResetField(sequence={Sequence})", sequence);
    }

    [TestCase("101001", 63)]
    public static void TestSetAll(string sequence, int expected)
    {
		_logger.LogError(">>> Entering TestSetAll(sequence={Sequence},expected={Expected})", sequence, expected);
        // Arrange
        var testObj = new BitArray(sequence);
		_logger.LogError("TestSetAll(sequence={Sequence},expected={Expected}): testObj → {TestObj}", sequence, expected, testObj);

        // Act
        testObj.SetAll(true);

        // Assert
        Assert.That(expected, Is.EqualTo(testObj.ToInt64()));
		_logger.LogWarning("<<< Exiting TestSetAll(sequence={Sequence},expected={Expected})", sequence, expected);
    }

    [Test]
    public static void TestCloneEquals()
    {
		_logger.LogError(">>> Entering TestCloneEquals()");
        // Arrange
        var testObj1 = new BitArray("110");
		_logger.LogError("TestCloneEquals(): testObj1 → {TestObj1}", testObj1);

        // Act
        var testObj2 = (BitArray)testObj1.Clone();
		_logger.LogError("TestCloneEquals(): testObj2 → {TestObj2}", testObj2);

        // Assert
        Assert.That(testObj1.Equals(testObj2), Is.True);
		_logger.LogWarning("<<< Exiting TestCloneEquals()");
    }

    [Test]
    public static void TestCloneNotEquals()
    {
		_logger.LogError(">>> Entering TestCloneNotEquals()");
        // Arrange
        var testObj1 = new BitArray("101");
		_logger.LogError("TestCloneNotEquals(): testObj1 → {TestObj1}", testObj1);
        var testObj2 = new BitArray(15);
		_logger.LogError("TestCloneNotEquals(): testObj2 → {TestObj2}", testObj2);
        var testObj3 = new BitArray(3);
		_logger.LogError("TestCloneNotEquals(): testObj3 → {TestObj3}", testObj3);

        // Act
        testObj3.Reset();

        // Assert
        testObj1.Equals(testObj2).Should().BeFalse();
        testObj1.Equals(testObj3).Should().BeFalse();
		_logger.LogWarning("<<< Exiting TestCloneNotEquals()");
    }

    [Test]
    public static void TestHasCode()
    {
		_logger.LogError(">>> Entering TestHasCode()");
        // Arrange
        const int num = 5;
        var testObj = new BitArray(3);
		_logger.LogError("TestHasCode(): testObj → {TestObj}", testObj);

        // Act
        testObj.Compile(num);
        var result = testObj.GetHashCode();
		_logger.LogError("TestHasCode(): result → {Result}", result);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(5, Is.EqualTo(result));
		_logger.LogWarning("<<< Exiting TestHasCode()");
    }

    [Test]
    public static void TestMoveNextCurrent()
    {
		_logger.LogError(">>> Entering TestMoveNextCurrent()");
        var testObj1 = new BitArray("1111010");

		_logger.LogError("TestMoveNextCurrent(): testObj1 → {TestObj1}", testObj1);
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
		_logger.LogWarning("<<< Exiting TestMoveNextCurrent()");
    }

    [Test]
    public static void IEnumerable_IterationWorks()
    {
		_logger.LogError(">>> Entering IEnumerable_IterationWorks()");
        var arr = new BitArray("010101010101010101");
        var current = 0;
		_logger.LogError("IEnumerable_IterationWorks(): arr → {Arr}", arr);
        foreach (var b in arr)
        {
            b.Should().Be(arr[current]);
            current++;
        }
		_logger.LogWarning("<<< Exiting IEnumerable_IterationWorks()");
    }

    [Test]
    public static void Equals_NullIsNotEqualToNotNull()
    {
		_logger.LogError(">>> Entering Equals_NullIsNotEqualToNotNull()");
        var arr1 = new BitArray("010101010101010101");
        BitArray? arr2 = null;
		_logger.LogError("Equals_NullIsNotEqualToNotNull(): arr1 → {Arr1}", arr1);
        arr1.Equals(arr2).Should().BeFalse();
		_logger.LogWarning("<<< Exiting Equals_NullIsNotEqualToNotNull()");
    }

    #region COMPILE TESTS

    [TestCase("00100", "00100")]
    [TestCase("01101", "01101")]
    [TestCase("100", "00100")]
    public static void TestCompileToString(string sequence, string expectedSequence)
    {
		_logger.LogError(">>> Entering TestCompileToString(sequence={Sequence},expectedSequence={ExpectedSequence})", sequence, expectedSequence);
        // Arrange
        var testObj = new BitArray(5);
		_logger.LogError("TestCompileToString(sequence={Sequence},expectedSequence={ExpectedSequence}): testObj → {TestObj}", sequence, expectedSequence, testObj);

        // Act
        testObj.Compile(sequence);

        // Assert
        Assert.That(expectedSequence, Is.EqualTo(testObj.ToString()));
		_logger.LogWarning("<<< Exiting TestCompileToString(sequence={Sequence},expectedSequence={ExpectedSequence})", sequence, expectedSequence);
    }

    [TestCase("klgml", 5)]
    [TestCase("klgml", 3)]
    public static void TestCompileToStringThorwsException(string sequence, int arrLen)
    {
		_logger.LogError(">>> Entering TestCompileToStringThorwsException(sequence={Sequence},arrLen={ArrLen})", sequence, arrLen);
        // Arrange
        var testObj = new BitArray(arrLen);
		_logger.LogError("TestCompileToStringThorwsException(sequence={Sequence},arrLen={ArrLen}): testObj → {TestObj}", sequence, arrLen, testObj);

        // Act
        void Act() => testObj.Compile(sequence);

        // Assert
        Assert.Throws<ArgumentException>(Act);
		_logger.LogWarning("<<< Exiting TestCompileToStringThorwsException(sequence={Sequence},arrLen={ArrLen})", sequence, arrLen);
    }

    [TestCase(15, "01111")]
    [TestCase(17, "10001")]
    [TestCase(4, "00100")]
    public static void TestCompileLong(int number, string expected)
    {
		_logger.LogError(">>> Entering TestCompileLong(number={Number},expected={Expected})", number, expected);
        // Arrange
        var testObj = new BitArray(5);
		_logger.LogError("TestCompileLong(number={Number},expected={Expected}): testObj → {TestObj}", number, expected, testObj);

        // Act
        testObj.Compile((long)number);

        // Assert
        Assert.That(expected, Is.EqualTo(testObj.ToString()));
		_logger.LogWarning("<<< Exiting TestCompileLong(number={Number},expected={Expected})", number, expected);
    }

    [TestCase(46, 3)]
    [TestCase(-46, 5)]
    public static void TestCompileLongThrowsException(int number, int arrLen)
    {
		_logger.LogError(">>> Entering TestCompileLongThrowsException(number={Number},arrLen={ArrLen})", number, arrLen);
        // Arrange
        var testObj = new BitArray(arrLen);
		_logger.LogError("TestCompileLongThrowsException(number={Number},arrLen={ArrLen}): testObj → {TestObj}", number, arrLen, testObj);

        // Act
        void Act() => testObj.Compile((long)number);

        // Assert
        Assert.Throws<ArgumentException>(Act);
		_logger.LogWarning("<<< Exiting TestCompileLongThrowsException(number={Number},arrLen={ArrLen})", number, arrLen);
    }

    [TestCase(17, "10001")]
    [TestCase(25, "11001")]
    [TestCase(4, "00100")]
    public static void TestCompileInteger(int number, string expected)
    {
		_logger.LogError(">>> Entering TestCompileInteger(number={Number},expected={Expected})", number, expected);
        // Arrange
        var testObj = new BitArray(5);
		_logger.LogError("TestCompileInteger(number={Number},expected={Expected}): testObj → {TestObj}", number, expected, testObj);

        // Act
        testObj.Compile(number);

        // Assert
        Assert.That(expected, Is.EqualTo(testObj.ToString()));
		_logger.LogWarning("<<< Exiting TestCompileInteger(number={Number},expected={Expected})", number, expected);
    }

    [TestCase(-8, 5)]
    [TestCase(18, 3)]
    public static void TestCompileIntegerThrowsException(int number, int arrayLength)
    {
		_logger.LogError(">>> Entering TestCompileIntegerThrowsException(number={Number},arrayLength={ArrayLength})", number, arrayLength);
        // Arrange
        var testObj = new BitArray(arrayLength);
		_logger.LogError("TestCompileIntegerThrowsException(number={Number},arrayLength={ArrayLength}): testObj → {TestObj}", number, arrayLength, testObj);

        // Act
        void Act() => testObj.Compile(number);

        // Assert
        Assert.Throws<ArgumentException>(Act);
		_logger.LogWarning("<<< Exiting TestCompileIntegerThrowsException(number={Number},arrayLength={ArrayLength})", number, arrayLength);
    }

    #endregion COMPILE TESTS

    #region CONSTRUCTOR TESTS

    [TestCase("00100", 4)]
    public static void TestConstructor(string sequence, int expected)
    {
		_logger.LogError(">>> Entering TestConstructor(sequence={Sequence},expected={Expected})", sequence, expected);
        // Arrange
        var testObj1 = new BitArray(sequence);
		_logger.LogError("TestConstructor(sequence={Sequence},expected={Expected}): testObj1 → {TestObj1}", sequence, expected, testObj1);

        // Act

        // Assert
        Assert.That(expected, Is.EqualTo(testObj1.ToInt64()));
		_logger.LogWarning("<<< Exiting TestConstructor(sequence={Sequence},expected={Expected})", sequence, expected);
    }

    [TestCase(new[] { true, false, true }, 5)]
    public static void TestConstructorBoolArray(bool[] sequence, int expected)
    {
		_logger.LogError(">>> Entering TestConstructorBoolArray(sequence={Sequence},expected={Expected})", sequence, expected);
        // Arrange
        var testObj3 = new BitArray(sequence);
		_logger.LogError("TestConstructorBoolArray(sequence={Sequence},expected={Expected}): testObj3 → {TestObj3}", sequence, expected, testObj3);

        // Act

        // Assert
        Assert.That(expected, Is.EqualTo(testObj3.ToInt64()));
		_logger.LogWarning("<<< Exiting TestConstructorBoolArray(sequence={Sequence},expected={Expected})", sequence, expected);
    }

    [TestCase("000120")]
    [TestCase("")]
    public static void TestConstructorThrowsException(string sequence)
    {
		_logger.LogError(">>> Entering TestConstructorThrowsException(sequence={Sequence})", sequence);
        // Arrange

        // Act
        Action act = () => new BitArray(sequence);
		_logger.LogError("TestConstructorThrowsException(sequence={Sequence}): act → {Act}", sequence, act);

        // Assert
        act.Should().Throw<ArgumentException>();
		_logger.LogWarning("<<< Exiting TestConstructorThrowsException(sequence={Sequence})", sequence);
    }

    #endregion CONSTRUCTOR TESTS

    #region OPERATOR TESTS

    [TestCase(17, 17, "10001")]
    [TestCase(25, 31, "11001")]
    public static void TestOperatorAnd(int tObj1, int tObj2, string expected)
    {
		_logger.LogError(">>> Entering TestOperatorAnd(tObj1={TObj1},tObj2={TObj2},expected={Expected})", tObj1, tObj2, expected);
        // Arrange
        var testObj1 = new BitArray(5);
		_logger.LogError("TestOperatorAnd(tObj1={TObj1},tObj2={TObj2},expected={Expected}): testObj1 → {TestObj1}", tObj1, tObj2, expected, testObj1);
        var testObj2 = new BitArray(5);
		_logger.LogError("TestOperatorAnd(tObj1={TObj1},tObj2={TObj2},expected={Expected}): testObj2 → {TestObj2}", tObj1, tObj2, expected, testObj2);

        // Act
        testObj1.Compile(tObj1);
        testObj2.Compile(tObj2);

        var result = testObj1 & testObj2;
		_logger.LogError("TestOperatorAnd(tObj1={TObj1},tObj2={TObj2},expected={Expected}): result → {Result}", tObj1, tObj2, expected, result);

        // Assert
        Assert.That(expected, Is.EqualTo(result.ToString()));
		_logger.LogWarning("<<< Exiting TestOperatorAnd(tObj1={TObj1},tObj2={TObj2},expected={Expected})", tObj1, tObj2, expected);
    }

    [TestCase(1, 1, 1, 1, "0")]
    [TestCase(5, 3, 8, 4, "1101")]
    [TestCase(9, 4, 4, 3, "1101")]
    public static void TestOperatorXorAndDiffSizes(int t1, int s1, int t2, int s2, string expected)
    {
		_logger.LogError(">>> Entering TestOperatorXorAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected})", t1, s1, t2, s2, expected);
        // Arrange
        var testObj1 = new BitArray(s1);
		_logger.LogError("TestOperatorXorAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected}): testObj1 → {TestObj1}", t1, s1, t2, s2, expected, testObj1);
        var testObj2 = new BitArray(s2);
		_logger.LogError("TestOperatorXorAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected}): testObj2 → {TestObj2}", t1, s1, t2, s2, expected, testObj2);

        // Act
        testObj1.Compile(t1);
        testObj2.Compile(t2);
        var result = testObj1 ^ testObj2;
		_logger.LogError("TestOperatorXorAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected}): result → {Result}", t1, s1, t2, s2, expected, result);

        // Assert
        Assert.That(expected, Is.EqualTo(result.ToString()));
		_logger.LogWarning("<<< Exiting TestOperatorXorAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected})", t1, s1, t2, s2, expected);
    }

    [TestCase(9, 4, 4, 3, "1101")]
    [TestCase(1, 1, 1, 1, "1")]
    [TestCase(5, 3, 8, 4, "1101")]
    public static void TestOperatorOrAndDiffSizes(int t1, int s1, int t2, int s2, string expected)
    {
		_logger.LogError(">>> Entering TestOperatorOrAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected})", t1, s1, t2, s2, expected);
        // Arrange
        var testObj1 = new BitArray(s1);
		_logger.LogError("TestOperatorOrAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected}): testObj1 → {TestObj1}", t1, s1, t2, s2, expected, testObj1);
        var testObj2 = new BitArray(s2);
		_logger.LogError("TestOperatorOrAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected}): testObj2 → {TestObj2}", t1, s1, t2, s2, expected, testObj2);

        // Act
        testObj1.Compile(t1);
        testObj2.Compile(t2);
        var result = testObj1 | testObj2;
		_logger.LogError("TestOperatorOrAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected}): result → {Result}", t1, s1, t2, s2, expected, result);

        // Assert
        Assert.That(expected, Is.EqualTo(result.ToString()));
		_logger.LogWarning("<<< Exiting TestOperatorOrAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected})", t1, s1, t2, s2, expected);
    }

    [TestCase(1, 1, 1, 1, "1")]
    [TestCase(5, 3, 8, 4, "0000")]
    [TestCase(9, 4, 4, 3, "0000")]
    public static void TestOperatorAndAndDiffSizes(int t1, int s1, int t2, int s2, string expected)
    {
		_logger.LogError(">>> Entering TestOperatorAndAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected})", t1, s1, t2, s2, expected);
        // Arrange
        var testObj1 = new BitArray(s1);
		_logger.LogError("TestOperatorAndAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected}): testObj1 → {TestObj1}", t1, s1, t2, s2, expected, testObj1);
        var testObj2 = new BitArray(s2);
		_logger.LogError("TestOperatorAndAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected}): testObj2 → {TestObj2}", t1, s1, t2, s2, expected, testObj2);

        // Act
        testObj1.Compile(t1);
        testObj2.Compile(t2);
        var result = testObj1 & testObj2;
		_logger.LogError("TestOperatorAndAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected}): result → {Result}", t1, s1, t2, s2, expected, result);

        // Assert
        Assert.That(expected, Is.EqualTo(result.ToString()));
		_logger.LogWarning("<<< Exiting TestOperatorAndAndDiffSizes(t1={T1},s1={S1},t2={T2},s2={S2},expected={Expected})", t1, s1, t2, s2, expected);
    }

    [TestCase(25, 30, "11111")]
    public static void TestOperatorOr(int tObj1, int tObj2, string expected)
    {
		_logger.LogError(">>> Entering TestOperatorOr(tObj1={TObj1},tObj2={TObj2},expected={Expected})", tObj1, tObj2, expected);
        // Arrange
        var testObj1 = new BitArray(5);
		_logger.LogError("TestOperatorOr(tObj1={TObj1},tObj2={TObj2},expected={Expected}): testObj1 → {TestObj1}", tObj1, tObj2, expected, testObj1);
        var testObj2 = new BitArray(5);
		_logger.LogError("TestOperatorOr(tObj1={TObj1},tObj2={TObj2},expected={Expected}): testObj2 → {TestObj2}", tObj1, tObj2, expected, testObj2);

        // Act
        testObj1.Compile(tObj1);
        testObj2.Compile(tObj2);

        var result = testObj1 | testObj2;
		_logger.LogError("TestOperatorOr(tObj1={TObj1},tObj2={TObj2},expected={Expected}): result → {Result}", tObj1, tObj2, expected, result);

        // Assert
        Assert.That(expected, Is.EqualTo(result.ToString()));
		_logger.LogWarning("<<< Exiting TestOperatorOr(tObj1={TObj1},tObj2={TObj2},expected={Expected})", tObj1, tObj2, expected);
    }

    [TestCase(16, "01111")]
    public static void TestOperatorNot(int number, string expected)
    {
		_logger.LogError(">>> Entering TestOperatorNot(number={Number},expected={Expected})", number, expected);
        // Arrange
        var testObj = new BitArray(5);
		_logger.LogError("TestOperatorNot(number={Number},expected={Expected}): testObj → {TestObj}", number, expected, testObj);

        // Act
        testObj.Compile(number);
        testObj = ~testObj;

        // Assert
        Assert.That(expected, Is.EqualTo(testObj.ToString()));
		_logger.LogWarning("<<< Exiting TestOperatorNot(number={Number},expected={Expected})", number, expected);
    }

    [TestCase(25, 30, 7)]
    public static void TestOperatorXor(int testNum, int testNum2, int expected)
    {
		_logger.LogError(">>> Entering TestOperatorXor(testNum={TestNum},testNum2={TestNum2},expected={Expected})", testNum, testNum2, expected);
        // Arrange
        var testObj1 = new BitArray(5);
		_logger.LogError("TestOperatorXor(testNum={TestNum},testNum2={TestNum2},expected={Expected}): testObj1 → {TestObj1}", testNum, testNum2, expected, testObj1);
        var testObj2 = new BitArray(5);
		_logger.LogError("TestOperatorXor(testNum={TestNum},testNum2={TestNum2},expected={Expected}): testObj2 → {TestObj2}", testNum, testNum2, expected, testObj2);

        // Act
        testObj1.Compile(testNum);
        testObj2.Compile(testNum2);

        var result = testObj1 ^ testObj2;
		_logger.LogError("TestOperatorXor(testNum={TestNum},testNum2={TestNum2},expected={Expected}): result → {Result}", testNum, testNum2, expected, result);

        // Assert
        Assert.That(expected, Is.EqualTo(result.ToInt32()));
		_logger.LogWarning("<<< Exiting TestOperatorXor(testNum={TestNum},testNum2={TestNum2},expected={Expected})", testNum, testNum2, expected);
    }

    [TestCase(16, "10000000")]
    public static void TestOperatorShiftLeft(int number, string expected)
    {
		_logger.LogError(">>> Entering TestOperatorShiftLeft(number={Number},expected={Expected})", number, expected);
        // Arrange
        var testObj = new BitArray(5);
		_logger.LogError("TestOperatorShiftLeft(number={Number},expected={Expected}): testObj → {TestObj}", number, expected, testObj);

        // Act
        testObj.Compile(number);
        testObj <<= 3;

        // Assert
        Assert.That(expected, Is.EqualTo(testObj.ToString()));
		_logger.LogWarning("<<< Exiting TestOperatorShiftLeft(number={Number},expected={Expected})", number, expected);
    }

    [TestCase(24, "110")]
    public static void TestOperatorShiftRight(int number, string expected)
    {
		_logger.LogError(">>> Entering TestOperatorShiftRight(number={Number},expected={Expected})", number, expected);
        // Arrange
        var testObj = new BitArray(5);
		_logger.LogError("TestOperatorShiftRight(number={Number},expected={Expected}): testObj → {TestObj}", number, expected, testObj);

        // Act
        testObj.Compile(number);
        testObj >>= 2;

        // Assert
        Assert.That(expected, Is.EqualTo(testObj.ToString()));
		_logger.LogWarning("<<< Exiting TestOperatorShiftRight(number={Number},expected={Expected})", number, expected);
    }

    #endregion OPERATOR TESTS

    #region COMPARE TESTS

    [Test]
    public static void TestParity()
    {
		_logger.LogError(">>> Entering TestParity()");
        // Arrange
        var testObj = new BitArray(5);
		_logger.LogError("TestParity(): testObj → {TestObj}", testObj);

        // Act
        testObj.Compile(26);

        // Assert
        Assert.That(testObj.EvenParity(), Is.False);
        Assert.That(testObj.OddParity(), Is.True);
		_logger.LogWarning("<<< Exiting TestParity()");
    }

    [Test]
    public static void TestCompare()
    {
		_logger.LogError(">>> Entering TestCompare()");
        // Arrange
        var testObj1 = new BitArray("110");
		_logger.LogError("TestCompare(): testObj1 → {TestObj1}", testObj1);
        var testObj2 = new BitArray("110");
		_logger.LogError("TestCompare(): testObj2 → {TestObj2}", testObj2);
        var testObj3 = new BitArray("100");
		_logger.LogError("TestCompare(): testObj3 → {TestObj3}", testObj3);

        // Act

        // Assert
        Assert.That(testObj1 == testObj2, Is.True);
        Assert.That(testObj1 != testObj3, Is.True);
		_logger.LogWarning("<<< Exiting TestCompare()");
    }

    [Test]
    public static void ArraysOfDifferentLengthsAreNotEqual()
    {
		_logger.LogError(">>> Entering ArraysOfDifferentLengthsAreNotEqual()");
        // Arrange
        var testObj1 = new BitArray("110");
		_logger.LogError("ArraysOfDifferentLengthsAreNotEqual(): testObj1 → {TestObj1}", testObj1);
        var testObj2 = new BitArray("10101");
		_logger.LogError("ArraysOfDifferentLengthsAreNotEqual(): testObj2 → {TestObj2}", testObj2);

        // Act

        // Assert
        Assert.That(testObj1 == testObj2, Is.False);
		_logger.LogWarning("<<< Exiting ArraysOfDifferentLengthsAreNotEqual()");
    }

    #endregion COMPARE TESTS
}
