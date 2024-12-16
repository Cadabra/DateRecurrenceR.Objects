using System.Text;
using DateRecurrenceR.Core;

namespace DateRecurrenceR.Objects.Internal;

internal sealed class MonthlyObjectByDayOfWeek : IRecurrenceObject, IRecurrence
{
    private readonly string _stringRepresentation;

    public MonthlyObjectByDayOfWeek(DateOnly beginDate,
        DateOnly endDate,
        DayOfWeek dayOfWeek,
        NumberOfWeek numberOfWeek,
        Interval interval)
    {
        BeginDate = beginDate;
        EndDate = endDate;
        DayOfWeek = dayOfWeek;
        NumberOfWeek = numberOfWeek;
        Interval = interval;

        var sb = new StringBuilder("M");
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
        sb.Append(TwoLetterDayNames.GetByIndex((int) dayOfWeek));
        sb.Append(' ');
        sb.Append(numberOfWeek);

        _stringRepresentation = sb.ToString();
    }

    public DateOnly BeginDate { get; }
    public DateOnly EndDate { get; }
    public DayOfWeek DayOfWeek { get; }
    public NumberOfWeek NumberOfWeek { get; }
    public Interval Interval { get; }

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

    public bool Contains(DateOnly date)
    {
        if (date.DayOfWeek != DayOfWeek) return false;

        if (date < BeginDate || EndDate < date) return false;

        if (((date.Year * 12 + date.Month) - (BeginDate.Year * 12 + BeginDate.Month)) % Interval > 0) return false;

        if ((Interval - 1) * 7 < date.Day || date.Day < Interval * 7) return true;

        return false;
    }
}