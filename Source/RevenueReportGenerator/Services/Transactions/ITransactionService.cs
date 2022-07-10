using RevenueReportGenerator.DTO;

namespace RevenueReportGenerator.Services.Transactions;

internal interface ITransactionService
{
    Task<IEnumerable<EarningDto>> GetEarningTransactions(DateTime startDate, DateTime endDate);
}
