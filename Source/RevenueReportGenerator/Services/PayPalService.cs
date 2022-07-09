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
        var queryParams = new PayPalTransactionsQueryParams
        {
            StartDate = startDate.ToPayPalFormat(),
            EndDate = endDate.ToPayPalFormat(),
            Fields = "all",
            PageSize = 100,
            Page = 1
        };

        var transactions = await GetTransactions(queryParams).ToListAsync();

        var revenueTransactions = transactions
            .SelectMany(tds => tds)
            .Select(td => td.TransactionInfo)
            .Where(ti => ti.Amount?.Value > 0 &&
                         ti.Subject is not null &&
                         ti.Status == CompletedStatus)
            .ToList() ?? Enumerable.Empty<TransactionInfoDto>();

        return revenueTransactions;
    }

    private async IAsyncEnumerable<IEnumerable<TransactionDetailsDto>> GetTransactions(PayPalTransactionsQueryParams queryParams)
    {
        do
        {
            var responseString = await _payPalApi.GetTransactions(queryParams);
            var response = JsonConvert.DeserializeObject<TransactionDto>(responseString,
                new JsonSerializerSettings {
                    ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() }
                });

            var transactionDetails = (response?.TransactionDetails ?? Enumerable.Empty<TransactionDetailsDto>()).ToList();
            yield return transactionDetails;

            if (queryParams.Page == response?.TotalPages || !transactionDetails.Any())
                break;

            queryParams.Page++;
        } while (true);
    }
}
