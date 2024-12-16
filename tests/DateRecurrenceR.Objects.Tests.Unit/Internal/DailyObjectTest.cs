using DateRecurrenceR.Core;
using DateRecurrenceR.Objects.Internal;
using FluentAssertions;

namespace DateRecurrenceR.Objects.Tests.Unit.Internal;

[TestFixture]
[TestOf(typeof(DailyObject))]
public class DailyObjectTest
{
    [Test]
    public void Test()
    {
        // Arrange
        var beginDate = DateOnly.MinValue;
        var endDate = DateOnly.MaxValue;
        var interval = new Interval(99);

        var sut = new DailyObject(beginDate, endDate, interval);

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