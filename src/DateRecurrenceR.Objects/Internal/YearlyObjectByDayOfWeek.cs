using System.Text;
using DateRecurrenceR.Core;

namespace DateRecurrenceR.Objects.Internal;

internal sealed class YearlyObjectByDayOfWeek : IRecurrenceObject, IRecurrence
{
    private readonly string _stringRepresentation;

    public YearlyObjectByDayOfWeek(DateOnly beginDate,
        DateOnly endDate,
        DayOfWeek dayOfWeek,
        NumberOfWeek numberOfWeek,
        MonthOfYear monthOfYear,
        Interval interval)
    {
        BeginDate = beginDate;
        EndDate = endDate;
        DayOfWeek = dayOfWeek;
        NumberOfWeek = numberOfWeek;
        MonthOfYear = monthOfYear;
        Interval = interval;

        var sb = new StringBuilder("Y");
        sb.Append(' ');
        sb.Append(beginDate.ToString("yyyy-MM-dd"));

        if (endDate != DateOnly.MaxValue)
        {
            sb.Append(' ');
            sb.Append(endDate.ToString("yyyy-MM-dd"));
        }

        sb.Append(' ');
        sb.Append(interval);
        sb.Append(' ');
        sb.Append(TwoLetterDayNames.GetByIndex((int) dayOfWeek));
        sb.Append(' ');
        sb.Append(numberOfWeek);

        _stringRepresentation = sb.ToString();
    }

    public DateOnly BeginDate { get; }
    public DateOnly EndDate { get; }
    public DayOfWeek DayOfWeek { get; }
    public NumberOfWeek NumberOfWeek { get; }
    public MonthOfYear MonthOfYear { get; }
    public Interval Interval { get; }

    public IEnumerator<DateOnly> ToEnumerator()
    {
        return Recurrence.Yearly(BeginDate, EndDate, BeginDate, EndDate, DayOfWeek, NumberOfWeek, MonthOfYear,
            Interval);
    }

    public IEnumerator<DateOnly> ToEnumerator(int takeCount)
    {
        return Recurrence.Yearly(BeginDate, BeginDate, takeCount, DayOfWeek, NumberOfWeek, MonthOfYear, Interval);
    }

    public IEnumerator<DateOnly> ToEnumerator(DateOnly fromDate, int takeCount)
    {
        return Recurrence.Yearly(BeginDate, fromDate, takeCount, DayOfWeek, NumberOfWeek, MonthOfYear, Interval);
    }

    public IEnumerator<DateOnly> ToEnumerator(DateOnly fromDate, DateOnly toDate)
    {
        return Recurrence.Yearly(BeginDate, EndDate, fromDate, toDate, DayOfWeek, NumberOfWeek, MonthOfYear, Interval);
    }

    public new string ToString()
    {
        return _stringRepresentation;
    }

    public bool Contains(DateOnly date)
    {
        if (date.DayOfWeek != DayOfWeek) return false;
        
        if (date.Month != MonthOfYear) return false;

        if (date < BeginDate || EndDate < date) return false;

        if ((Interval - 1) * 7 < date.Day || date.Day < Interval * 7) return true;

        return (date.Year - BeginDate.Year) % Interval == 0;
    }
}