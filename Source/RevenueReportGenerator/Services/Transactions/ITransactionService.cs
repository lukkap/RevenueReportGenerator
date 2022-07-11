using RevenueReportGenerator.DTO;

namespace RevenueReportGenerator.Services.Transactions;

internal interface ITransactionService
{
    Task<EarningHistoryDto> GetEarningTransactions(int year, int month);
}
