using DateRecurrenceR.Core;
using DateRecurrenceR.Objects.Internal;
using FluentAssertions;

namespace DateRecurrenceR.Objects.Tests.Unit.Internal;

[TestFixture]
[TestOf(typeof(YearlyObjectByDayOfMonth))]
public class YearlyObjectByDayOfMonthTest
{

    [Test]
    public void Test()
    {
        // Arrange
        var beginDate = new DateOnly(4, 1, 1);
        var endDate = DateOnly.MaxValue;
        var dayOfMonth = new DayOfMonth(29);
        var monthOfYear = new MonthOfYear(2);
        var interval = new Interval(99);

        var sut = new YearlyObjectByDayOfMonth(beginDate, endDate, dayOfMonth, monthOfYear, interval);

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