namespace RevenueReportGenerator;

internal static class Extensions
{
    internal static string ToPayPalFormat(this DateTime dateTime)
        => $"{new DateTimeOffset(dateTime):yyyy-MM-ddTHH:mm:sszz}{new DateTimeOffset(dateTime).Offset:mm}";
}
