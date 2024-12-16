using DateRecurrenceR.Core;
using DateRecurrenceR.Objects.Internal;

namespace DateRecurrenceR.Objects;

public class RecurrenceObject : IRecurrenceObject, IEquatable<RecurrenceObject>
{
    private readonly IRecurrenceObject _recurrenceObject;

    public RecurrenceObject(DateOnly beginDate, DateOnly endDate, Interval interval)
        : this(RecurrenceType.Daily, beginDate, endDate, interval)
    {
    }

    public RecurrenceObject(DateOnly beginDate, DateOnly endDate, Interval interval, WeekDays weekDays,
        DayOfWeek firstDayOfWeek)
        : this(RecurrenceType.Weekly, beginDate, endDate, interval, weekDays: weekDays, firstDayOfWeek: firstDayOfWeek)
    {
    }

    public RecurrenceObject(DateOnly beginDate, DateOnly endDate, Interval interval, DayOfWeek dayOfWeek,
        NumberOfWeek numberOfWeek)
        : this(RecurrenceType.MonthlyByDayOfWeek, beginDate, endDate, interval, dayOfWeek: dayOfWeek,
            numberOfWeek: numberOfWeek)
    {
    }

    public RecurrenceObject(DateOnly beginDate, DateOnly endDate, Interval interval, DayOfWeek dayOfWeek,
        NumberOfWeek numberOfWeek, MonthOfYear monthOfYear)
        : this(RecurrenceType.YearlyByDayOfWeek, beginDate, endDate, interval, dayOfWeek: dayOfWeek,
            numberOfWeek: numberOfWeek, monthOfYear: monthOfYear)
    {
    }

    public RecurrenceObject(DateOnly beginDate, DateOnly endDate, Interval interval, DayOfMonth dayOfMonth,
        MonthOfYear monthOfYear)
        : this(RecurrenceType.YearlyByDayOfMonth, beginDate, endDate, interval, dayOfMonth: dayOfMonth,
            monthOfYear: monthOfYear)
    {
    }

    public RecurrenceObject(DateOnly beginDate, DateOnly endDate, Interval interval, DayOfMonth dayOfMonth)
        : this(RecurrenceType.MonthlyByDayOfMonth, beginDate, endDate, interval, dayOfMonth: dayOfMonth)
    {
    }

    public RecurrenceObject(DateOnly beginDate, DateOnly endDate, Interval interval, DayOfYear dayOfYear)
        : this(RecurrenceType.YearlyByDayOfYear, beginDate, endDate, interval, dayOfYear: dayOfYear)
    {
    }

