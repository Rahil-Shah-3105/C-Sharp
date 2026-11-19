using FluentAssertions.Execution;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests;

public static class TimelineTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void CountTest()
    {
		_logger.LogError(">>> Entering CountTest()");
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
			_logger.LogWarning("<<< Exiting CountTest()");
    }

    [Test]
    public static void TimesCountTest()
    {
		_logger.LogError(">>> Entering TimesCountTest()");
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
			_logger.LogWarning("<<< Exiting TimesCountTest()");
    }

    [Test]
    public static void ValuesCountTest()
    {
		_logger.LogError(">>> Entering ValuesCountTest()");
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
			_logger.LogWarning("<<< Exiting ValuesCountTest()");
    }

    [Test]
    public static void IndexerGetTest()
    {
		_logger.LogError(">>> Entering IndexerGetTest()");
        const string eventName = "TestTime2";
        var eventDate = new DateTime(2000, 1, 1);
		_logger.LogError("IndexerGetTest(): eventDate → {EventDate}", eventDate);

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
			_logger.LogWarning("<<< Exiting IndexerGetTest()");
    }

    [Test]
    public static void IndexerSetTest()
    {
		_logger.LogError(">>> Entering IndexerSetTest()");
        var eventDate = new DateTime(2000, 1, 1);

		_logger.LogError("IndexerSetTest(): eventDate → {EventDate}", eventDate);
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
			_logger.LogWarning("<<< Exiting IndexerSetTest()");
    }

    [Test]
    public static void EqualsTest()
    {
		_logger.LogError(">>> Entering EqualsTest()");
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
			_logger.LogWarning("<<< Exiting EqualsTest()");
    }

    [Test]
    public static void ClearTest()
    {
		_logger.LogError(">>> Entering ClearTest()");
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
			_logger.LogWarning("<<< Exiting ClearTest()");
    }

    [Test]
    public static void CopyToTest()
    {
		_logger.LogError(">>> Entering CopyToTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var array = new (DateTime Time, string Value)[timeline.Count];
		_logger.LogError("CopyToTest(): array → {Array}", array);
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
		_logger.LogWarning("<<< Exiting CopyToTest()");
    }

    [Test]
    public static void GetAllTimesTest()
    {
		_logger.LogError(">>> Entering GetAllTimesTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var times = timeline.GetAllTimes();
		_logger.LogError("GetAllTimesTest(): times → {Times}", times);

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
		_logger.LogWarning("<<< Exiting GetAllTimesTest()");
    }

    [Test]
    public static void GetTimesByValueTest()
    {
		_logger.LogError(">>> Entering GetTimesByValueTest()");
        var eventDate = new DateTime(2000, 1, 1);
        const string eventName = "TestTime2";
		_logger.LogError("GetTimesByValueTest(): eventDate → {EventDate}", eventDate);

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
			_logger.LogWarning("<<< Exiting GetTimesByValueTest()");
    }

    [Test]
    public static void GetTimesBeforeTest()
    {
		_logger.LogError(">>> Entering GetTimesBeforeTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var times = timeline.GetTimesBefore(new DateTime(2003, 1, 1));
		_logger.LogError("GetTimesBeforeTest(): times → {Times}", times);

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
		_logger.LogWarning("<<< Exiting GetTimesBeforeTest()");
    }

    [Test]
    public static void GetTimesAfterTest()
    {
		_logger.LogError(">>> Entering GetTimesAfterTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var times = timeline.GetTimesAfter(new DateTime(2003, 1, 1));
		_logger.LogError("GetTimesAfterTest(): times → {Times}", times);

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
		_logger.LogWarning("<<< Exiting GetTimesAfterTest()");
    }

    [Test]
    public static void GetAllValuesTest()
    {
		_logger.LogError(">>> Entering GetAllValuesTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var values = timeline.GetAllValues();
		_logger.LogError("GetAllValuesTest(): values → {Values}", values);

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
		_logger.LogWarning("<<< Exiting GetAllValuesTest()");
    }

    [Test]
    public static void GetValuesByTimeTest()
    {
		_logger.LogError(">>> Entering GetValuesByTimeTest()");
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
			_logger.LogWarning("<<< Exiting GetValuesByTimeTest()");
    }

    [Test]
    public static void GetValuesBeforeTest()
    {
		_logger.LogError(">>> Entering GetValuesBeforeTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var array = timeline.GetValuesBefore(new DateTime(2003, 1, 1)).ToArray();
		_logger.LogError("GetValuesBeforeTest(): array → {Array}", array);

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
		_logger.LogWarning("<<< Exiting GetValuesBeforeTest()");
    }

    [Test]
    public static void GetValuesAfterTest()
    {
		_logger.LogError(">>> Entering GetValuesAfterTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var array = timeline.GetValuesAfter(new DateTime(2003, 1, 1)).ToArray();
		_logger.LogError("GetValuesAfterTest(): array → {Array}", array);

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
		_logger.LogWarning("<<< Exiting GetValuesAfterTest()");
    }

    [Test]
    public static void GetValuesByMillisecondTest()
    {
		_logger.LogError(">>> Entering GetValuesByMillisecondTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1985, 1, 1, 10, 0, 0, 250), "TestTime1" },
            { new DateTime(1990, 1, 1, 10, 0, 0, 250), "TestTime2" },
            { new DateTime(1995, 1, 1, 10, 0, 0, 250), "TestTime3" },
            { new DateTime(2005, 1, 1, 10, 0, 0, 750), "TestTime4" },
            { new DateTime(2015, 1, 1, 10, 0, 0, 750), "TestTime5" },
        };

        var query = timeline.GetValuesByMillisecond(750);
		_logger.LogError("GetValuesByMillisecondTest(): query → {Query}", query);

        query.Count
            .Should()
            .Be(2);
			_logger.LogWarning("<<< Exiting GetValuesByMillisecondTest()");
    }

    [Test]
    public static void GetValuesBySecondTest()
    {
		_logger.LogError(">>> Entering GetValuesBySecondTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1985, 1, 1, 10, 0, 5), "TestTime1" },
            { new DateTime(1990, 1, 1, 10, 0, 5), "TestTime2" },
            { new DateTime(1995, 1, 1, 10, 0, 5), "TestTime3" },
            { new DateTime(2005, 1, 1, 10, 0, 20), "TestTime4" },
            { new DateTime(2015, 1, 1, 10, 0, 20), "TestTime5" },
        };

        var query = timeline.GetValuesBySecond(20);
		_logger.LogError("GetValuesBySecondTest(): query → {Query}", query);

        using (new AssertionScope())
        {
            query.Count
                .Should()
                .Be(2);

            timeline
                .Should()
                .Contain(query);
        }
		_logger.LogWarning("<<< Exiting GetValuesBySecondTest()");
    }

    [Test]
    public static void GetValuesByMinuteTest()
    {
		_logger.LogError(">>> Entering GetValuesByMinuteTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1985, 1, 1, 10, 15, 0), "TestTime1" },
            { new DateTime(1990, 1, 1, 10, 15, 0), "TestTime2" },
            { new DateTime(1995, 1, 1, 10, 15, 0), "TestTime3" },
            { new DateTime(2005, 1, 1, 10, 40, 0), "TestTime4" },
            { new DateTime(2015, 1, 1, 10, 40, 0), "TestTime5" },
        };

        var query = timeline.GetValuesByMinute(40);
		_logger.LogError("GetValuesByMinuteTest(): query → {Query}", query);

        using (new AssertionScope())
        {
            query.Count
                .Should()
                .Be(2);

            timeline
                .Should()
                .Contain(query);
        }
		_logger.LogWarning("<<< Exiting GetValuesByMinuteTest()");
    }

    [Test]
    public static void GetValuesByHourTest()
    {
		_logger.LogError(">>> Entering GetValuesByHourTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1985, 1, 1, 7, 0, 0), "TestTime1" },
            { new DateTime(1990, 1, 1, 7, 0, 0), "TestTime2" },
            { new DateTime(1995, 1, 1, 7, 0, 0), "TestTime3" },
            { new DateTime(2005, 1, 1, 16, 0, 0), "TestTime4" },
            { new DateTime(2015, 1, 1, 16, 0, 0), "TestTime5" },
        };

        var query = timeline.GetValuesByHour(16);
		_logger.LogError("GetValuesByHourTest(): query → {Query}", query);

        using (new AssertionScope())
        {
            query.Count
                .Should()
                .Be(2);

            timeline
                .Should()
                .Contain(query);
        }
		_logger.LogWarning("<<< Exiting GetValuesByHourTest()");
    }

    [Test]
    public static void GetValuesByDayTest()
    {
		_logger.LogError(">>> Entering GetValuesByDayTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1985, 1, 10), "TestTime1" },
            { new DateTime(1990, 1, 10), "TestTime2" },
            { new DateTime(1995, 1, 10), "TestTime3" },
            { new DateTime(2005, 1, 20), "TestTime4" },
            { new DateTime(2015, 1, 20), "TestTime5" },
        };

        var query = timeline.GetValuesByDay(20);
		_logger.LogError("GetValuesByDayTest(): query → {Query}", query);

        using (new AssertionScope())
        {
            query.Count
                .Should()
                .Be(2);

            timeline
                .Should()
                .Contain(query);
        }
		_logger.LogWarning("<<< Exiting GetValuesByDayTest()");
    }

    [Test]
    public static void GetValuesByTimeOfDayTest()
    {
		_logger.LogError(">>> Entering GetValuesByTimeOfDayTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1985, 1, 1, 10, 30, 15, 500), "TestTime1" },
            { new DateTime(1990, 1, 1, 10, 30, 15, 500), "TestTime2" },
            { new DateTime(1995, 1, 1, 10, 30, 15, 500), "TestTime3" },
            { new DateTime(2005, 1, 1, 21, 15, 40, 600), "TestTime4" },
            { new DateTime(2015, 1, 1, 21, 15, 40, 600), "TestTime5" },
        };

        var query = timeline.GetValuesByTimeOfDay(new TimeSpan(0, 21, 15, 40, 600));
		_logger.LogError("GetValuesByTimeOfDayTest(): query → {Query}", query);

        using (new AssertionScope())
        {
            query.Count
                .Should()
                .Be(2);

            timeline
                .Should()
                .Contain(query);
        }
		_logger.LogWarning("<<< Exiting GetValuesByTimeOfDayTest()");
    }

    [Test]
    public static void GetValuesByDayOfWeekTest()
    {
		_logger.LogError(">>> Entering GetValuesByDayOfWeekTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(2015, 1, 5), "TestTime1" }, //Monday
            { new DateTime(2015, 2, 2), "TestTime2" }, //Monday
            { new DateTime(2015, 1, 6), "TestTime3" }, //Tuesday
            { new DateTime(2015, 1, 7), "TestTime4" }, //Wednesday
            { new DateTime(2015, 1, 8), "TestTime5" }, //Thursday
        };

        var query = timeline.GetValuesByDayOfWeek(DayOfWeek.Monday);
		_logger.LogError("GetValuesByDayOfWeekTest(): query → {Query}", query);

        using (new AssertionScope())
        {
            query.Count
                .Should()
                .Be(2);

            timeline
                .Should()
                .Contain(query);
        }
		_logger.LogWarning("<<< Exiting GetValuesByDayOfWeekTest()");
    }

    [Test]
    public static void GetValuesByDayOfYearTest()
    {
		_logger.LogError(">>> Entering GetValuesByDayOfYearTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1985, 1, 3), "TestTime1" }, //3rd day of year
            { new DateTime(1990, 1, 7), "TestTime2" }, //7th day of year
            { new DateTime(1995, 1, 22), "TestTime3" }, //22th day of year
            { new DateTime(2000, 2, 1), "TestTime4" }, //32th day of year
            { new DateTime(2005, 2, 1), "TestTime5" }, //32th day of year
        };

        var query = timeline.GetValuesByDayOfYear(32);
		_logger.LogError("GetValuesByDayOfYearTest(): query → {Query}", query);

        using (new AssertionScope())
        {
            query.Count
                .Should()
                .Be(2);

            timeline
                .Should()
                .Contain(query);
        }
		_logger.LogWarning("<<< Exiting GetValuesByDayOfYearTest()");
    }

    [Test]
    public static void GetValuesByMonthTest()
    {
		_logger.LogError(">>> Entering GetValuesByMonthTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1985, 1, 1), "TestTime1" },
            { new DateTime(1990, 2, 1), "TestTime2" },
            { new DateTime(1995, 3, 1), "TestTime3" },
            { new DateTime(2005, 4, 1), "TestTime4" },
            { new DateTime(2015, 4, 1), "TestTime5" },
        };

        var query = timeline.GetValuesByMonth(4);
		_logger.LogError("GetValuesByMonthTest(): query → {Query}", query);

        using (new AssertionScope())
        {
            query.Count
                .Should()
                .Be(2);

            timeline
                .Should()
                .Contain(query);
        }
		_logger.LogWarning("<<< Exiting GetValuesByMonthTest()");
    }

    [Test]
    public static void GetValuesByYearTest()
    {
		_logger.LogError(">>> Entering GetValuesByYearTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1985, 1, 2), "TestTime1" },
            { new DateTime(1990, 2, 1), "TestTime2" },
            { new DateTime(1995, 1, 2), "TestTime3" },
            { new DateTime(2005, 2, 1), "TestTime4" },
            { new DateTime(2005, 1, 2), "TestTime5" },
        };

        var query = timeline.GetValuesByYear(2005);
		_logger.LogError("GetValuesByYearTest(): query → {Query}", query);

        using (new AssertionScope())
        {
            query.Count
                .Should()
                .Be(2);

            timeline
                .Should()
                .Contain(query);
        }
		_logger.LogWarning("<<< Exiting GetValuesByYearTest()");
    }

    [Test]
    public static void AddDateTimeAndTValueTest() //void Add(DateTime time, TValue value)
    {
		_logger.LogError(">>> Entering AddDateTimeAndTValueTest()");
        var eventDate = new DateTime(2015, 1, 1);
        const string eventName = "TestTime";
		_logger.LogError("AddDateTimeAndTValueTest(): eventDate → {EventDate}", eventDate);

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
			_logger.LogWarning("<<< Exiting AddDateTimeAndTValueTest()");
    }

    [Test]
    public static void AddDateTimeAndTValueArrayTest() //void Add(params (DateTime, TValue)[] timeline)
    {
		_logger.LogError(">>> Entering AddDateTimeAndTValueArrayTest()");
        var eventDate1 = new DateTime(2015, 1, 1);
        const string eventName1 = "TestTime1";
		_logger.LogError("AddDateTimeAndTValueArrayTest(): eventDate1 → {EventDate1}", eventDate1);

        var eventDate2 = new DateTime(1750, 1, 1);
		_logger.LogError("AddDateTimeAndTValueArrayTest(): eventDate2 → {EventDate2}", eventDate2);
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
		_logger.LogWarning("<<< Exiting AddDateTimeAndTValueArrayTest()");
    }

    [Test]
    public static void AddTimelineTest() //void Add(Timeline<TValue> timeline)
    {
		_logger.LogError(">>> Entering AddTimelineTest()");
        var eventDate = new DateTime(2015, 1, 1);
        const string eventName = "TestTime";
		_logger.LogError("AddTimelineTest(): eventDate → {EventDate}", eventDate);

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
		_logger.LogWarning("<<< Exiting AddTimelineTest()");
    }

    [Test]
    public static void AddNowTest()
    {
		_logger.LogError(">>> Entering AddNowTest()");
        var timeline = new Timeline<string>();

		_logger.LogError("AddNowTest(): timeline → {Timeline}", timeline);
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
		_logger.LogWarning("<<< Exiting AddNowTest()");
    }

    [Test]
    public static void ContainsDateTimeAndTValueTest() //bool Contains(DateTime time, TValue value)
    {
		_logger.LogError(">>> Entering ContainsDateTimeAndTValueTest()");
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
			_logger.LogWarning("<<< Exiting ContainsDateTimeAndTValueTest()");
    }

    [Test]
    public static void ContainsDateTimeAndTValueArrayTest() //bool Contains(params (DateTime, TValue)[] timeline)
    {
		_logger.LogError(">>> Entering ContainsDateTimeAndTValueArrayTest()");
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
			_logger.LogWarning("<<< Exiting ContainsDateTimeAndTValueArrayTest()");
    }

    [Test]
    public static void ContainsTimelineTest() //bool Contains(Timeline<TValue> timeline)
    {
		_logger.LogError(">>> Entering ContainsTimelineTest()");
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
			_logger.LogWarning("<<< Exiting ContainsTimelineTest()");
    }

    [Test]
    public static void ContainsTimeTest()
    {
		_logger.LogError(">>> Entering ContainsTimeTest()");
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
			_logger.LogWarning("<<< Exiting ContainsTimeTest()");
    }

    [Test]
    public static void ContainsValueTest()
    {
		_logger.LogError(">>> Entering ContainsValueTest()");
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
			_logger.LogWarning("<<< Exiting ContainsValueTest()");
    }

    [Test]
    public static void RemoveDateTimeAndTValueTest() //bool Remove(DateTime time, TValue value)
    {
		_logger.LogError(">>> Entering RemoveDateTimeAndTValueTest()");
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
		_logger.LogWarning("<<< Exiting RemoveDateTimeAndTValueTest()");
    }

    [Test]
    public static void RemoveDateTimeAndTValueArrayTest() //bool Remove(params (DateTime, TValue)[] timeline)
    {
		_logger.LogError(">>> Entering RemoveDateTimeAndTValueArrayTest()");
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
		_logger.LogWarning("<<< Exiting RemoveDateTimeAndTValueArrayTest()");
    }

    [Test]
    public static void RemoveTimelineTest() //bool Remove(Timeline<TValue> timeline)
    {
		_logger.LogError(">>> Entering RemoveTimelineTest()");
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
		_logger.LogWarning("<<< Exiting RemoveTimelineTest()");
    }

    [Test]
    public static void RemoveTimeTest()
    {
		_logger.LogError(">>> Entering RemoveTimeTest()");
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
		_logger.LogWarning("<<< Exiting RemoveTimeTest()");
    }

    [Test]
    public static void RemoveValueTest()
    {
		_logger.LogError(">>> Entering RemoveValueTest()");
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
		_logger.LogWarning("<<< Exiting RemoveValueTest()");
    }

    [Test]
    public static void ToArrayTest()
    {
		_logger.LogError(">>> Entering ToArrayTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var array = timeline.ToArray();
		_logger.LogError("ToArrayTest(): array → {Array}", array);

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
		_logger.LogWarning("<<< Exiting ToArrayTest()");
    }

    [Test]
    public static void ToListTest()
    {
		_logger.LogError(">>> Entering ToListTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var list = timeline.ToList();
		_logger.LogError("ToListTest(): list → {List}", list);

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
		_logger.LogWarning("<<< Exiting ToListTest()");
    }

    [Test]
    public static void ToDictionaryTest()
    {
		_logger.LogError(">>> Entering ToDictionaryTest()");
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var dictionary = timeline.ToDictionary();
		_logger.LogError("ToDictionaryTest(): dictionary → {Dictionary}", dictionary);

        var timelineList = new List<(DateTime Time, string Value)>();
		_logger.LogError("ToDictionaryTest(): timelineList → {TimelineList}", timelineList);
        foreach (var pair in timeline)
        {
            timelineList.Add(pair);
        }

        var dictionaryList = new List<(DateTime Time, string Value)>();
		_logger.LogError("ToDictionaryTest(): dictionaryList → {DictionaryList}", dictionaryList);
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
		_logger.LogWarning("<<< Exiting ToDictionaryTest()");
    }

    [Test]
    public static void EqualityOperatorTest()
    {
		_logger.LogError(">>> Entering EqualityOperatorTest()");
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
			_logger.LogWarning("<<< Exiting EqualityOperatorTest()");
    }

    [Test]
    public static void InequalityOperatorTest()
    {
		_logger.LogError(">>> Entering InequalityOperatorTest()");
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
			_logger.LogWarning("<<< Exiting InequalityOperatorTest()");
    }
}
