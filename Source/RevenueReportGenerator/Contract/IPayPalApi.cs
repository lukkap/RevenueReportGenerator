using Refit;

namespace RevenueReportGenerator.Contract;

internal interface IPayPalApi
{
    [Get("/v1/reporting/transactions")]
    Task<string> GetTransactions(DateTime startDate, DateTime endDate);
}