    internal RecurrenceObject(RecurrenceType recurrenceType,
        DateOnly beginDate,
        DateOnly endDate,
        Interval interval,
        DayOfWeek? dayOfWeek = null,
        DayOfMonth? dayOfMonth = null,
        DayOfYear? dayOfYear = null,
        NumberOfWeek? numberOfWeek = null,
        MonthOfYear? monthOfYear = null,
        DayOfWeek? firstDayOfWeek = null,
        WeekDays? weekDays = null)
    {
        BeginDate = beginDate;
        EndDate = endDate;
        Interval = interval;

        BeginDate = beginDate;
        EndDate = endDate;
        Interval = interval;
        DayOfWeek = dayOfWeek;
        DayOfMonth = dayOfMonth;
        DayOfYear = dayOfYear;
        NumberOfWeek = numberOfWeek;
        MonthOfYear = monthOfYear;
        FirstDayOfWeek = firstDayOfWeek;
        WeekDays = weekDays;
        RecurrenceType = recurrenceType;

        switch (RecurrenceType)
        {
            case RecurrenceType.Daily:
                _recurrenceObject = new DailyObject(BeginDate, EndDate, Interval);
                break;
            case RecurrenceType.Weekly:
                _recurrenceObject =
                    new WeeklyObject(BeginDate, EndDate, WeekDays!, FirstDayOfWeek!.Value, Interval);
                break;
            case RecurrenceType.MonthlyByDayOfWeek:
                _recurrenceObject = new MonthlyObjectByDayOfWeek(BeginDate, EndDate, DayOfWeek!.Value,
                    NumberOfWeek!.Value, Interval);
                break;
            case RecurrenceType.MonthlyByDayOfMonth:
                _recurrenceObject =
                    new MonthlyObjectByDayOfMonth(BeginDate, EndDate, DayOfMonth!.Value, Interval);
                break;
            case RecurrenceType.YearlyByDayOfWeek:
                _recurrenceObject = new YearlyObjectByDayOfWeek(BeginDate, EndDate, DayOfWeek!.Value,
                    NumberOfWeek!.Value, MonthOfYear!.Value, Interval);
                break;
            case RecurrenceType.YearlyByDayOfMonth:
                _recurrenceObject = new YearlyObjectByDayOfMonth(BeginDate, EndDate, DayOfMonth!.Value,
                    MonthOfYear!.Value, Interval);
                break;
            case RecurrenceType.YearlyByDayOfYear:
                _recurrenceObject = new YearlyObjectByDayOfYear(BeginDate, EndDate, DayOfYear!.Value, Interval);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public DateOnly BeginDate { get; init; }
    public DateOnly EndDate { get; init; }
    public Interval Interval { get; init; }

    public DayOfWeek? DayOfWeek { get; init; }
    public DayOfMonth? DayOfMonth { get; init; }
    public DayOfYear? DayOfYear { get; init; }
    public NumberOfWeek? NumberOfWeek { get; init; }
    public MonthOfYear? MonthOfYear { get; init; }
    public DayOfWeek? FirstDayOfWeek { get; init; }
    public WeekDays? WeekDays { get; init; }

    public RecurrenceType RecurrenceType { get; init; }

    public static RecurrenceObject Parse(string str)
    {
        return RecurrenceModelStringParser.Parse(str);
    }

    public IEnumerator<DateOnly> ToEnumerator()
    {
        return _recurrenceObject.ToEnumerator();
    }

    public IEnumerator<DateOnly> ToEnumerator(int takeCount)
    {
        return _recurrenceObject.ToEnumerator(takeCount);
    }

    public IEnumerator<DateOnly> ToEnumerator(DateOnly fromDate, int takeCount)
    {
        return _recurrenceObject.ToEnumerator(fromDate, takeCount);
    }

    public IEnumerator<DateOnly> ToEnumerator(DateOnly fromDate, DateOnly toDate)
    {
        return _recurrenceObject.ToEnumerator(fromDate, toDate);
    }

    public new string ToString()
    {
        return _recurrenceObject.ToString();
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((RecurrenceObject) obj);
    }

    public bool Equals(RecurrenceObject? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        return _recurrenceObject.Equals(other._recurrenceObject) && BeginDate.Equals(other.BeginDate) &&
               EndDate.Equals(other.EndDate) && Interval == other.Interval && DayOfWeek == other.DayOfWeek &&
               DayOfMonth == other.DayOfMonth && DayOfYear == other.DayOfYear && NumberOfWeek == other.NumberOfWeek &&
               MonthOfYear == other.MonthOfYear && FirstDayOfWeek == other.FirstDayOfWeek &&
               Equals(WeekDays, other.WeekDays) && RecurrenceType == other.RecurrenceType;
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(_recurrenceObject);
        hashCode.Add(BeginDate);
        hashCode.Add(EndDate);
        hashCode.Add(Interval);
        hashCode.Add(DayOfWeek);
        hashCode.Add(DayOfMonth);
        hashCode.Add(DayOfYear);
        hashCode.Add(NumberOfWeek);
        hashCode.Add(MonthOfYear);
        hashCode.Add(FirstDayOfWeek);
        hashCode.Add(WeekDays);
        hashCode.Add((int) RecurrenceType);
        return hashCode.ToHashCode();
    }

    public static bool operator ==(RecurrenceObject? left, RecurrenceObject? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(RecurrenceObject? left, RecurrenceObject? right)
    {
        return !Equals(left, right);
    }
}