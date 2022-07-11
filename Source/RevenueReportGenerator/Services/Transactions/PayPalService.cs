using Mapster;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RevenueReportGenerator.DTO;
using RevenueReportGenerator.PayPal.Contract;

namespace RevenueReportGenerator.Services.Transactions;

internal class PayPalService : ITransactionService
{
    private readonly IPayPalApi _payPalApi;

    public PayPalService(IPayPalApi payPalApi)
    {
        _payPalApi = payPalApi;
    }

    public async Task<EarningHistoryDto> GetEarningTransactions(int year, int month)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddTicks(-1);

        var request = new PayPalTransactionsRequest
        {
            StartDate = startDate.ToPayPalFormat(),
            EndDate = endDate.ToPayPalFormat(),
            Fields = "transaction_info,payer_info",
            PageSize = 100,
            Page = 1
        };

        var transactions = await GetTransactions(request).ToListAsync();

        var earningTransactions = transactions
            .SelectMany(tds => tds)
            .Where(td => td.IsPositiveAndCompleted())
            .Select(td => td.Adapt<EarningDto>())
            .ToList();

        var earningHistory = new EarningHistoryDto
        {
            Year = year,
            Month = month,
            Earnings = earningTransactions
        };

        return earningHistory;
    }

    private async IAsyncEnumerable<IEnumerable<TransactionDetails>> GetTransactions(PayPalTransactionsRequest request)
    {
        do
        {
            var responseString = await _payPalApi.GetTransactions(request);
            var response = JsonConvert.DeserializeObject<TransactionsResponse>(responseString,
                new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() }
                });

            var transactionDetails = (response?.TransactionDetails ?? Enumerable.Empty<TransactionDetails>()).ToList();
            yield return transactionDetails;

            if (request.Page == response?.TotalPages || !transactionDetails.Any())
                break;

            request.Page++;
        } while (true);
    }
}
