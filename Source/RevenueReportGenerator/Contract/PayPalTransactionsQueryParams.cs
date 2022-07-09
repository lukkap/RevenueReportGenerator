using Refit;

namespace RevenueReportGenerator.Contract;

internal record PayPalTransactionsQueryParams
{
    [AliasAs("start_date")]
    public string StartDate { get; init; }
    [AliasAs("end_date")]
    public string EndDate { get; init; }
    [AliasAs("fields")]
    public string Fields { get; init; }
    [AliasAs("page_size")]
    public int PageSize { get; set; }
    [AliasAs("page")]
    public int Page { get; set; }
}
