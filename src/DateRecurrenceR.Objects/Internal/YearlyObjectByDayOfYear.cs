using System.Text;

namespace DateRecurrenceR.Objects.Internal;

internal sealed class YearlyObjectByDayOfYear : IRecurrenceObject
{
    private readonly string _stringRepresentation;

    public YearlyObjectByDayOfYear(DateOnly beginDate,
        DateOnly endDate,
        int dayOfYear,
        int interval)
    {
        BeginDate = beginDate;
        EndDate = endDate;
        DayOfYear = dayOfYear;
        Interval = interval;
        
        var sb = new StringBuilder("Y");
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
        sb.Append(dayOfYear);

        _stringRepresentation = sb.ToString();
    }

    public DateOnly BeginDate { get; }
    public DateOnly EndDate { get; }
    public int DayOfYear { get; }
    public int Interval { get; }

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
}