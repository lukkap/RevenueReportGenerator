using System.Globalization;
using CsvHelper;
using RevenueReportGenerator.DTO;

namespace RevenueReportGenerator.Services.Report;

internal class CsvReportGenerator : IReportGenerator
{
    public string GenerateEarningsReport(EarningHistoryDto earningHistory)
    {
        var filePath = Path.Combine(Environment.CurrentDirectory, $"{earningHistory.Year}-{earningHistory.Month}.csv");

        using (var writer = new StreamWriter(filePath))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(earningHistory.Earnings);
        }

        return filePath;
    }
}
