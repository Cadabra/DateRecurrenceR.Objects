# DateRecurrenceR.Objects

[![Release](https://github.com/Cadabra/DateRecurrenceR.Objects/actions/workflows/release.yml/badge.svg)](https://github.com/Cadabra/DateRecurrenceR.Objects/actions/workflows/release.yml) [![Nuget](https://img.shields.io/nuget/v/DateRecurrenceR.Objects?logo=NuGet)](https://www.nuget.org/packages/DateRecurrenceR.Objects)

**DateRecurrenceR.Objects** simplifies the management of recurring dates by providing both object-oriented and
string-based representations, making it easier to store, manipulate, and interpret recurrence patterns.

## Key Features

* **Object representation:** Use recurrence patterns as objects, providing a robust and intuitive way to handle
  recurring dates.
* **String representation:** Convert recurrence patterns to strings for easy storage and transmission.
* **Parse object:** Parse recurrence patterns from strings, enabling flexible and dynamic date handling.

## IRecurrenceObject interface

The RecurrenceObject implements the interface of IRecurrenceObject.

More information about [parameters](https://github.com/Cadabra/DateRecurrenceR?tab=readme-ov-file#all-parameters)
and [subranges](https://github.com/Cadabra/DateRecurrenceR?tab=readme-ov-file#subranges).

| method                         | description                                                                                                               |
|--------------------------------|---------------------------------------------------------------------------------------------------------------------------|
| ToEnumerator()                 | Returns an enumerator for the full recurrence patterns, where `fromDate` equals `beginDate` and `toDate` equals `endDate` |
| ToEnumerator(count)            | Returns an enumerator for a subrange, where `fromDate` equals `beginDate` and `toDate` equals `DateOnly.MaxValue`         |
| ToEnumerator(fromDate, count)  | Returns an enumerator for a subrange, where `toDate` equals `DateOnly.MaxValue`                                           |
| ToEnumerator(fromDate, toDate) | Returns an enumerator for a subrange.                                                                                     |

All these methods return an instance of `IEnumerator<DateOnly>`.

## String representation format

Example of string: `"M B0 E3652058 I1 D256"`

### First token

| token      | description        |
|------------|--------------------|
| 'd' or 'D' | Daily recurrence   |
| 'w' or 'W' | Weekly recurrence  |
| 'm' or 'M' | Monthly recurrence |
| 'y' or 'Y' | Yearly recurrence  |

### Common tokens

| token      | format | description            |
|------------|--------|------------------------|
| 'b' or 'B' | D1     | The number of the day. |
| 'e' or 'E' | E1     | The number of the day. |
| 'i' or 'I' | I1     | The interval.          |

### Weekly tokens

| token      | format | description                |
|------------|--------|----------------------------|
| 'd' or 'D' | D1     | The day of the week.       |
| 'f' or 'F' | F1     | The first day of the week. |

### Monthly tokens

| starts with | format | description                                             |
|-------------|--------|---------------------------------------------------------|
| 'd' or 'D'  | D1     | The day of the month.                                   |
| 'w' or 'w'  | W1/2   | The number of the day of week / the number of the week. |

### Yearly tokens

| starts with | format | description                                                                       |
|-------------|--------|-----------------------------------------------------------------------------------|
| 'd' or 'D'  | D1     | The day of the year.                                                              |
| 'm' or 'M'  | M1/2   | The number of the day of month / the number of the month.                         |
| 'w' or 'W'  | W1/2/3 | The number of the day of week / the number of the week / the number of the month. |



## Examples of Use

Create an instance of RecurrenceObject via constructor:

```csharp
var obj = new RecurrenceObject(DateOnly.MinValue, DateOnly.MaxValue, interval);
```

Create an instance of RecurrenceObject by parsing of string:

```csharp
var obj = RecurrenceObject.Parse("M B0 E3652058 I1 D256");
```

## Installing DateRecurrenceR.Objects

You can install DateRecurrenceR via [NuGet](https://www.nuget.org/packages/DateRecurrenceR.Objects):

**Package Manager Console:**

```shell
Install-Package DateRecurrenceR.Objects
```

**.NET Core CLI:**

```shell
dotnet add package DateRecurrenceR.Objects
```