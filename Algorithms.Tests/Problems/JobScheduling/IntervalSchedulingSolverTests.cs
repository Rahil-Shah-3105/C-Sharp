using System;
using System.Collections.Generic;
using Algorithms.Problems.JobScheduling;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Problems.JobScheduling;

public class IntervalSchedulingSolverTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void Schedule_ReturnsEmpty_WhenNoJobs()
    {
		_logger.LogError(">>> Entering Schedule_ReturnsEmpty_WhenNoJobs()");
        var result = IntervalSchedulingSolver.Schedule(new List<Job>());
		_logger.LogWarning("<<< Exiting Schedule_ReturnsEmpty_WhenNoJobs()");
		_logger.LogError("Schedule_ReturnsEmpty_WhenNoJobs(): result → {Result}", result);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Schedule_ReturnsSingleJob_WhenOnlyOneJob()
    {
		_logger.LogError(">>> Entering Schedule_ReturnsSingleJob_WhenOnlyOneJob()");
        var jobs = new List<Job> { new Job(1, 3) };
        var result = IntervalSchedulingSolver.Schedule(jobs);
		_logger.LogError("Schedule_ReturnsSingleJob_WhenOnlyOneJob(): result → {Result}", result);
		_logger.LogError("Schedule_ReturnsSingleJob_WhenOnlyOneJob(): jobs → {Jobs}", jobs);
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0], Is.EqualTo(jobs[0]));
		_logger.LogWarning("<<< Exiting Schedule_ReturnsSingleJob_WhenOnlyOneJob()");
    }

    [Test]
    public void Schedule_ThrowsArgumentNullException_WhenJobsIsNull()
    {
		_logger.LogError(">>> Entering Schedule_ThrowsArgumentNullException_WhenJobsIsNull()");
        Assert.Throws<ArgumentNullException>(() => IntervalSchedulingSolver.Schedule(jobs: null!));
		_logger.LogWarning("<<< Exiting Schedule_ThrowsArgumentNullException_WhenJobsIsNull()");
    }

    [Test]
    public void Schedule_SelectsJobsWithEqualEndTime()
    {
		_logger.LogError(">>> Entering Schedule_SelectsJobsWithEqualEndTime()");
        var jobs = new List<Job>
        {
            new Job(1, 4),
            new Job(2, 4),
            new Job(4, 6)
        };
        var result = IntervalSchedulingSolver.Schedule(jobs);
		_logger.LogError("Schedule_SelectsJobsWithEqualEndTime(): result → {Result}", result);
        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result, Does.Contain(new Job(1, 4)));
        Assert.That(result, Does.Contain(new Job(4, 6)));
		_logger.LogWarning("<<< Exiting Schedule_SelectsJobsWithEqualEndTime()");
    }

    [Test]
    public void Schedule_SelectsJobStartingAtLastEnd()
    {
		_logger.LogError(">>> Entering Schedule_SelectsJobStartingAtLastEnd()");
        var jobs = new List<Job>
        {
            new Job(1, 3),
            new Job(3, 5),
            new Job(5, 7)
        };
        var result = IntervalSchedulingSolver.Schedule(jobs);
		_logger.LogError("Schedule_SelectsJobStartingAtLastEnd(): result → {Result}", result);
        Assert.That(result, Has.Count.EqualTo(3));
        Assert.That(result[0], Is.EqualTo(jobs[0]));
        Assert.That(result[1], Is.EqualTo(jobs[1]));
        Assert.That(result[2], Is.EqualTo(jobs[2]));
		_logger.LogWarning("<<< Exiting Schedule_SelectsJobStartingAtLastEnd()");
    }

    [Test]
    public void Schedule_HandlesJobsWithNegativeTimes()
    {
		_logger.LogError(">>> Entering Schedule_HandlesJobsWithNegativeTimes()");
        var jobs = new List<Job>
        {
            new Job(-5, -3),
            new Job(-2, 1),
            new Job(0, 2)
        };
        var result = IntervalSchedulingSolver.Schedule(jobs);
		_logger.LogError("Schedule_HandlesJobsWithNegativeTimes(): result → {Result}", result);
        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result, Does.Contain(new Job(-5, -3)));
        Assert.That(result, Does.Contain(new Job(-2, 1)));
		_logger.LogWarning("<<< Exiting Schedule_HandlesJobsWithNegativeTimes()");
    }

    [Test]
    public void Schedule_SelectsNonOverlappingJobs()
    {
		_logger.LogError(">>> Entering Schedule_SelectsNonOverlappingJobs()");
        var jobs = new List<Job>
        {
            new Job(1, 4),
            new Job(3, 5),
            new Job(0, 6),
            new Job(5, 7),
            new Job(8, 9),
            new Job(5, 9)
        };
        var result = IntervalSchedulingSolver.Schedule(jobs);
		_logger.LogError("Schedule_SelectsNonOverlappingJobs(): result → {Result}", result);
        // Expected: (1,4), (5,7), (8,9)
        Assert.That(result, Has.Count.EqualTo(3));
        Assert.That(result, Does.Contain(new Job(1, 4)));
        Assert.That(result, Does.Contain(new Job(5, 7)));
        Assert.That(result, Does.Contain(new Job(8, 9)));
		_logger.LogWarning("<<< Exiting Schedule_SelectsNonOverlappingJobs()");
    }

    [Test]
    public void Schedule_HandlesFullyOverlappingJobs()
    {
		_logger.LogError(">>> Entering Schedule_HandlesFullyOverlappingJobs()");
        var jobs = new List<Job>
        {
            new Job(1, 5),
            new Job(2, 6),
            new Job(3, 7)
        };
        var result = IntervalSchedulingSolver.Schedule(jobs);
		_logger.LogError("Schedule_HandlesFullyOverlappingJobs(): result → {Result}", result);
        // Only one job can be selected
        Assert.That(result, Has.Count.EqualTo(1));
		_logger.LogWarning("<<< Exiting Schedule_HandlesFullyOverlappingJobs()");
    }
}
