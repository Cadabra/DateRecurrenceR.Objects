using System.Diagnostics.CodeAnalysis;

namespace DateRecurrenceR.Objects;

internal struct RecurrenceModelStringParser
{
    public static RecurrenceObject Parse(string str)
    {
        if (TryParse(str, out var model))
        {
            return model;
        }

        throw new FormatException();
    }

    public static bool TryParse(string str, [NotNullWhen(true)] out RecurrenceObject? model)
    {
        if (str.Length < 2)
        {
            model = default;
            return false;
        }

        var startIndex = 2;
        var source = str.AsSpan();

        var shortType = TryGetRecurrenceType(source[0]);
        if (shortType == ShortType.None)
        {
            model = default;
            return false;
        }

        DateOnly? beginDate = null;
        DateOnly endDate = DateOnly.MaxValue;
        int? interval = null;
        DayOfWeek? dayOfWeek = null;
        int? dayOfMonth = null;
        int? dayOfYear = null;
        NumberOfWeek? numberOfWeek = null;
        int? monthOfYear = null;
        DayOfWeek? firstDayOfWeek = null;
        WeekDays? weekDays = null;

        ReadOnlySpan<char> token;
        while ((token = ReadNext(ref source, ref startIndex)).Length > 0)
        {
            SkipSpaces(ref source, ref startIndex);
            var c = token[0];

            switch (c)
            {
                case 'b':
                case 'B':
                    beginDate = DateOnly.FromDayNumber(int.Parse(token[1..]));
                    break;
                case 'e':
                case 'E':
                    endDate = DateOnly.FromDayNumber(int.Parse(token[1..]));
                    break;
                case 'i':
                case 'I':
                    interval = int.Parse(token[1..]);
                    break;
                case 'd':
                case 'D':
                    weekDays = WeekDaysParse(token[1..]);
                    dayOfMonth = int.Parse(token[1..]);
                    dayOfYear = int.Parse(token[1..]);
                    break;
                case 'w':
                case 'W':
                {
                    var index = 1;
                    var val1 = ReadNextSlash(ref token, ref index);
                    if (val1.Length > 0)
                    {
                        dayOfWeek = (DayOfWeek) int.Parse(val1);
                        index++;
                    }
                    else
                    {
                        break;
                    }

                    var val2 = ReadNextSlash(ref token, ref index);
                    if (val2.Length > 0)
                    {
                        numberOfWeek = (NumberOfWeek) int.Parse(val2);
                        index++;
                    }
                    else
                    {
                        break;
                    }

                    var val3 = ReadNextSlash(ref token, ref index);
                    if (val3.Length > 0)
                    {
                        monthOfYear = int.Parse(val3);
                    }

                    break;
                }
                case 'm':
                case 'M':
                {
                    var index = 1;
                    var val1 = ReadNextSlash(ref token, ref index);
                    if (val1.Length > 0)
                    {
                        dayOfMonth = int.Parse(val1);
                        index++;
                    }
                    else
                    {
                        break;
                    }

                    var val2 = ReadNextSlash(ref token, ref index);
                    if (val2.Length > 0)
                    {
                        monthOfYear = int.Parse(val2);
                        index++;
                    }

                    break;
                }
                case 'f':
                case 'F':
                    firstDayOfWeek = (DayOfWeek) int.Parse(token[1..]);
                    break;
            }
        }

        switch (shortType)
        {
            case ShortType.Daily:
                if (beginDate.HasValue && interval.HasValue)
                {
                    model = new RecurrenceObject(
                        beginDate.Value,
                        endDate,
                        interval.Value);
                    return true;
                }

                break;
            case ShortType.Weekly:
                if (beginDate.HasValue && interval.HasValue && weekDays != null && firstDayOfWeek.HasValue)
                {
                    model = new RecurrenceObject(
                        beginDate.Value,
                        endDate,
                        interval.Value,
                        weekDays,
                        firstDayOfWeek.Value);
                    return true;
                }

                break;
            case ShortType.Monthly:
            {
                if (beginDate.HasValue && interval.HasValue)
                {
                    if (dayOfMonth.HasValue)
                    {
                        model = new RecurrenceObject(beginDate.Value, endDate, interval.Value, dayOfMonth.Value,
                            PeriodOf.Month);
                        return true;
                    }

                    if (dayOfWeek.HasValue && numberOfWeek.HasValue)
                    {
                        model = new RecurrenceObject(beginDate.Value, endDate, interval.Value, dayOfWeek.Value,
                            numberOfWeek.Value);
                        return true;
                    }
                }

                break;
            }
            case ShortType.Yearly:
                if (beginDate.HasValue && interval.HasValue)
                {
                    if (dayOfYear.HasValue)
                    {
                        model = new RecurrenceObject(beginDate.Value, endDate, interval.Value, dayOfYear.Value,
                            PeriodOf.Year);
                        return true;
                    }

                    if (dayOfWeek.HasValue && numberOfWeek.HasValue && monthOfYear.HasValue)
                    {
                        model = new RecurrenceObject(beginDate.Value, endDate, interval.Value, dayOfWeek.Value,
                            numberOfWeek.Value, monthOfYear.Value);
                        return true;
                    }

                    if (dayOfMonth.HasValue && monthOfYear.HasValue)
                    {
                        model = new RecurrenceObject(beginDate.Value, endDate, interval.Value, dayOfMonth.Value,
                            monthOfYear.Value);
                        return true;
                    }
                }

                break;
        }

        model = default;
        return false;
    }

    private static ReadOnlySpan<char> ReadNext(ref ReadOnlySpan<char> span, ref int startIndex)
    {
        for (var i = startIndex; i < span.Length; i++)
        {
            if (!char.IsWhiteSpace(span[i])) continue;

            var length = i - startIndex;
            var res = span.Slice(startIndex, length);
            startIndex += length;
            return res;
        }

        {
            var length = span.Length - startIndex;
            var res = span.Slice(startIndex, length);
            startIndex += length;
            return res;
        }
    }

    private static ReadOnlySpan<char> ReadNextSlash(ref ReadOnlySpan<char> span, ref int startIndex)
    {
        for (var i = startIndex; i < span.Length; i++)
        {
            if (span[i] != '/') continue;

            var length = i - startIndex;
            var res = span.Slice(startIndex, length);
            startIndex += length;
            return res;
        }

        if (startIndex < span.Length)
        {
            var length = span.Length - startIndex;
            var res = span.Slice(startIndex, length);
            startIndex += length;
            return res;
        }

        return ReadOnlySpan<char>.Empty;
    }

    private static void SkipSpaces(ref ReadOnlySpan<char> span, ref int startIndex)
    {
        for (; startIndex < span.Length; startIndex++)
        {
            if (!char.IsWhiteSpace(span[startIndex]))
            {
                return;
            }
        }
    }

    private static WeekDays WeekDaysParse(ReadOnlySpan<char> span)
    {
        var wd = new WeekDays(
            span.Contains('1'),
            span.Contains('2'),
            span.Contains('3'),
            span.Contains('4'),
            span.Contains('5'),
            span.Contains('6'),
            span.Contains('7'));

        return wd;
    }

    private static ShortType TryGetRecurrenceType(in char t)
    {
        return t switch
        {
            'd' or 'D' => ShortType.Daily,
            'w' or 'W' => ShortType.Weekly,
            'm' or 'M' => ShortType.Monthly,
            'y' or 'Y' => ShortType.Yearly,
            _ => ShortType.None
        };
    }

    private enum ShortType
    {
        None,
        Daily,
        Weekly,
        Monthly,
        Yearly
    }
}