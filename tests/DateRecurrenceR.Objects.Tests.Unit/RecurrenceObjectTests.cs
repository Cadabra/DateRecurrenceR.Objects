using DateRecurrenceR.Core;
using DateRecurrenceR.Objects.Internal;
using FluentAssertions;

namespace DateRecurrenceR.Objects.Tests.Unit;

public class RecurrenceObjectTests
{
    [Test]
    public void Recurrence()
    {
        var interval = new Interval(9);
        var dayOfMonth = new DayOfMonth(27);
        var daily1 = new DailyObject(DateOnly.MinValue, DateOnly.MaxValue, interval);
        // var daily2 = new DailyObject(DateOnly.MinValue, DateOnly.MaxValue.AddDays(-1), interval);

        var weekDays = new WeekDays(DayOfWeek.Tuesday, DayOfWeek.Thursday);
        var weekly1 = new WeeklyObject(DateOnly.MinValue, DateOnly.MaxValue, weekDays, DayOfWeek.Monday, interval);
        // var weekly2 = new WeeklyObject(DateOnly.MinValue, DateOnly.MaxValue.AddDays(-1), weekDays, DayOfWeek.Monday, interval);

        var monthly1 = new MonthlyObjectByDayOfMonth(DateOnly.MinValue, DateOnly.MaxValue, dayOfMonth, interval);
        // var monthly2 = new MonthlyObjectByDayOfMonth(DateOnly.MinValue, DateOnly.MaxValue.AddDays(-1), dayOfMonth, interval);
        var monthly3 = new MonthlyObjectByDayOfWeek(DateOnly.MinValue, DateOnly.MaxValue, DayOfWeek.Monday, NumberOfWeek.Last, interval);
        // var monthly4 = new MonthlyObjectByDayOfWeek(DateOnly.MinValue, DateOnly.MaxValue.AddDays(-1), DayOfWeek.Monday, NumberOfWeek.Last, interval);

        var yearly1 = new YearlyObjectByDayOfMonth(DateOnly.MinValue, DateOnly.MaxValue, dayOfMonth, new MonthOfYear(1), interval);
        // var yearly2 = new YearlyObjectByDayOfMonth(DateOnly.MinValue, DateOnly.MaxValue.AddDays(-1), dayOfMonth, new MonthOfYear(1), interval);
        var yearly3 = new YearlyObjectByDayOfWeek(DateOnly.MinValue, DateOnly.MaxValue, DayOfWeek.Monday, NumberOfWeek.First, new MonthOfYear(1), interval);
        // var yearly4 = new YearlyObjectByDayOfWeek(DateOnly.MinValue, DateOnly.MaxValue.AddDays(-1), DayOfWeek.Monday, NumberOfWeek.First, new MonthOfYear(1), interval);
        var yearly5 = new YearlyObjectByDayOfYear(DateOnly.MinValue, DateOnly.MaxValue, new DayOfYear(265), interval);
        // var yearly6 = new YearlyObjectByDayOfYear(DateOnly.MinValue, DateOnly.MaxValue.AddDays(-1), new DayOfYear(265), interval);

        var daily1Str = daily1.ToString();
        // var daily2Str = daily2.ToString();
        var weekly1Str = weekly1.ToString();
        // var weekly2Str = weekly2.ToString();
        var monthly1Str = monthly1.ToString();
        // var monthly2Str = monthly2.ToString();
        var monthly3Str = monthly3.ToString();
        // var monthly4Str = monthly4.ToString();
        var yearly1Str = yearly1.ToString();
        // var yearly2Str = yearly2.ToString();
        var yearly3Str = yearly3.ToString();
        // var yearly4Str = yearly4.ToString();
        var yearly5Str = yearly5.ToString();
        // var yearly6Str = yearly6.ToString();
    }

    [Test]
    public void RecurrenceObject_Daily_toSting_and_Parse()
    {
        // Arrange
        var interval = new Interval(1);
        var obj = new RecurrenceObject(DateOnly.MinValue, DateOnly.MaxValue, interval);
        var stringRepresentation = obj.ToString();

        // Act
        var sut = RecurrenceObject.Parse(stringRepresentation);

        //Assert
        obj.RecurrenceType.Should().Be(RecurrenceType.Daily);
        sut.RecurrenceType.Should().Be(obj.RecurrenceType);
        sut.Should().NotBeSameAs(obj);
    }

