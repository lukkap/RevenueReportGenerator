using RevenueReportGenerator.DTO;

namespace RevenueReportGenerator.Services;

internal interface IPaymentService
{
    Task<IEnumerable<TransactionDto>> GetTransactions(DateTime startDate, DateTime endDate);
}
