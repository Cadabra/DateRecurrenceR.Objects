using FluentAssertions;

namespace DateRecurrenceR.Objects.Tests.Unit;

public class RecurrenceObjectTests
{
    [Test]
    public void RecurrenceModel_Daily_toSting_and_Parse()
    {
        // Arrange
        var interval = 1;
        var model = new RecurrenceObject(DateOnly.MinValue, DateOnly.MaxValue, interval);
        var stringRepresentation = model.ToString();

        // Act
        var sut = RecurrenceObject.Parse(stringRepresentation);

        //Assert
        model.RecurrenceType.Should().Be(RecurrenceType.Daily);
        sut.RecurrenceType.Should().Be(model.RecurrenceType);
        sut.Should().NotBeSameAs(model);
    }

    [Test]
    public void RecurrenceModel_Weekly_toSting_and_Parse()
    {
        // Arrange
        var interval = 1;
        var weekDays = new WeekDays(DayOfWeek.Monday);
        var firstDayOfWeek = DayOfWeek.Monday;
        var model = new RecurrenceObject(DateOnly.MinValue, DateOnly.MaxValue, interval, weekDays, firstDayOfWeek);
        var stringRepresentation = model.ToString();

        // Act
        var sut = RecurrenceObject.Parse(stringRepresentation);

        //Assert
        model.RecurrenceType.Should().Be(RecurrenceType.Weekly);
        sut.RecurrenceType.Should().Be(model.RecurrenceType);
        sut.Should().NotBeSameAs(model);
    }

    [Test]
    public void RecurrenceModel_MonthlyByDayOfWeek_toSting_and_Parse()
    {
        // Arrange
        var interval = 1;
        var dayOfWeek = DayOfWeek.Monday;
        var numberOfWeek = NumberOfWeek.First;
        var model = new RecurrenceObject(DateOnly.MinValue, DateOnly.MaxValue, interval, dayOfWeek, numberOfWeek);
        var stringRepresentation = model.ToString();

        // Act
        var sut = RecurrenceObject.Parse(stringRepresentation);

        //Assert
        model.RecurrenceType.Should().Be(RecurrenceType.MonthlyByDayOfWeek);
        sut.RecurrenceType.Should().Be(model.RecurrenceType);
        sut.Should().NotBeSameAs(model);
    }

    [Test]
    public void RecurrenceModel_MonthlyByDayOfMonth_toSting_and_Parse()
    {
        // Arrange
        var interval = 1;
        var dayOfPeriod = 1;
        var model = new RecurrenceObject(DateOnly.MinValue, DateOnly.MaxValue, interval, dayOfPeriod, PeriodOf.Month);
        var stringRepresentation = model.ToString();

        // Act
        var sut = RecurrenceObject.Parse(stringRepresentation);

        //Assert
        model.RecurrenceType.Should().Be(RecurrenceType.MonthlyByDayOfMonth);
        sut.RecurrenceType.Should().Be(model.RecurrenceType);
        sut.Should().NotBeSameAs(model);
    }

    [Test]
    public void RecurrenceModel_YearlyByDayOfWeek_toSting_and_Parse()
    {
        // Arrange
        var interval = 1;
        var dayOfWeek = DayOfWeek.Monday;
        var numberOfWeek = NumberOfWeek.First;
        var monthOfYear = 1;
        var model = new RecurrenceObject(DateOnly.MinValue, DateOnly.MaxValue, interval, dayOfWeek, numberOfWeek, monthOfYear);
        var stringRepresentation = model.ToString();

        // Act
        var sut = RecurrenceObject.Parse(stringRepresentation);

        //Assert
        model.RecurrenceType.Should().Be(RecurrenceType.YearlyByDayOfWeek);
        sut.RecurrenceType.Should().Be(model.RecurrenceType);
        sut.Should().NotBeSameAs(model);
    }

    [Test]
    public void RecurrenceModel_YearlyByDayOfMonth_toSting_and_Parse()
    {
        // Arrange
        var interval = 1;
        var dayOfMonth = 1;
        var monthOfYear = 1;
        var model = new RecurrenceObject(DateOnly.MinValue, DateOnly.MaxValue, interval, dayOfMonth, monthOfYear);
        var stringRepresentation = model.ToString();

        // Act
        var sut = RecurrenceObject.Parse(stringRepresentation);

        //Assert
        model.RecurrenceType.Should().Be(RecurrenceType.YearlyByDayOfMonth);
        sut.RecurrenceType.Should().Be(model.RecurrenceType);
        sut.Should().NotBeSameAs(model);
    }

    [Test]
    public void RecurrenceModel_YearlyByDayOfYear_toSting_and_Parse()
    {
        // Arrange
        var interval = 1;
        var dayOfPeriod = 1;
        var model = new RecurrenceObject(DateOnly.MinValue, DateOnly.MaxValue, interval, dayOfPeriod, PeriodOf.Year);
        var stringRepresentation = model.ToString();

        // Act
        var sut = RecurrenceObject.Parse(stringRepresentation);

        //Assert
        model.RecurrenceType.Should().Be(RecurrenceType.YearlyByDayOfYear);
        sut.RecurrenceType.Should().Be(model.RecurrenceType);
        sut.Should().NotBeSameAs(model);
    }
}