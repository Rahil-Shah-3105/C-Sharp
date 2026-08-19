using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Algorithms.Problems.JobScheduling;

/// <summary>
/// Implements the greedy algorithm for Interval Scheduling.
/// Finds the maximum set of non-overlapping jobs.
/// </summary>
public static class IntervalSchedulingSolver
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    /// Returns the maximal set of non-overlapping jobs.
    /// </summary>
    /// <param name="jobs">List of jobs to schedule.</param>
    /// <returns>List of selected jobs (maximal set).</returns>
    public static List<Job> Schedule(IEnumerable<Job> jobs)
    {
		_logger.LogDebug(">>> Entering Schedule(jobs={Jobs})", jobs);
        if (jobs == null)
        {
			_logger.LogWarning("Throwing ArgumentNullException from Schedule(jobs={Jobs})", jobs);
            throw new ArgumentNullException(nameof(jobs));
        }

        // Sort jobs by their end time (earliest finish first)
        var sortedJobs = jobs.OrderBy(j => j.End).ToList();
		_logger.LogDebug("Schedule(jobs={Jobs}): sortedJobs → {SortedJobs}", jobs, sortedJobs);
        var result = new List<Job>();
		_logger.LogDebug("Schedule(jobs={Jobs}): result → {Result}", jobs, result);
        int lastEnd = int.MinValue;
		_logger.LogDebug("Schedule(jobs={Jobs}): lastEnd → {LastEnd}", jobs, lastEnd);

        foreach (var job in sortedJobs)
        {
            // If the job starts after the last selected job ends, select it
            if (job.Start >= lastEnd)
            {
                result.Add(job);
                lastEnd = job.End;
            }
        }

		_logger.LogDebug("<<< Exiting Schedule(jobs={Jobs})", jobs);
        return result;
    }
}
