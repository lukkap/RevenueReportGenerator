using RevenueReportGenerator.Contract;
using RevenueReportGenerator.DTO;

namespace RevenueReportGenerator.Services;

internal class PayPalService : IPaymentService
{
    private readonly IPayPalApi _payPalApi;

    public PayPalService(IPayPalApi payPalApi)
    {
        _payPalApi = payPalApi;
    }

    public async Task<IEnumerable<TransactionDto>> GetTransactions(DateTime startDate, DateTime endDate)
    {
        var queryParams = new PayPalTransactionsQueryParams
        {
            StartDate = startDate.ToPayPalFormat(),
            EndDate = endDate.ToPayPalFormat(),
            Fields = "all"
        };

        var responseString = await _payPalApi.GetTransactions(queryParams);

        // TODO: Deserialize response
        throw new NotImplementedException();
    }
}
