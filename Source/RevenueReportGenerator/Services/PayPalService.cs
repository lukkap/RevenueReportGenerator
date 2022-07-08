using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RevenueReportGenerator.Contract;
using RevenueReportGenerator.DTO;

namespace RevenueReportGenerator.Services;

internal class PayPalService : IPaymentService
{
    private readonly IPayPalApi _payPalApi;
    private const string CompletedStatus = "S";

    public PayPalService(IPayPalApi payPalApi)
    {
        _payPalApi = payPalApi;
    }

    public async Task<IEnumerable<TransactionInfoDto>> GetRevenueTransactions(DateTime startDate, DateTime endDate)
    {
        // TODO: Handle paging
        var queryParams = new PayPalTransactionsQueryParams
        {
            StartDate = startDate.ToPayPalFormat(),
            EndDate = endDate.ToPayPalFormat(),
            Fields = "all"
        };

        var responseString = await _payPalApi.GetTransactions(queryParams);

        var transaction = JsonConvert.DeserializeObject<TransactionDto>(responseString,
            new JsonSerializerSettings {
                ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() }
            });

        var revenueTransactions = transaction?.TransactionDetails
            .Select(td => td.TransactionInfo)
            .Where(ti => ti.Amount?.Value > 0 &&
                         ti.Subject is not null &&
                         ti.Status == CompletedStatus)
            .ToList() ?? Enumerable.Empty<TransactionInfoDto>();

        return revenueTransactions;
    }
}
