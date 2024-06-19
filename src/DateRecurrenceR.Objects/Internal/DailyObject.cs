using System.Text;

namespace DateRecurrenceR.Objects.Internal;

internal sealed class DailyObject : IRecurrenceObject
{
    private readonly string _stringRepresentation;

    public DailyObject(DateOnly beginDate, DateOnly endDate, int interval)
    {
        BeginDate = beginDate;
        EndDate = endDate;
        Interval = interval;

        var sb = new StringBuilder("D");
        sb.Append(' ');
        sb.Append('B');
        sb.Append(beginDate.DayNumber);
        sb.Append(' ');
        sb.Append('E');
        sb.Append(endDate.DayNumber);
        sb.Append(' ');
        sb.Append('I');
        sb.Append(interval);

        _stringRepresentation = sb.ToString();
    }

    private DateOnly BeginDate { get; init; }
    private DateOnly EndDate { get; init; }
    private int Interval { get; init; }

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

    public new string ToString()
    {
        return _stringRepresentation;
    }
}