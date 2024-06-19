namespace DateRecurrenceR.Objects;

public interface IRecurrenceObject
{
    public IEnumerator<DateOnly> ToEnumerator();
    public IEnumerator<DateOnly> ToEnumerator(int takeCount);
    public IEnumerator<DateOnly> ToEnumerator(DateOnly fromDate, int takeCount);
    public IEnumerator<DateOnly> ToEnumerator(DateOnly fromDate, DateOnly toDate);

    public string ToString();
}