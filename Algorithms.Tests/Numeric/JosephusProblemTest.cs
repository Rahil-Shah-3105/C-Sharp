using Microsoft.Extensions.Logging;
﻿using Algorithms.Numeric;

namespace Algorithms.Tests.Numeric;

public class JosephusProblemTest
{
private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;

    [TestCase(10, 0)]
    [TestCase(10, -1)]
    public void JosephusProblemInvalidStepSize(long groupSize, long step)
    {
		_logger.LogDebug(">>> Entering JosephusProblemInvalidStepSize(groupSize={GroupSize},step={Step})", groupSize, step);
        Assert.Throws(Is.TypeOf<ArgumentException>()
                .And.Message.EqualTo("The step cannot be smaller than 1"),
            delegate { JosephusProblem.FindWinner(groupSize, step); });
			_logger.LogDebug("<<< Exiting JosephusProblemInvalidStepSize(groupSize={GroupSize},step={Step})", groupSize, step);
    }

    [TestCase(10, 12)]
    public void JosephusProblemStepSizeGreaterThanGroup(long groupSize, long step)
    {
		_logger.LogDebug(">>> Entering JosephusProblemStepSizeGreaterThanGroup(groupSize={GroupSize},step={Step})", groupSize, step);
        Assert.Throws(Is.TypeOf<ArgumentException>()
                .And.Message.EqualTo("The step cannot be greater than the size of the group"),
            delegate { JosephusProblem.FindWinner(groupSize, step); });
			_logger.LogDebug("<<< Exiting JosephusProblemStepSizeGreaterThanGroup(groupSize={GroupSize},step={Step})", groupSize, step);
    }

    [TestCase(10, 2, 5)]
    [TestCase(10, 8, 1)]
    [TestCase(254, 18, 92)]
    [TestCase(3948, 614, 2160)]
    [TestCase(86521, 65903, 29473)]
    public void JosephusProblemWinnerCalculation(long groupSize, long step, long position)
    {
		_logger.LogDebug(">>> Entering JosephusProblemWinnerCalculation(groupSize={GroupSize},step={Step},position={Position})", groupSize, step, position);
        Assert.That(JosephusProblem.FindWinner(groupSize, step), Is.EqualTo(position));
		_logger.LogDebug("<<< Exiting JosephusProblemWinnerCalculation(groupSize={GroupSize},step={Step},position={Position})", groupSize, step, position);
    }
}
