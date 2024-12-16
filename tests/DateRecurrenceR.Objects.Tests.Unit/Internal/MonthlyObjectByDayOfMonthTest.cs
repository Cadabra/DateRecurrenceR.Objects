using DateRecurrenceR.Core;
using DateRecurrenceR.Objects.Internal;
using FluentAssertions;

namespace DateRecurrenceR.Objects.Tests.Unit.Internal;

[TestFixture]
[TestOf(typeof(MonthlyObjectByDayOfMonth))]
public class MonthlyObjectByDayOfMonthTest
{

    [Test]
    public void Test()
    {
        // Arrange
        var beginDate = DateOnly.MinValue;
        var endDate = DateOnly.MaxValue;
        var dayOfMonth = new DayOfMonth(15);
        var interval = new Interval(99);

        var sut = new MonthlyObjectByDayOfMonth(beginDate, endDate, dayOfMonth, interval);

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