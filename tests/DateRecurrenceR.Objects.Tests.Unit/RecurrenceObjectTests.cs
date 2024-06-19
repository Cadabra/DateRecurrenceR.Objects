using FluentAssertions;

namespace DateRecurrenceR.Objects.Tests.Unit;

public class RecurrenceObjectTests
{
    [Test]
    public void RecurrenceObject_Daily_toSting_and_Parse()
    {
        // Arrange
        var interval = 1;
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
        var interval = 1;
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
        var interval = 1;
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
        var interval = 1;
        var dayOfPeriod = 1;
        var obj = new RecurrenceObject(DateOnly.MinValue, DateOnly.MaxValue, interval, dayOfPeriod, PeriodOf.Month);
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
        var interval = 1;
        var dayOfWeek = DayOfWeek.Monday;
        var numberOfWeek = NumberOfWeek.First;
        var monthOfYear = 1;
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
        var interval = 1;
        var dayOfMonth = 1;
        var monthOfYear = 1;
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
        var interval = 1;
        var dayOfPeriod = 1;
        var obj = new RecurrenceObject(DateOnly.MinValue, DateOnly.MaxValue, interval, dayOfPeriod, PeriodOf.Year);
        var stringRepresentation = obj.ToString();

        // Act
        var sut = RecurrenceObject.Parse(stringRepresentation);

        //Assert
        obj.RecurrenceType.Should().Be(RecurrenceType.YearlyByDayOfYear);
        sut.RecurrenceType.Should().Be(obj.RecurrenceType);
        sut.Should().NotBeSameAs(obj);
    }
}