namespace RevenueReportGenerator.PayPal.Contract;

public interface IPayPalApi
{
    [Get("/v1/reporting/transactions")]
    Task<string> GetTransactions(PayPalTransactionsRequest request);
}
