using System.Text;

namespace DateRecurrenceR.Objects.Internal;

internal sealed class MonthlyObjectByDayOfWeek : IRecurrenceObject
{
    private readonly string _stringRepresentation;

    public MonthlyObjectByDayOfWeek(DateOnly beginDate,
        DateOnly endDate,
        DayOfWeek dayOfWeek,
        NumberOfWeek numberOfWeek,
        int interval)
    {
        BeginDate = beginDate;
        EndDate = endDate;
        DayOfWeek = dayOfWeek;
        NumberOfWeek = numberOfWeek;
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
        sb.Append('W');
        sb.Append((int)dayOfWeek + 1);
        sb.Append('/');
        sb.Append((int)numberOfWeek + 1);

        _stringRepresentation = sb.ToString();
    }

    public DateOnly BeginDate { get; }
    public DateOnly EndDate { get; }
    public DayOfWeek DayOfWeek { get; }
    public NumberOfWeek NumberOfWeek { get; }
    public int Interval { get; }

    public IEnumerator<DateOnly> ToEnumerator()
    {
        return Recurrence.Monthly(BeginDate, EndDate, BeginDate, EndDate, DayOfWeek, NumberOfWeek, Interval);
    }

    public IEnumerator<DateOnly> ToEnumerator(int takeCount)
    {
        return Recurrence.Monthly(BeginDate, BeginDate, takeCount, DayOfWeek, NumberOfWeek, Interval);
    }

    public IEnumerator<DateOnly> ToEnumerator(DateOnly fromDate, int takeCount)
    {
        return Recurrence.Monthly(BeginDate, fromDate, takeCount, DayOfWeek, NumberOfWeek, Interval);
    }

    public IEnumerator<DateOnly> ToEnumerator(DateOnly fromDate, DateOnly toDate)
    {
        return Recurrence.Monthly(BeginDate, EndDate, fromDate, toDate, DayOfWeek, NumberOfWeek, Interval);
    }

    public new string ToString()
    {
        return _stringRepresentation;
    }
}