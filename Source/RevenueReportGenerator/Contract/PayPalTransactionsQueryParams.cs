using Refit;

namespace RevenueReportGenerator.Contract;

internal record PayPalTransactionsQueryParams
{
    [AliasAs("start_date")]
    public string StartDate { get; init; }
    [AliasAs("end_date")]
    public string EndDate { get; init; }
    public string Fields { get; init; }
}
