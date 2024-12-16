namespace DateRecurrenceR.Objects.Internal;

internal static class TwoLetterDayNames
{
    private const string Sunday = "Su";
    private const string Monday = "Mo";
    private const string Tuesday = "Tu";
    private const string Wednesday = "We";
    private const string Thursday = "Th";
    private const string Friday = "Fr";
    private const string Saturday = "Sa";
    
    public static string GetByIndex(int index)
    {
        return index switch
        {
            0 => Sunday,
            1 => Monday,
            2 => Tuesday,
            3 => Wednesday,
            4 => Thursday,
            5 => Friday,
            6 => Saturday,
            _ => throw new IndexOutOfRangeException()
        };
    }
}