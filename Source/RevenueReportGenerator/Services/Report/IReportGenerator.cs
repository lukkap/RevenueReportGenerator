using RevenueReportGenerator.DTO;

namespace RevenueReportGenerator.Services.Report;

internal interface IReportGenerator
{
    public string GenerateEarningsReport(EarningHistoryDto earnings);
}
