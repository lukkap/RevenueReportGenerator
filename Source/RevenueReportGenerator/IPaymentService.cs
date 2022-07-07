using RevenueReportGenerator.DTO;

namespace RevenueReportGenerator;

internal interface IPaymentService
{
    Task<IEnumerable<TransactionDto>> GetTransactions(DateTime startDate, DateTime endDate);
}
