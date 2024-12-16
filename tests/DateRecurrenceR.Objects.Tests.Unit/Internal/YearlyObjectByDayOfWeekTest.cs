using DateRecurrenceR.Core;
using DateRecurrenceR.Objects.Internal;
using FluentAssertions;

namespace DateRecurrenceR.Objects.Tests.Unit.Internal;

[TestFixture]
[TestOf(typeof(YearlyObjectByDayOfWeek))]
public class YearlyObjectByDayOfWeekTest
{

    [Test]
    public void Test()
    {
        // Arrange
        var beginDate = DateOnly.MinValue;
        var endDate = DateOnly.MaxValue;
        var dayOfWeek = DayOfWeek.Sunday;
        var numberOfWeek = NumberOfWeek.Second;
        var monthOfYear = new MonthOfYear(7);
        var interval = new Interval(99);

        var sut = new YearlyObjectByDayOfWeek(beginDate, endDate, dayOfWeek, numberOfWeek, monthOfYear, interval);

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