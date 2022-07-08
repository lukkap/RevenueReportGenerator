using RevenueReportGenerator.DTO;

namespace RevenueReportGenerator.Services;

internal interface IPaymentService
{
    Task<IEnumerable<TransactionInfoDto>> GetRevenueTransactions(DateTime startDate, DateTime endDate);
}
