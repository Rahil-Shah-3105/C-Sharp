using FluentAssertions.Execution;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests;

public static class TimelineTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void CountTest()
    {
		_logger.LogDebug(">>> Entering CountTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.Count
            .Should()
            .Be(5);
			_logger.LogDebug("<<< Exiting CountTest()");
    }

    [Test]
    public static void TimesCountTest()
    {
		_logger.LogDebug(">>> Entering TimesCountTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.TimesCount
            .Should()
            .Be(timeline.GetAllTimes().Length);
			_logger.LogDebug("<<< Exiting TimesCountTest()");
    }

    [Test]
    public static void ValuesCountTest()
    {
		_logger.LogDebug(">>> Entering ValuesCountTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.ValuesCount
            .Should()
            .Be(timeline.GetAllValues().Length);
			_logger.LogDebug("<<< Exiting ValuesCountTest()");
    }

    [Test]
    public static void IndexerGetTest()
    {
		_logger.LogDebug(">>> Entering IndexerGetTest()");
        const string eventName = "TestTime2";
        var eventDate = new DateTime(2000, 1, 1);
		_logger.LogDebug("IndexerGetTest(): eventDate → {EventDate}", eventDate);

        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { eventDate, eventName },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline[eventDate][0]
            .Should()
            .Be(eventName);
			_logger.LogDebug("<<< Exiting IndexerGetTest()");
    }

    [Test]
    public static void IndexerSetTest()
    {
		_logger.LogDebug(">>> Entering IndexerSetTest()");
        var eventDate = new DateTime(2000, 1, 1);

		_logger.LogDebug("IndexerSetTest(): eventDate → {EventDate}", eventDate);
        const string formerEventName = "TestTime2";
        const string eventName = "TestTime2Modified";

        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { eventDate, formerEventName },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline[new DateTime(2000, 1, 1)] = [eventName];

        timeline[new DateTime(2000, 1, 1)][0]
            .Should()
            .Be(eventName);
			_logger.LogDebug("<<< Exiting IndexerSetTest()");
    }

    [Test]
    public static void EqualsTest()
    {
		_logger.LogDebug(">>> Entering EqualsTest()");
        var timeline1 = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var timeline2 = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        (timeline1 == timeline2)
            .Should()
            .BeTrue();
			_logger.LogDebug("<<< Exiting EqualsTest()");
    }

    [Test]
    public static void ClearTest()
    {
		_logger.LogDebug(">>> Entering ClearTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.Clear();

        timeline.Count
            .Should()
            .Be(0);
			_logger.LogDebug("<<< Exiting ClearTest()");
    }

    [Test]
    public static void CopyToTest()
    {
		_logger.LogDebug(">>> Entering CopyToTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var array = new (DateTime Time, string Value)[timeline.Count];
		_logger.LogDebug("CopyToTest(): array → {Array}", array);
        timeline.CopyTo(array, 0);

        timeline.Count
            .Should()
            .Be(array.Length);

        var i = 0;
        using (new AssertionScope())
        {
            foreach (var (time, value) in timeline)
            {
                array[i].Time
                    .Should()
                    .Be(time);

                array[i].Value
                    .Should()
                    .Be(value);

                ++i;
            }
        }
		_logger.LogDebug("<<< Exiting CopyToTest()");
    }

    [Test]
    public static void GetAllTimesTest()
    {
		_logger.LogDebug(">>> Entering GetAllTimesTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var times = timeline.GetAllTimes();
		_logger.LogDebug("GetAllTimesTest(): times → {Times}", times);

        var i = 0;
        using (new AssertionScope())
        {
            foreach (var (time, _) in timeline)
            {
                times[i++]
                    .Should()
                    .Be(time);
            }
        }
		_logger.LogDebug("<<< Exiting GetAllTimesTest()");
    }

    [Test]
    public static void GetTimesByValueTest()
    {
		_logger.LogDebug(">>> Entering GetTimesByValueTest()");
        var eventDate = new DateTime(2000, 1, 1);
        const string eventName = "TestTime2";
		_logger.LogDebug("GetTimesByValueTest(): eventDate → {EventDate}", eventDate);

        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { eventDate, eventName },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.GetTimesByValue(eventName)[0]
            .Should()
            .Be(eventDate);
			_logger.LogDebug("<<< Exiting GetTimesByValueTest()");
    }

    [Test]
    public static void GetTimesBeforeTest()
    {
		_logger.LogDebug(">>> Entering GetTimesBeforeTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var times = timeline.GetTimesBefore(new DateTime(2003, 1, 1));
		_logger.LogDebug("GetTimesBeforeTest(): times → {Times}", times);

        using (new AssertionScope())
        {
            times.Length
                .Should()
                .Be(2);

            times[0]
                .Should()
                .Be(new DateTime(1995, 1, 1));

            times[1]
                .Should()
                .Be(new DateTime(2000, 1, 1));
        }
		_logger.LogDebug("<<< Exiting GetTimesBeforeTest()");
    }

    [Test]
    public static void GetTimesAfterTest()
    {
		_logger.LogDebug(">>> Entering GetTimesAfterTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var times = timeline.GetTimesAfter(new DateTime(2003, 1, 1));
		_logger.LogDebug("GetTimesAfterTest(): times → {Times}", times);

        using (new AssertionScope())
        {
            times.Length
                .Should()
                .Be(3);

            times[0]
                .Should()
                .Be(new DateTime(2005, 1, 1));

            times[1]
                .Should()
                .Be(new DateTime(2010, 1, 1));

            times[2]
                .Should()
                .Be(new DateTime(2015, 1, 1));
        }
		_logger.LogDebug("<<< Exiting GetTimesAfterTest()");
    }

    [Test]
    public static void GetAllValuesTest()
    {
		_logger.LogDebug(">>> Entering GetAllValuesTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var values = timeline.GetAllValues();
		_logger.LogDebug("GetAllValuesTest(): values → {Values}", values);

        var i = 0;
        using (new AssertionScope())
        {
            foreach (var (_, value) in timeline)
            {
                values[i++]
                    .Should()
                    .Be(value);
            }
        }
		_logger.LogDebug("<<< Exiting GetAllValuesTest()");
    }

    [Test]
    public static void GetValuesByTimeTest()
    {
		_logger.LogDebug(">>> Entering GetValuesByTimeTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.GetValuesByTime(new DateTime(2000, 1, 1))[0]
            .Should()
            .Be("TestTime2");
			_logger.LogDebug("<<< Exiting GetValuesByTimeTest()");
    }

    [Test]
    public static void GetValuesBeforeTest()
    {
		_logger.LogDebug(">>> Entering GetValuesBeforeTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var array = timeline.GetValuesBefore(new DateTime(2003, 1, 1)).ToArray();
		_logger.LogDebug("GetValuesBeforeTest(): array → {Array}", array);

        using (new AssertionScope())
        {
            array.Length
                .Should()
                .Be(2);

            array[0].Time
                .Should()
                .Be(new DateTime(1995, 1, 1));

            array[1].Time
                .Should()
                .Be(new DateTime(2000, 1, 1));
        }
		_logger.LogDebug("<<< Exiting GetValuesBeforeTest()");
    }

    [Test]
    public static void GetValuesAfterTest()
    {
		_logger.LogDebug(">>> Entering GetValuesAfterTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var array = timeline.GetValuesAfter(new DateTime(2003, 1, 1)).ToArray();
		_logger.LogDebug("GetValuesAfterTest(): array → {Array}", array);

        using (new AssertionScope())
        {
            array.Length
                .Should()
                .Be(3);

            array[0].Time
                .Should()
                .Be(new DateTime(2005, 1, 1));

            array[1].Time
                .Should()
                .Be(new DateTime(2010, 1, 1));

            array[2].Time
                .Should()
                .Be(new DateTime(2015, 1, 1));
        }
		_logger.LogDebug("<<< Exiting GetValuesAfterTest()");
    }

    [Test]
    public static void GetValuesByMillisecondTest()
    {
		_logger.LogDebug(">>> Entering GetValuesByMillisecondTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1985, 1, 1, 10, 0, 0, 250), "TestTime1" },
            { new DateTime(1990, 1, 1, 10, 0, 0, 250), "TestTime2" },
            { new DateTime(1995, 1, 1, 10, 0, 0, 250), "TestTime3" },
            { new DateTime(2005, 1, 1, 10, 0, 0, 750), "TestTime4" },
            { new DateTime(2015, 1, 1, 10, 0, 0, 750), "TestTime5" },
        };

        var query = timeline.GetValuesByMillisecond(750);
		_logger.LogDebug("GetValuesByMillisecondTest(): query → {Query}", query);

        query.Count
            .Should()
            .Be(2);
			_logger.LogDebug("<<< Exiting GetValuesByMillisecondTest()");
    }

    [Test]
    public static void GetValuesBySecondTest()
    {
		_logger.LogDebug(">>> Entering GetValuesBySecondTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1985, 1, 1, 10, 0, 5), "TestTime1" },
            { new DateTime(1990, 1, 1, 10, 0, 5), "TestTime2" },
            { new DateTime(1995, 1, 1, 10, 0, 5), "TestTime3" },
            { new DateTime(2005, 1, 1, 10, 0, 20), "TestTime4" },
            { new DateTime(2015, 1, 1, 10, 0, 20), "TestTime5" },
        };

        var query = timeline.GetValuesBySecond(20);
		_logger.LogDebug("GetValuesBySecondTest(): query → {Query}", query);

        using (new AssertionScope())
        {
            query.Count
                .Should()
                .Be(2);

            timeline
                .Should()
                .Contain(query);
        }
		_logger.LogDebug("<<< Exiting GetValuesBySecondTest()");
    }

    [Test]
    public static void GetValuesByMinuteTest()
    {
		_logger.LogDebug(">>> Entering GetValuesByMinuteTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1985, 1, 1, 10, 15, 0), "TestTime1" },
            { new DateTime(1990, 1, 1, 10, 15, 0), "TestTime2" },
            { new DateTime(1995, 1, 1, 10, 15, 0), "TestTime3" },
            { new DateTime(2005, 1, 1, 10, 40, 0), "TestTime4" },
            { new DateTime(2015, 1, 1, 10, 40, 0), "TestTime5" },
        };

        var query = timeline.GetValuesByMinute(40);
		_logger.LogDebug("GetValuesByMinuteTest(): query → {Query}", query);

        using (new AssertionScope())
        {
            query.Count
                .Should()
                .Be(2);

            timeline
                .Should()
                .Contain(query);
        }
		_logger.LogDebug("<<< Exiting GetValuesByMinuteTest()");
    }

    [Test]
    public static void GetValuesByHourTest()
    {
		_logger.LogDebug(">>> Entering GetValuesByHourTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1985, 1, 1, 7, 0, 0), "TestTime1" },
            { new DateTime(1990, 1, 1, 7, 0, 0), "TestTime2" },
            { new DateTime(1995, 1, 1, 7, 0, 0), "TestTime3" },
            { new DateTime(2005, 1, 1, 16, 0, 0), "TestTime4" },
            { new DateTime(2015, 1, 1, 16, 0, 0), "TestTime5" },
        };

        var query = timeline.GetValuesByHour(16);
		_logger.LogDebug("GetValuesByHourTest(): query → {Query}", query);

        using (new AssertionScope())
        {
            query.Count
                .Should()
                .Be(2);

            timeline
                .Should()
                .Contain(query);
        }
		_logger.LogDebug("<<< Exiting GetValuesByHourTest()");
    }

    [Test]
    public static void GetValuesByDayTest()
    {
		_logger.LogDebug(">>> Entering GetValuesByDayTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1985, 1, 10), "TestTime1" },
            { new DateTime(1990, 1, 10), "TestTime2" },
            { new DateTime(1995, 1, 10), "TestTime3" },
            { new DateTime(2005, 1, 20), "TestTime4" },
            { new DateTime(2015, 1, 20), "TestTime5" },
        };

        var query = timeline.GetValuesByDay(20);
		_logger.LogDebug("GetValuesByDayTest(): query → {Query}", query);

        using (new AssertionScope())
        {
            query.Count
                .Should()
                .Be(2);

            timeline
                .Should()
                .Contain(query);
        }
		_logger.LogDebug("<<< Exiting GetValuesByDayTest()");
    }

    [Test]
    public static void GetValuesByTimeOfDayTest()
    {
		_logger.LogDebug(">>> Entering GetValuesByTimeOfDayTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1985, 1, 1, 10, 30, 15, 500), "TestTime1" },
            { new DateTime(1990, 1, 1, 10, 30, 15, 500), "TestTime2" },
            { new DateTime(1995, 1, 1, 10, 30, 15, 500), "TestTime3" },
            { new DateTime(2005, 1, 1, 21, 15, 40, 600), "TestTime4" },
            { new DateTime(2015, 1, 1, 21, 15, 40, 600), "TestTime5" },
        };

        var query = timeline.GetValuesByTimeOfDay(new TimeSpan(0, 21, 15, 40, 600));
		_logger.LogDebug("GetValuesByTimeOfDayTest(): query → {Query}", query);

        using (new AssertionScope())
        {
            query.Count
                .Should()
                .Be(2);

            timeline
                .Should()
                .Contain(query);
        }
		_logger.LogDebug("<<< Exiting GetValuesByTimeOfDayTest()");
    }

    [Test]
    public static void GetValuesByDayOfWeekTest()
    {
		_logger.LogDebug(">>> Entering GetValuesByDayOfWeekTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(2015, 1, 5), "TestTime1" }, //Monday
            { new DateTime(2015, 2, 2), "TestTime2" }, //Monday
            { new DateTime(2015, 1, 6), "TestTime3" }, //Tuesday
            { new DateTime(2015, 1, 7), "TestTime4" }, //Wednesday
            { new DateTime(2015, 1, 8), "TestTime5" }, //Thursday
        };

        var query = timeline.GetValuesByDayOfWeek(DayOfWeek.Monday);
		_logger.LogDebug("GetValuesByDayOfWeekTest(): query → {Query}", query);

        using (new AssertionScope())
        {
            query.Count
                .Should()
                .Be(2);

            timeline
                .Should()
                .Contain(query);
        }
		_logger.LogDebug("<<< Exiting GetValuesByDayOfWeekTest()");
    }

    [Test]
    public static void GetValuesByDayOfYearTest()
    {
		_logger.LogDebug(">>> Entering GetValuesByDayOfYearTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1985, 1, 3), "TestTime1" }, //3rd day of year
            { new DateTime(1990, 1, 7), "TestTime2" }, //7th day of year
            { new DateTime(1995, 1, 22), "TestTime3" }, //22th day of year
            { new DateTime(2000, 2, 1), "TestTime4" }, //32th day of year
            { new DateTime(2005, 2, 1), "TestTime5" }, //32th day of year
        };

        var query = timeline.GetValuesByDayOfYear(32);
		_logger.LogDebug("GetValuesByDayOfYearTest(): query → {Query}", query);

        using (new AssertionScope())
        {
            query.Count
                .Should()
                .Be(2);

            timeline
                .Should()
                .Contain(query);
        }
		_logger.LogDebug("<<< Exiting GetValuesByDayOfYearTest()");
    }

    [Test]
    public static void GetValuesByMonthTest()
    {
		_logger.LogDebug(">>> Entering GetValuesByMonthTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1985, 1, 1), "TestTime1" },
            { new DateTime(1990, 2, 1), "TestTime2" },
            { new DateTime(1995, 3, 1), "TestTime3" },
            { new DateTime(2005, 4, 1), "TestTime4" },
            { new DateTime(2015, 4, 1), "TestTime5" },
        };

        var query = timeline.GetValuesByMonth(4);
		_logger.LogDebug("GetValuesByMonthTest(): query → {Query}", query);

        using (new AssertionScope())
        {
            query.Count
                .Should()
                .Be(2);

            timeline
                .Should()
                .Contain(query);
        }
		_logger.LogDebug("<<< Exiting GetValuesByMonthTest()");
    }

    [Test]
    public static void GetValuesByYearTest()
    {
		_logger.LogDebug(">>> Entering GetValuesByYearTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1985, 1, 2), "TestTime1" },
            { new DateTime(1990, 2, 1), "TestTime2" },
            { new DateTime(1995, 1, 2), "TestTime3" },
            { new DateTime(2005, 2, 1), "TestTime4" },
            { new DateTime(2005, 1, 2), "TestTime5" },
        };

        var query = timeline.GetValuesByYear(2005);
		_logger.LogDebug("GetValuesByYearTest(): query → {Query}", query);

        using (new AssertionScope())
        {
            query.Count
                .Should()
                .Be(2);

            timeline
                .Should()
                .Contain(query);
        }
		_logger.LogDebug("<<< Exiting GetValuesByYearTest()");
    }

    [Test]
    public static void AddDateTimeAndTValueTest() //void Add(DateTime time, TValue value)
    {
		_logger.LogDebug(">>> Entering AddDateTimeAndTValueTest()");
        var eventDate = new DateTime(2015, 1, 1);
        const string eventName = "TestTime";
		_logger.LogDebug("AddDateTimeAndTValueTest(): eventDate → {EventDate}", eventDate);

        var timeline = new Timeline<string>
        {
            { eventDate, eventName }
        };

        timeline.Count
            .Should()
            .Be(1);

        timeline[eventDate][0]
            .Should()
            .Be(eventName);
			_logger.LogDebug("<<< Exiting AddDateTimeAndTValueTest()");
    }

    [Test]
    public static void AddDateTimeAndTValueArrayTest() //void Add(params (DateTime, TValue)[] timeline)
    {
		_logger.LogDebug(">>> Entering AddDateTimeAndTValueArrayTest()");
        var eventDate1 = new DateTime(2015, 1, 1);
        const string eventName1 = "TestTime1";
		_logger.LogDebug("AddDateTimeAndTValueArrayTest(): eventDate1 → {EventDate1}", eventDate1);

        var eventDate2 = new DateTime(1750, 1, 1);
		_logger.LogDebug("AddDateTimeAndTValueArrayTest(): eventDate2 → {EventDate2}", eventDate2);
        const string eventName2 = "TestTime2";

        var timeline = new Timeline<string>
        {
            {
                (eventDate1, eventName1),
                (eventDate2, eventName2)
            }
        };

        using (new AssertionScope())
        {
            timeline.Count
                .Should()
                .Be(2);

            timeline[eventDate1][0]
                .Should()
                .Be(eventName1);

            timeline[eventDate2][0]
                .Should()
                .Be(eventName2);
        }
		_logger.LogDebug("<<< Exiting AddDateTimeAndTValueArrayTest()");
    }

    [Test]
    public static void AddTimelineTest() //void Add(Timeline<TValue> timeline)
    {
		_logger.LogDebug(">>> Entering AddTimelineTest()");
        var eventDate = new DateTime(2015, 1, 1);
        const string eventName = "TestTime";
		_logger.LogDebug("AddTimelineTest(): eventDate → {EventDate}", eventDate);

        var timeline = new Timeline<string>
        {
            new Timeline<string>(eventDate, eventName)
        };

        using (new AssertionScope())
        {
            timeline.Count
                .Should()
                .Be(1);

            timeline[eventDate][0]
                .Should()
                .Be(eventName);
        }
		_logger.LogDebug("<<< Exiting AddTimelineTest()");
    }

    [Test]
    public static void AddNowTest()
    {
		_logger.LogDebug(">>> Entering AddNowTest()");
        var timeline = new Timeline<string>();

		_logger.LogDebug("AddNowTest(): timeline → {Timeline}", timeline);
        timeline.AddNow("Now");

        using (new AssertionScope())
        {
            timeline.Count
                .Should()
                .Be(1);

            timeline.ContainsValue("Now")
                .Should()
                .BeTrue();
        }
		_logger.LogDebug("<<< Exiting AddNowTest()");
    }

    [Test]
    public static void ContainsDateTimeAndTValueTest() //bool Contains(DateTime time, TValue value)
    {
		_logger.LogDebug(">>> Entering ContainsDateTimeAndTValueTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.Contains(new DateTime(2000, 1, 1), "TestTime2")
            .Should()
            .BeTrue();
			_logger.LogDebug("<<< Exiting ContainsDateTimeAndTValueTest()");
    }

    [Test]
    public static void ContainsDateTimeAndTValueArrayTest() //bool Contains(params (DateTime, TValue)[] timeline)
    {
		_logger.LogDebug(">>> Entering ContainsDateTimeAndTValueArrayTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.Contains(
                (new DateTime(1995, 1, 1), "TestTime1"),
                (new DateTime(2000, 1, 1), "TestTime2"))
            .Should()
            .BeTrue();
			_logger.LogDebug("<<< Exiting ContainsDateTimeAndTValueArrayTest()");
    }

    [Test]
    public static void ContainsTimelineTest() //bool Contains(Timeline<TValue> timeline)
    {
		_logger.LogDebug(">>> Entering ContainsTimelineTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.Contains(new Timeline<string>(new DateTime(2000, 1, 1), "TestTime2"))
            .Should()
            .BeTrue();
			_logger.LogDebug("<<< Exiting ContainsTimelineTest()");
    }

    [Test]
    public static void ContainsTimeTest()
    {
		_logger.LogDebug(">>> Entering ContainsTimeTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.ContainsTime(new DateTime(2000, 1, 1))
            .Should()
            .BeTrue();
			_logger.LogDebug("<<< Exiting ContainsTimeTest()");
    }

    [Test]
    public static void ContainsValueTest()
    {
		_logger.LogDebug(">>> Entering ContainsValueTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.ContainsValue("TestTime1")
            .Should()
            .BeTrue();
			_logger.LogDebug("<<< Exiting ContainsValueTest()");
    }

    [Test]
    public static void RemoveDateTimeAndTValueTest() //bool Remove(DateTime time, TValue value)
    {
		_logger.LogDebug(">>> Entering RemoveDateTimeAndTValueTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.Remove(new DateTime(2000, 1, 1), "TestTime2");

        using (new AssertionScope())
        {
            timeline.Count
                .Should()
                .Be(4);

            timeline.Contains(new DateTime(2000, 1, 1), "TestTime2")
                .Should()
                .BeFalse();
        }
		_logger.LogDebug("<<< Exiting RemoveDateTimeAndTValueTest()");
    }

    [Test]
    public static void RemoveDateTimeAndTValueArrayTest() //bool Remove(params (DateTime, TValue)[] timeline)
    {
		_logger.LogDebug(">>> Entering RemoveDateTimeAndTValueArrayTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.Remove(
            (new DateTime(1995, 1, 1), "TestTime1"),
            (new DateTime(2000, 1, 1), "TestTime2"));

        using (new AssertionScope())
        {
            timeline.Count
                .Should()
                .Be(3);

            timeline.Contains(
                    (new DateTime(1995, 1, 1), "TestTime1"),
                    (new DateTime(2000, 1, 1), "TestTime2"))
                .Should()
                .BeFalse();
        }
		_logger.LogDebug("<<< Exiting RemoveDateTimeAndTValueArrayTest()");
    }

    [Test]
    public static void RemoveTimelineTest() //bool Remove(Timeline<TValue> timeline)
    {
		_logger.LogDebug(">>> Entering RemoveTimelineTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.Remove(new Timeline<string>(new DateTime(2000, 1, 1), "TestTime2"));

        using (new AssertionScope())
        {
            timeline.Count
                .Should()
                .Be(4);

            timeline.Contains(new DateTime(2000, 1, 1), "TestTime2")
                .Should()
                .BeFalse();
        }
		_logger.LogDebug("<<< Exiting RemoveTimelineTest()");
    }

    [Test]
    public static void RemoveTimeTest()
    {
		_logger.LogDebug(">>> Entering RemoveTimeTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.RemoveTimes(new DateTime(2000, 1, 1));

        using (new AssertionScope())
        {
            timeline.Count
                .Should()
                .Be(4);

            timeline.ContainsTime(new DateTime(2000, 1, 1))
                .Should()
                .BeFalse();
        }
		_logger.LogDebug("<<< Exiting RemoveTimeTest()");
    }

    [Test]
    public static void RemoveValueTest()
    {
		_logger.LogDebug(">>> Entering RemoveValueTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.RemoveValues("TestTime1");

        using (new AssertionScope())
        {
            timeline.Count
                .Should()
                .Be(4);

            timeline.ContainsValue("TestTime1")
                .Should()
                .BeFalse();
        }
		_logger.LogDebug("<<< Exiting RemoveValueTest()");
    }

    [Test]
    public static void ToArrayTest()
    {
		_logger.LogDebug(">>> Entering ToArrayTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var array = timeline.ToArray();
		_logger.LogDebug("ToArrayTest(): array → {Array}", array);

        timeline.Count
            .Should()
            .Be(array.Length);

        using (new AssertionScope())
        {
            var i = 0;
            foreach (var (time, value) in timeline)
            {
                time
                    .Should()
                    .Be(array[i].Time);

                value
                    .Should()
                    .Be(array[i].Value);

                ++i;
            }
        }
		_logger.LogDebug("<<< Exiting ToArrayTest()");
    }

    [Test]
    public static void ToListTest()
    {
		_logger.LogDebug(">>> Entering ToListTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var list = timeline.ToList();
		_logger.LogDebug("ToListTest(): list → {List}", list);

        timeline.Count
            .Should()
            .Be(list.Count);

        using (new AssertionScope())
        {
            var i = 0;
            foreach (var (time, value) in timeline)
            {
                time
                    .Should()
                    .Be(list[i].Time);

                value
                    .Should()
                    .Be(list[i].Value);

                ++i;
            }
        }
		_logger.LogDebug("<<< Exiting ToListTest()");
    }

    [Test]
    public static void ToDictionaryTest()
    {
		_logger.LogDebug(">>> Entering ToDictionaryTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var dictionary = timeline.ToDictionary();
		_logger.LogDebug("ToDictionaryTest(): dictionary → {Dictionary}", dictionary);

        var timelineList = new List<(DateTime Time, string Value)>();
		_logger.LogDebug("ToDictionaryTest(): timelineList → {TimelineList}", timelineList);
        foreach (var pair in timeline)
        {
            timelineList.Add(pair);
        }

        var dictionaryList = new List<(DateTime Time, string Value)>();
		_logger.LogDebug("ToDictionaryTest(): dictionaryList → {DictionaryList}", dictionaryList);
        foreach (var (key, value) in dictionary)
        {
            dictionaryList.Add((key, value));
        }

        timelineList.OrderBy(pair => pair.Time);
        dictionaryList.OrderBy(pair => pair.Time);

        timelineList.Count
            .Should()
            .Be(dictionaryList.Count);

        using (new AssertionScope())
        {
            for (var i = 0; i < timelineList.Count; ++i)
            {
                timelineList[i].Time
                    .Should()
                    .Be(dictionaryList[i].Time);

                timelineList[i].Value
                    .Should()
                    .Be(dictionaryList[i].Value);
            }
        }
		_logger.LogDebug("<<< Exiting ToDictionaryTest()");
    }

    [Test]
    public static void EqualityOperatorTest()
    {
		_logger.LogDebug(">>> Entering EqualityOperatorTest()");
        var timeline1 = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var timeline2 = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        (timeline1 == timeline2)
            .Should()
            .BeTrue();
			_logger.LogDebug("<<< Exiting EqualityOperatorTest()");
    }

    [Test]
    public static void InequalityOperatorTest()
    {
		_logger.LogDebug(">>> Entering InequalityOperatorTest()");
        var timeline1 = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var timeline2 = new Timeline<string>
        {
            { new DateTime(1895, 1, 1), "TestTime6" },
            { new DateTime(1900, 1, 1), "TestTime7" },
            { new DateTime(1905, 1, 1), "TestTime8" },
            { new DateTime(1910, 1, 1), "TestTime9" },
            { new DateTime(1915, 1, 1), "TestTime10" },
        };

        (timeline1 == timeline2)
            .Should()
            .BeFalse();
			_logger.LogDebug("<<< Exiting InequalityOperatorTest()");
    }
}
