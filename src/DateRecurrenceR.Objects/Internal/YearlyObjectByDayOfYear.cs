using System.Text;
using DateRecurrenceR.Core;

namespace DateRecurrenceR.Objects.Internal;

internal sealed class YearlyObjectByDayOfYear : IRecurrenceObject, IRecurrence
{
    private readonly string _stringRepresentation;

    public YearlyObjectByDayOfYear(DateOnly beginDate,
        DateOnly endDate,
        DayOfYear dayOfYear,
        Interval interval)
    {
        BeginDate = beginDate;
        EndDate = endDate;
        DayOfYear = dayOfYear;
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
        sb.Append(dayOfYear);

        _stringRepresentation = sb.ToString();
    }

    public DateOnly BeginDate { get; }
    public DateOnly EndDate { get; }
    public DayOfYear DayOfYear { get; }
    public Interval Interval { get; }

    public IEnumerator<DateOnly> ToEnumerator()
    {
        return Recurrence.Yearly(BeginDate, EndDate, BeginDate, EndDate, DayOfYear, Interval);
    }

    public IEnumerator<DateOnly> ToEnumerator(int takeCount)
    {
        return Recurrence.Yearly(BeginDate, BeginDate, takeCount, DayOfYear, Interval);
    }

    public IEnumerator<DateOnly> ToEnumerator(DateOnly fromDate, int takeCount)
    {
        return Recurrence.Yearly(BeginDate, fromDate, takeCount, DayOfYear, Interval);
    }

    public IEnumerator<DateOnly> ToEnumerator(DateOnly fromDate, DateOnly toDate)
    {
        return Recurrence.Yearly(BeginDate, EndDate, fromDate, toDate, DayOfYear, Interval);
    }

    public new string ToString()
    {
        return _stringRepresentation;
    }

    public bool Contains(DateOnly date)
    {
        if (date.DayOfYear != DayOfYear) return false;

        if (date < BeginDate || EndDate < date) return false;

        return (date.Year - BeginDate.Year) % Interval == 0;
    }
}