using System.Text;

namespace DateRecurrenceR.Objects.Internal;

internal sealed class MonthlyObjectByDayOfMonth : IRecurrenceObject
{
    private readonly string _stringRepresentation;

    public MonthlyObjectByDayOfMonth(
        DateOnly beginDate,
        DateOnly endDate,
        int dayOfMonth,
        int interval)
    {
        BeginDate = beginDate;
        EndDate = endDate;
        DayOfMonth = dayOfMonth;
        Interval = interval;
        
        var sb = new StringBuilder("M");
        sb.Append(' ');
        sb.Append('B');
        sb.Append(beginDate.DayNumber);
        sb.Append(' ');
        sb.Append('E');
        sb.Append(endDate.DayNumber);
        sb.Append(' ');
        sb.Append('I');
        sb.Append(interval);
        sb.Append(' ');
        sb.Append('D');
        sb.Append(dayOfMonth);

        _stringRepresentation = sb.ToString();
    }

    public DateOnly BeginDate { get; }
    public DateOnly EndDate { get; }
    public int DayOfMonth { get; }
    public int Interval { get; }

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
}