    [Test]
    public void RecurrenceObject_Weekly_toSting_and_Parse()
    {
        // Arrange
        var interval = new Interval(1);
        var weekDays = new WeekDays(DayOfWeek.Monday);
        var firstDayOfWeek = DayOfWeek.Monday;
        var obj = new RecurrenceObject(DateOnly.MinValue, DateOnly.MaxValue, interval, weekDays, firstDayOfWeek);
        var stringRepresentation = obj.ToString();

        // Act
        var sut = RecurrenceObject.Parse(stringRepresentation);

        //Assert
        obj.RecurrenceType.Should().Be(RecurrenceType.Weekly);
        sut.RecurrenceType.Should().Be(obj.RecurrenceType);
        sut.Should().NotBeSameAs(obj);
    }

    [Test]
    public void RecurrenceObject_MonthlyByDayOfWeek_toSting_and_Parse()
    {
        // Arrange
        var interval = new Interval(1);
        var dayOfWeek = DayOfWeek.Monday;
        var numberOfWeek = NumberOfWeek.First;
        var obj = new RecurrenceObject(DateOnly.MinValue, DateOnly.MaxValue, interval, dayOfWeek, numberOfWeek);
        var stringRepresentation = obj.ToString();

        // Act
        var sut = RecurrenceObject.Parse(stringRepresentation);

        //Assert
        obj.RecurrenceType.Should().Be(RecurrenceType.MonthlyByDayOfWeek);
        sut.RecurrenceType.Should().Be(obj.RecurrenceType);
        sut.Should().NotBeSameAs(obj);
    }

    [Test]
    public void RecurrenceObject_MonthlyByDayOfMonth_toSting_and_Parse()
    {
        // Arrange
        var interval = new Interval(1);
        var dayOfMonth = new DayOfMonth(1);
        var obj = new RecurrenceObject(DateOnly.MinValue, DateOnly.MaxValue, interval, dayOfMonth);
        var stringRepresentation = obj.ToString();

        // Act
        var sut = RecurrenceObject.Parse(stringRepresentation);

        //Assert
        obj.RecurrenceType.Should().Be(RecurrenceType.MonthlyByDayOfMonth);
        sut.RecurrenceType.Should().Be(obj.RecurrenceType);
        sut.Should().NotBeSameAs(obj);
    }

    [Test]
    public void RecurrenceObject_YearlyByDayOfWeek_toSting_and_Parse()
    {
        // Arrange
        var interval = new Interval(1);
        var dayOfWeek = DayOfWeek.Monday;
        var numberOfWeek = NumberOfWeek.First;
        var monthOfYear = new MonthOfYear(1);
        var obj = new RecurrenceObject(DateOnly.MinValue, DateOnly.MaxValue, interval, dayOfWeek, numberOfWeek, monthOfYear);
        var stringRepresentation = obj.ToString();

        // Act
        var sut = RecurrenceObject.Parse(stringRepresentation);

        //Assert
        obj.RecurrenceType.Should().Be(RecurrenceType.YearlyByDayOfWeek);
        sut.RecurrenceType.Should().Be(obj.RecurrenceType);
        sut.Should().NotBeSameAs(obj);
    }

    [Test]
    public void RecurrenceObject_YearlyByDayOfMonth_toSting_and_Parse()
    {
        // Arrange
        var interval = new Interval(1);
        var dayOfMonth = new DayOfMonth(1);
        var monthOfYear = new MonthOfYear(1);
        var obj = new RecurrenceObject(DateOnly.MinValue, DateOnly.MaxValue, interval, dayOfMonth, monthOfYear);
        var stringRepresentation = obj.ToString();

        // Act
        var sut = RecurrenceObject.Parse(stringRepresentation);

        //Assert
        obj.RecurrenceType.Should().Be(RecurrenceType.YearlyByDayOfMonth);
        sut.RecurrenceType.Should().Be(obj.RecurrenceType);
        sut.Should().NotBeSameAs(obj);
    }

    [Test]
    public void RecurrenceObject_YearlyByDayOfYear_toSting_and_Parse()
    {
        // Arrange
        var interval = new Interval(1);
        var dayOfYear = new DayOfYear(1);
        var obj = new RecurrenceObject(DateOnly.MinValue, DateOnly.MaxValue, interval, dayOfYear);
        var stringRepresentation = obj.ToString();

        // Act
        var sut = RecurrenceObject.Parse(stringRepresentation);

        //Assert
        obj.RecurrenceType.Should().Be(RecurrenceType.YearlyByDayOfYear);
        sut.RecurrenceType.Should().Be(obj.RecurrenceType);
        sut.Should().NotBeSameAs(obj);
    }
}