using System.Diagnostics.CodeAnalysis;
using DateRecurrenceR.Core;

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

    public static bool TryParse(ReadOnlySpan<char> source, [NotNullWhen(true)] out RecurrenceObject? model)
    {
        if (source.Length < 2)
        {
            model = default;
            return false;
        }

        var startIndex = 2;

        var shortType = TryGetRecurrenceType(source[0]);
        if (shortType == ShortType.None)
        {
            model = default;
            return false;
        }

        DateOnly beginDate = DateOnly.MinValue;
        DateOnly endDate = DateOnly.MaxValue;
        Interval interval = new Interval(1);
        DayOfWeek? dayOfWeek = null;
        DayOfMonth? dayOfMonth = null;
        DayOfYear? dayOfYear = null;
        NumberOfWeek? numberOfWeek = null;
        MonthOfYear? monthOfYear = null;
        DayOfWeek? firstDayOfWeek = null;
        WeekDays? weekDays = null;

#if NET6_0
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
                    interval = new Interval(int.Parse(token[1..]));
                    break;
                case 'd':
                case 'D':
                    weekDays = WeekDaysParse(token[1..]);
                    dayOfMonth = new DayOfMonth(int.Parse(token[1..]));
                    dayOfYear = new DayOfYear(int.Parse(token[1..]));
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
                        monthOfYear = new MonthOfYear(int.Parse(val3));
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
                        dayOfMonth = new DayOfMonth(int.Parse(val1));
                        index++;
                    }
                    else
                    {
                        break;
                    }

                    var val2 = ReadNextSlash(ref token, ref index);
                    if (val2.Length > 0)
                    {
                        monthOfYear = new MonthOfYear(int.Parse(val2));
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
#endif
#if NET8_0_OR_GREATER
        var token = ReadNext(ref source, ref startIndex);
        while (token.Length > 0)
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
                    interval = new Interval(int.Parse(token[1..]));
                    break;
                case 'd':
                case 'D':
                    weekDays = WeekDaysParse(token[1..]);
                    dayOfMonth = new DayOfMonth(int.Parse(token[1..]));
                    dayOfYear = new DayOfYear(int.Parse(token[1..]));
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
                        monthOfYear = new MonthOfYear(int.Parse(val3));
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
                        dayOfMonth = new DayOfMonth(int.Parse(val1));
                        index++;
                    }
                    else
                    {
                        break;
                    }

                    var val2 = ReadNextSlash(ref token, ref index);
                    if (val2.Length > 0)
                    {
                        monthOfYear = new MonthOfYear(int.Parse(val2));
                        index++;
                    }

                    break;
                }
                case 'f':
                case 'F':
                    firstDayOfWeek = (DayOfWeek) int.Parse(token[1..]);
                    break;
            }

            token = ReadNext(ref source, ref startIndex);
        }
#endif

        switch (shortType)
        {
            case ShortType.Daily:
                model = new RecurrenceObject(
                    beginDate,
                    endDate,
                    interval);
                return true;
            case ShortType.Weekly:
                if (weekDays != null && firstDayOfWeek.HasValue)
                {
                    model = new RecurrenceObject(
                        beginDate,
                        endDate,
                        interval,
                        weekDays,
                        firstDayOfWeek.Value);
                    return true;
                }

                break;
            case ShortType.Monthly:
            {
                if (dayOfMonth.HasValue)
                {
                    model = new RecurrenceObject(beginDate, endDate, interval, dayOfMonth.Value);
                    return true;
                }

                if (dayOfWeek.HasValue && numberOfWeek.HasValue)
                {
                    model = new RecurrenceObject(beginDate, endDate, interval, dayOfWeek.Value,
                        numberOfWeek.Value);
                    return true;
                }

                break;
            }
            case ShortType.Yearly:
                if (dayOfYear.HasValue)
                {
                    model = new RecurrenceObject(beginDate, endDate, interval, dayOfYear.Value);
                    return true;
                }

                if (dayOfWeek.HasValue && numberOfWeek.HasValue && monthOfYear.HasValue)
                {
                    model = new RecurrenceObject(beginDate, endDate, interval, dayOfWeek.Value,
                        numberOfWeek.Value, monthOfYear.Value);
                    return true;
                }

                if (dayOfMonth.HasValue && monthOfYear.HasValue)
                {
                    model = new RecurrenceObject(beginDate, endDate, interval, dayOfMonth.Value,
                        monthOfYear.Value);
                    return true;
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