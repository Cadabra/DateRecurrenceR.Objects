using System.Text;
using DateRecurrenceR.Core;

namespace DateRecurrenceR.Objects.Internal;

internal sealed class WeeklyObject : IRecurrenceObject, IRecurrence
{
    private readonly string _stringRepresentation;

    public WeeklyObject(DateOnly beginDate,
        DateOnly endDate,
        WeekDays weekDays,
        DayOfWeek firstDayOfWeek,
        Interval interval)
    {
        BeginDate = beginDate;
        EndDate = endDate;
        WeekDays = weekDays;
        FirstDayOfWeek = firstDayOfWeek;
        Interval = interval;

        var daysCount = 0;
        var sb = new StringBuilder("W");
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
        for (var i = 0; i < 7; i++)
        {
            if (weekDays[(DayOfWeek) i])
            {
                if (daysCount > 0)
                {
                    sb.Append(',');
                }

                sb.Append(TwoLetterDayNames.GetByIndex(i));
                daysCount++;
            }
        }

        sb.Append(' ');
        sb.Append(TwoLetterDayNames.GetByIndex((int) firstDayOfWeek));

        _stringRepresentation = sb.ToString();
    }

    public DateOnly BeginDate { get; }
    public DateOnly EndDate { get; }
    public WeekDays WeekDays { get; }
    public DayOfWeek FirstDayOfWeek { get; }
    public Interval Interval { get; }

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

    public bool Contains(DateOnly date)
    {
        if (!WeekDays[date.DayOfWeek]) return false;

        if (date < BeginDate || EndDate < date) return false;

        if ((date.DayNumber - BeginDate.DayNumber) % (Interval * 7) > 7) return false;
        
        return true;
    }
}