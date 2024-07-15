using System.Globalization;
using CsvHelper;
using RevenueReportGenerator.DTO;

namespace RevenueReportGenerator.Services.Report;

internal class CsvReportGenerator : IReportGenerator
{
    public string GenerateEarningsReport(EarningHistoryDto earningHistory)
    {
        var folderPath = Environment.GetEnvironmentVariable("outputpath") ?? Environment.CurrentDirectory;
        var yearAndMonth = $"{earningHistory.Year}-{earningHistory.Month:00}";
        var filePath = Path.Combine(folderPath + $"\\{yearAndMonth}", $"{yearAndMonth}.csv");

        using (var writer = new StreamWriter(filePath))
        using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
        {
            csv.WriteRecords(earningHistory.Earnings);
        }

        return filePath;
    }
}
