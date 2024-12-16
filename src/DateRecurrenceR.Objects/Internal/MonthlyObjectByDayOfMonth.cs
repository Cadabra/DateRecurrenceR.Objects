using System.Text;
using DateRecurrenceR.Core;

namespace DateRecurrenceR.Objects.Internal;

internal sealed class MonthlyObjectByDayOfMonth : IRecurrenceObject, IRecurrence
{
    private readonly string _stringRepresentation;

    public MonthlyObjectByDayOfMonth(DateOnly beginDate,
        DateOnly endDate,
        DayOfMonth dayOfMonth,
        Interval interval)
    {
        BeginDate = beginDate;
        EndDate = endDate;
        DayOfMonth = dayOfMonth;
        Interval = interval;

        var sb = new StringBuilder("Monthly");
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
        sb.Append(dayOfMonth);

        _stringRepresentation = sb.ToString();
    }

    public DateOnly BeginDate { get; }
    public DateOnly EndDate { get; }
    public DayOfMonth DayOfMonth { get; }
    public Interval Interval { get; }

    public IEnumerator<DateOnly> ToEnumerator()
    {
        return Recurrence.Monthly(BeginDate, EndDate, BeginDate, EndDate, DayOfMonth, Interval);
    }

    public IEnumerator<DateOnly> ToEnumerator(int takeCount)
    {
        return Recurrence.Monthly(BeginDate, BeginDate, takeCount, DayOfMonth, Interval);
    }

    public IEnumerator<DateOnly> ToEnumerator(DateOnly fromDate, int takeCount)
    {
        return Recurrence.Monthly(BeginDate, fromDate, takeCount, DayOfMonth, Interval);
    }

    public IEnumerator<DateOnly> ToEnumerator(DateOnly fromDate, DateOnly toDate)
    {
        return Recurrence.Monthly(BeginDate, EndDate, fromDate, toDate, DayOfMonth, Interval);
    }

    public new string ToString()
    {
        return _stringRepresentation;
    }

    public bool Contains(DateOnly date)
    {
        if (date < BeginDate || EndDate < date) return false;
        
        if (date.Day != Math.Min(DateTime.DaysInMonth(date.Year,date.Month), DayOfMonth)) return false;
        
        if (((date.Year * 12 + date.Month) - (BeginDate.Year * 12 + BeginDate.Month)) % Interval > 0) return false;

        return true;
    }
}