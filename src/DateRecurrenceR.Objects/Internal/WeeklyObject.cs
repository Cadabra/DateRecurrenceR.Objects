using System.Text;

namespace DateRecurrenceR.Objects.Internal;

internal sealed class WeeklyObject : IRecurrenceObject
{
    private readonly string _stringRepresentation;

    public WeeklyObject(DateOnly beginDate,
        DateOnly endDate,
        WeekDays weekDays,
        DayOfWeek firstDayOfWeek,
        int interval)
    {
        BeginDate = beginDate;
        EndDate = endDate;
        WeekDays = weekDays;
        FirstDayOfWeek = firstDayOfWeek;
        Interval = interval;

        var sb = new StringBuilder("W");
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
        for (var i = 0; i < 7; i++)
        {
            if (weekDays[(DayOfWeek) i])
            {
                sb.Append(i + 1);
            }
        }
        sb.Append(' ');
        sb.Append('F');
        sb.Append((int)firstDayOfWeek + 1);

        _stringRepresentation = sb.ToString();
    }

    public DateOnly BeginDate { get; }
    public DateOnly EndDate { get; }
    public WeekDays WeekDays { get; }
    public DayOfWeek FirstDayOfWeek { get; }
    public int Interval { get; }

    public IEnumerator<DateOnly> ToEnumerator()
    {
        return Recurrence.Weekly(BeginDate, EndDate, BeginDate, EndDate, WeekDays, FirstDayOfWeek, Interval);
    }

    public IEnumerator<DateOnly> ToEnumerator(int takeCount)
    {
        return Recurrence.Weekly(BeginDate, BeginDate, takeCount, WeekDays, FirstDayOfWeek, Interval);
    }

    public IEnumerator<DateOnly> ToEnumerator(DateOnly fromDate, int takeCount)
    {
        return Recurrence.Weekly(BeginDate, fromDate, takeCount, WeekDays, FirstDayOfWeek, Interval);
    }

    public IEnumerator<DateOnly> ToEnumerator(DateOnly fromDate, DateOnly toDate)
    {
        return Recurrence.Weekly(BeginDate, EndDate, fromDate, toDate, WeekDays, FirstDayOfWeek, Interval);
    }

    public new string ToString()
    {
        return _stringRepresentation;
    }
}