namespace DateRecurrenceR.Objects;

public interface IRecurrence
{
    bool Contains(DateOnly date);
}