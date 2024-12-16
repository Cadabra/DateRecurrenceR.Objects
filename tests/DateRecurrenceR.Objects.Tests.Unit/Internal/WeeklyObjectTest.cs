using DateRecurrenceR.Core;
using DateRecurrenceR.Objects.Internal;
using FluentAssertions;

namespace DateRecurrenceR.Objects.Tests.Unit.Internal;

[TestFixture]
[TestOf(typeof(WeeklyObject))]
public class WeeklyObjectTest
{

    [Test]
    public void Test()
    {
        // Arrange
        var beginDate = DateOnly.MinValue;
        var endDate = DateOnly.MaxValue;
        var weekDays = new WeekDays(DayOfWeek.Monday, DayOfWeek.Thursday);
        var firstDayOfWeek = Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
        var interval = new Interval(5);

        var sut = new WeeklyObject(beginDate, endDate, weekDays, firstDayOfWeek, interval);

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