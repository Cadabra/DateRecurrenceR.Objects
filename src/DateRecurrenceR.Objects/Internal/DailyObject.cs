using System.Text;
using DateRecurrenceR.Core;

namespace DateRecurrenceR.Objects.Internal;

internal sealed class DailyObject : IRecurrenceObject, IRecurrence
{
    private readonly string _stringRepresentation;

    public DailyObject(DateOnly beginDate, DateOnly endDate, Interval interval)
    {
        BeginDate = beginDate;
        EndDate = endDate;
        Interval = interval;

        var sb = new StringBuilder("Daily");
        sb.Append(' ');
        sb.Append(beginDate.ToString("yyyy-MM-dd"));

        if (endDate != DateOnly.MaxValue)
        {
            sb.Append(' ');
            sb.Append(endDate.ToString("yyyy-MM-dd"));
        }

        sb.Append(' ');
        sb.Append(interval);

        _stringRepresentation = sb.ToString();
    }

    private DateOnly BeginDate { get; init; }
    private DateOnly EndDate { get; init; }
    private Interval Interval { get; init; }

    public IEnumerator<DateOnly> ToEnumerator()
    {
        return Recurrence.Daily(BeginDate, EndDate, BeginDate, EndDate, Interval);
    }

    public IEnumerator<DateOnly> ToEnumerator(int takeCount)
    {
        return Recurrence.Daily(BeginDate, BeginDate, takeCount, Interval);
    }

    public IEnumerator<DateOnly> ToEnumerator(DateOnly fromDate, int takeCount)
    {
        return Recurrence.Daily(BeginDate, fromDate, takeCount, Interval);
    }

    public IEnumerator<DateOnly> ToEnumerator(DateOnly fromDate, DateOnly toDate)
    {
        return Recurrence.Daily(BeginDate, EndDate, fromDate, toDate, Interval);
    }

    public bool Contains(DateOnly date)
    {
        if (date < BeginDate || EndDate < date) return false;

        return (date.DayNumber - BeginDate.DayNumber) % Interval == 0;
    }

    public new string ToString()
    {
        return _stringRepresentation;
    }
}