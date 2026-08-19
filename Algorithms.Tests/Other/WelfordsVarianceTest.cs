using Algorithms.Other;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Other;

public class WelfordsVarianceTest
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void WelfordVariance_Example1()
    {
		_logger.LogDebug(">>> Entering WelfordVariance_Example1()");
        var welfordsVariance = new WelfordsVariance();
        welfordsVariance.AddValue(4);
		_logger.LogDebug("WelfordVariance_Example1(): welfordsVariance → {WelfordsVariance}", welfordsVariance);
        welfordsVariance.AddValue(7);
        welfordsVariance.AddValue(13);
        welfordsVariance.AddValue(16);

        Assert.That(welfordsVariance.Count, Is.EqualTo(4));
        Assert.That(welfordsVariance.Mean, Is.EqualTo(10).Within(0.0000001));
        Assert.That(welfordsVariance.Variance, Is.EqualTo(22.5).Within(0.0000001));
        Assert.That(welfordsVariance.SampleVariance, Is.EqualTo(30).Within(0.0000001));
		_logger.LogDebug("<<< Exiting WelfordVariance_Example1()");
    }

    [Test]
    public void WelfordVariance_Example2()
    {
		_logger.LogDebug(">>> Entering WelfordVariance_Example2()");
        var stats = new WelfordsVariance();
        stats.AddValue(100000004);
		_logger.LogDebug("WelfordVariance_Example2(): stats → {Stats}", stats);
        stats.AddValue(100000007);
        stats.AddValue(100000013);
        stats.AddValue(100000016);
        Assert.That(stats.Count, Is.EqualTo(4));
        Assert.That(stats.Mean, Is.EqualTo(100000010).Within(0.0000001));
        Assert.That(stats.Variance, Is.EqualTo(22.5).Within(0.0000001));
        Assert.That(stats.SampleVariance, Is.EqualTo(30).Within(0.0000001));
		_logger.LogDebug("<<< Exiting WelfordVariance_Example2()");
    }

    [Test]
    public void WelfordVariance_Example3()
    {
		_logger.LogDebug(">>> Entering WelfordVariance_Example3()");
        var stats = new WelfordsVariance();
        stats.AddValue(1000000004);
		_logger.LogDebug("WelfordVariance_Example3(): stats → {Stats}", stats);
        stats.AddValue(1000000007);
        stats.AddValue(1000000013);
        stats.AddValue(1000000016);
        Assert.That(stats.Count, Is.EqualTo(4));
        Assert.That(stats.Mean, Is.EqualTo(1000000010).Within(0.0000001));
        Assert.That(stats.Variance, Is.EqualTo(22.5).Within(0.0000001));
        Assert.That(stats.SampleVariance, Is.EqualTo(30).Within(0.0000001));
		_logger.LogDebug("<<< Exiting WelfordVariance_Example3()");
    }

    [Test]
    public void WelfordVariance_Example4()
    {
		_logger.LogDebug(">>> Entering WelfordVariance_Example4()");
        var stats = new WelfordsVariance();
        stats.AddValue(6);
		_logger.LogDebug("WelfordVariance_Example4(): stats → {Stats}", stats);
        stats.AddValue(2);
        stats.AddValue(3);
        stats.AddValue(1);
        Assert.That(stats.Count, Is.EqualTo(4));
        Assert.That(stats.Mean, Is.EqualTo(3).Within(0.0000001));
        Assert.That(stats.Variance, Is.EqualTo(3.5).Within(0.0000001));
        Assert.That(stats.SampleVariance, Is.EqualTo(4.6666667).Within(0.0000001));
		_logger.LogDebug("<<< Exiting WelfordVariance_Example4()");
    }

    [Test]
    public void WelfordVariance_Example5()
    {
		_logger.LogDebug(">>> Entering WelfordVariance_Example5()");
        var stats = new WelfordsVariance([2, 2, 5, 7]);
        Assert.That(stats.Count, Is.EqualTo(4));
		_logger.LogDebug("WelfordVariance_Example5(): stats → {Stats}", stats);
        Assert.That(stats.Mean, Is.EqualTo(4).Within(0.0000001));
        Assert.That(stats.Variance, Is.EqualTo(4.5).Within(0.0000001));
        Assert.That(stats.SampleVariance, Is.EqualTo(6).Within(0.0000001));
		_logger.LogDebug("<<< Exiting WelfordVariance_Example5()");
    }

    [Test]
    public void WelfordVariance_Example6()
    {
		_logger.LogDebug(">>> Entering WelfordVariance_Example6()");
        var stats = new WelfordsVariance();
        stats.AddRange([2, 4, 4, 4, 5, 5, 7, 9]);
		_logger.LogDebug("WelfordVariance_Example6(): stats → {Stats}", stats);
        Assert.That(stats.Count, Is.EqualTo(8));
        Assert.That(stats.Mean, Is.EqualTo(5).Within(0.0000001));
        Assert.That(stats.Variance, Is.EqualTo(4).Within(0.0000001));
        Assert.That(stats.SampleVariance, Is.EqualTo(4.5714286).Within(0.0000001));
		_logger.LogDebug("<<< Exiting WelfordVariance_Example6()");
    }

    [Test]
    public void WelfordVariance_Example7()
    {
		_logger.LogDebug(">>> Entering WelfordVariance_Example7()");
        var stats = new WelfordsVariance();
        stats.AddRange([9, 2, 5, 4, 12, 7, 8, 11, 9, 3, 7, 4, 12, 5, 4, 10, 9, 6, 9, 4]);
		_logger.LogDebug("WelfordVariance_Example7(): stats → {Stats}", stats);
        Assert.That(stats.Count, Is.EqualTo(20));
        Assert.That(stats.Mean, Is.EqualTo(7).Within(0.0000001));
        Assert.That(stats.Variance, Is.EqualTo(8.9).Within(0.0000001));
        Assert.That(stats.SampleVariance, Is.EqualTo(9.3684211).Within(0.0000001));
		_logger.LogDebug("<<< Exiting WelfordVariance_Example7()");
    }

    [Test]
    public void WelfordVariance_Example8()
    {
		_logger.LogDebug(">>> Entering WelfordVariance_Example8()");
        var stats = new WelfordsVariance();
        stats.AddRange([51.3, 55.6, 49.9, 52.0]);
		_logger.LogDebug("WelfordVariance_Example8(): stats → {Stats}", stats);
        Assert.That(stats.Count, Is.EqualTo(4));
        Assert.That(stats.Mean, Is.EqualTo(52.2).Within(0.0000001));
        Assert.That(stats.Variance, Is.EqualTo(4.4250000).Within(0.0000001));
        Assert.That(stats.SampleVariance, Is.EqualTo(5.9000000).Within(0.0000001));
		_logger.LogDebug("<<< Exiting WelfordVariance_Example8()");
    }

    [Test]
    public void WelfordVariance_Example9()
    {
		_logger.LogDebug(">>> Entering WelfordVariance_Example9()");
        var stats = new WelfordsVariance();
        stats.AddRange([-5, -3, -1, 1, 3]);
		_logger.LogDebug("WelfordVariance_Example9(): stats → {Stats}", stats);
        Assert.That(stats.Count, Is.EqualTo(5));
        Assert.That(stats.Mean, Is.EqualTo(-1).Within(0.0000001));
        Assert.That(stats.Variance, Is.EqualTo(8).Within(0.0000001));
        Assert.That(stats.SampleVariance, Is.EqualTo(10).Within(0.0000001));
		_logger.LogDebug("<<< Exiting WelfordVariance_Example9()");
    }

    [Test]
    public void WelfordVariance_Example10()
    {
		_logger.LogDebug(">>> Entering WelfordVariance_Example10()");
        var stats = new WelfordsVariance();
        stats.AddRange([-1, 0, 1]);
		_logger.LogDebug("WelfordVariance_Example10(): stats → {Stats}", stats);
        Assert.That(stats.Count, Is.EqualTo(3));
        Assert.That(stats.Mean, Is.EqualTo(0).Within(0.0000001));
        Assert.That(stats.Variance, Is.EqualTo(0.6666667).Within(0.0000001));
        Assert.That(stats.SampleVariance, Is.EqualTo(1).Within(0.0000001));
		_logger.LogDebug("<<< Exiting WelfordVariance_Example10()");
    }

    [Test]
    public void WelfordVariance_NoValue()
    {
		_logger.LogDebug(">>> Entering WelfordVariance_NoValue()");
        var stats = new WelfordsVariance();
        Assert.That(stats.Count, Is.EqualTo(0));
		_logger.LogDebug("WelfordVariance_NoValue(): stats → {Stats}", stats);
        Assert.That(stats.Mean, Is.EqualTo(double.NaN));
        Assert.That(stats.Variance, Is.EqualTo(double.NaN));
        Assert.That(stats.SampleVariance, Is.EqualTo(double.NaN));
		_logger.LogDebug("<<< Exiting WelfordVariance_NoValue()");
    }

    [Test]
    public void WelfordVariance_OneValue()
    {
		_logger.LogDebug(">>> Entering WelfordVariance_OneValue()");
        var stats = new WelfordsVariance();
        stats.AddValue(1);
		_logger.LogDebug("WelfordVariance_OneValue(): stats → {Stats}", stats);
        Assert.That(stats.Count, Is.EqualTo(1));
        Assert.That(stats.Mean, Is.EqualTo(double.NaN));
        Assert.That(stats.Variance, Is.EqualTo(double.NaN));
        Assert.That(stats.SampleVariance, Is.EqualTo(double.NaN));
		_logger.LogDebug("<<< Exiting WelfordVariance_OneValue()");
    }

    [Test]
    public void WelfordVariance_TwoValues()
    {
		_logger.LogDebug(">>> Entering WelfordVariance_TwoValues()");
        var stats = new WelfordsVariance();
        stats.AddValue(1);
		_logger.LogDebug("WelfordVariance_TwoValues(): stats → {Stats}", stats);
        stats.AddValue(2);
        Assert.That(stats.Count, Is.EqualTo(2));
        Assert.That(stats.Mean, Is.EqualTo(1.5).Within(0.0000001));
        Assert.That(stats.Variance, Is.EqualTo(0.25).Within(0.0000001));
        Assert.That(stats.SampleVariance, Is.EqualTo(0.5).Within(0.0000001));
		_logger.LogDebug("<<< Exiting WelfordVariance_TwoValues()");
    }
}
