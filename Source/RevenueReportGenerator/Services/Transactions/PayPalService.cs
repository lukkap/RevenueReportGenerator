using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RevenueReportGenerator.DTO;
using RevenueReportGenerator.PayPal.Contract;

namespace RevenueReportGenerator.Services.Transactions;

internal class PayPalService : ITransactionService
{
    private readonly IPayPalApi _payPalApi;

    private const string CompletedStatus = "S";

    public PayPalService(IPayPalApi payPalApi)
    {
        _payPalApi = payPalApi;
    }

    public async Task<IEnumerable<EarningDto>> GetEarningTransactions(DateTime startDate, DateTime endDate)
    {
        var queryParams = new PayPalTransactionsRequest
        {
            StartDate = startDate.ToPayPalFormat(),
            EndDate = endDate.ToPayPalFormat(),
            Fields = "transaction_info,payer_info",
            PageSize = 100,
            Page = 1
        };

        var transactions = await GetTransactions(queryParams).ToListAsync();

        var earningTransactions = transactions
            .SelectMany(tds => tds)
            .Select(td => td.TransactionInfo)
            .Where(ti => ti.Amount?.Value > 0 &&
                         ti.Subject is not null &&
                         ti.Status == CompletedStatus)
            .ToList() ?? Enumerable.Empty<TransactionInfo>();

        // TODO: Map earningTransactions to IEnumerable<EarningDto> 

        throw new NotImplementedException();
    }

    private async IAsyncEnumerable<IEnumerable<TransactionDetails>> GetTransactions(PayPalTransactionsRequest queryParams)
    {
        do
        {
            var responseString = await _payPalApi.GetTransactions(queryParams);
            var response = JsonConvert.DeserializeObject<TransactionsResponse>(responseString,
                new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() }
                });

            var transactionDetails = (response?.TransactionDetails ?? Enumerable.Empty<TransactionDetails>()).ToList();
            yield return transactionDetails;

            if (queryParams.Page == response?.TotalPages || !transactionDetails.Any())
                break;

            queryParams.Page++;
        } while (true);
    }
}
