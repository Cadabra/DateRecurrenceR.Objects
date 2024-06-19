using System.Text;

namespace DateRecurrenceR.Objects.Internal;

internal sealed class YearlyObjectByDayOfWeek : IRecurrenceObject
{
    private readonly string _stringRepresentation;

    public YearlyObjectByDayOfWeek(
        DateOnly beginDate,
        DateOnly endDate,
        DayOfWeek dayOfWeek,
        NumberOfWeek numberOfWeek,
        int monthOfYear,
        int interval)
    {
        BeginDate = beginDate;
        EndDate = endDate;
        DayOfWeek = dayOfWeek;
        NumberOfWeek = numberOfWeek;
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
        sb.Append('W');
        sb.Append((int)dayOfWeek + 1);
        sb.Append('/');
        sb.Append((int)numberOfWeek + 1);
        sb.Append('/');
        sb.Append(monthOfYear);

        _stringRepresentation = sb.ToString();
    }

    public DateOnly BeginDate { get; }
    public DateOnly EndDate { get; }
    public DayOfWeek DayOfWeek { get; }
    public NumberOfWeek NumberOfWeek { get; }
    public int MonthOfYear { get; }
    public int Interval { get; }

    public IEnumerator<DateOnly> ToEnumerator()
    {
        return Recurrence.Yearly(BeginDate, EndDate, BeginDate, EndDate, DayOfWeek, NumberOfWeek, MonthOfYear, Interval);
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
}