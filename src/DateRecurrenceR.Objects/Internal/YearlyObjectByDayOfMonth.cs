using System.Text;

namespace DateRecurrenceR.Objects.Internal;

internal sealed class YearlyObjectByDayOfMonth : IRecurrenceObject
{
    private readonly string _stringRepresentation;

    public YearlyObjectByDayOfMonth(DateOnly beginDate,
        DateOnly endDate,
        int dayOfMonth,
        int monthOfYear,
        int interval)
    {
        BeginDate = beginDate;
        EndDate = endDate;
        DayOfMonth = dayOfMonth;
        MonthOfYear = monthOfYear;
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
        sb.Append('M');
        sb.Append(dayOfMonth);
        sb.Append('/');
        sb.Append(monthOfYear);

        _stringRepresentation = sb.ToString();
    }

    public DateOnly BeginDate { get; }
    public DateOnly EndDate { get; }
    public int DayOfMonth { get; }
    public int MonthOfYear { get; }
    public int Interval { get; }

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
}