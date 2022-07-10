using RevenueReportGenerator.DTO;

namespace RevenueReportGenerator.Services;

internal interface IPaymentService
{
    Task<IEnumerable<EarningDto>> GetEarningTransactions(DateTime startDate, DateTime endDate);
}
