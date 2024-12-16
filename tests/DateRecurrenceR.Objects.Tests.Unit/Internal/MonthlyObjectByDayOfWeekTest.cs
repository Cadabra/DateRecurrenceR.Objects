using DateRecurrenceR.Core;
using DateRecurrenceR.Objects.Internal;
using FluentAssertions;

namespace DateRecurrenceR.Objects.Tests.Unit.Internal;

[TestFixture]
[TestOf(typeof(MonthlyObjectByDayOfWeek))]
public class MonthlyObjectByDayOfWeekTest
{

    [Test]
    public void Test()
    {
        // Arrange
        var beginDate = DateOnly.MinValue;
        var endDate = DateOnly.MaxValue;
        var dayOfWeek = DayOfWeek.Tuesday;
        var numberOfWeek = NumberOfWeek.Second;
        var interval = new Interval(99);

        var sut = new MonthlyObjectByDayOfWeek(beginDate, endDate, dayOfWeek, numberOfWeek, interval);

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