using System.Text;
using DateRecurrenceR.Core;

namespace DateRecurrenceR.Objects.Internal;

internal sealed class YearlyObjectByDayOfMonth : IRecurrenceObject, IRecurrence
{
    private readonly string _stringRepresentation;

    public YearlyObjectByDayOfMonth(DateOnly beginDate,
        DateOnly endDate,
        DayOfMonth dayOfMonth,
        MonthOfYear monthOfYear,
        Interval interval)
    {
        BeginDate = beginDate;
        EndDate = endDate;
        DayOfMonth = dayOfMonth;
        MonthOfYear = monthOfYear;
        Interval = interval;

        var sb = new StringBuilder("Yearly");
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
        sb.Append(((int) dayOfMonth).ToString());
        sb.Append(' ');
        sb.Append(((int) monthOfYear).ToString());

        _stringRepresentation = sb.ToString();
    }

    public DateOnly BeginDate { get; }
    public DateOnly EndDate { get; }
    public DayOfMonth DayOfMonth { get; }
    public MonthOfYear MonthOfYear { get; }
    public Interval Interval { get; }

    public IEnumerator<DateOnly> ToEnumerator()
    {
        return Recurrence.Yearly(BeginDate, EndDate, BeginDate, EndDate, DayOfMonth, MonthOfYear, Interval);
    }

    public IEnumerator<DateOnly> ToEnumerator(int takeCount)
    {
        return Recurrence.Yearly(BeginDate, BeginDate, takeCount, DayOfMonth, MonthOfYear, Interval);
    }

    public IEnumerator<DateOnly> ToEnumerator(DateOnly fromDate, int takeCount)
    {
        return Recurrence.Yearly(BeginDate, fromDate, takeCount, DayOfMonth, MonthOfYear, Interval);
    }

    public IEnumerator<DateOnly> ToEnumerator(DateOnly fromDate, DateOnly toDate)
    {
        return Recurrence.Yearly(BeginDate, EndDate, fromDate, toDate, DayOfMonth, MonthOfYear, Interval);
    }

    public new string ToString()
    {
        return _stringRepresentation;
    }

    public bool Contains(DateOnly date)
    {
        if (date.Month != MonthOfYear) return false;

        if (date < BeginDate || EndDate < date) return false;

        if (date.Day != Math.Min(DateTime.DaysInMonth(date.Year,date.Month), DayOfMonth)) return false;

        return (date.Year - BeginDate.Year) % Interval == 0;
    }
}