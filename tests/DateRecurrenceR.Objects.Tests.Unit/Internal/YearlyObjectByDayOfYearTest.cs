using DateRecurrenceR.Core;
using DateRecurrenceR.Objects.Internal;
using FluentAssertions;

namespace DateRecurrenceR.Objects.Tests.Unit.Internal;

[TestFixture]
[TestOf(typeof(YearlyObjectByDayOfYear))]
public class YearlyObjectByDayOfYearTest
{

    [Test]
    public void Test()
    {
        // Arrange
        var beginDate = DateOnly.MinValue;
        var endDate = DateOnly.MaxValue;
        var dayOfYear = new DayOfYear(256);
        var interval = new Interval(99);

        var sut = new YearlyObjectByDayOfYear(beginDate, endDate, dayOfYear, interval);

        // Act
        var enumerator = sut.ToEnumerator();
        var res = false;
        while (enumerator.MoveNext())
        {
            res = sut.Contains(enumerator.Current);

            if (!res) break;
        }

        // Assert
        res.Should().Be(true);
    }
}