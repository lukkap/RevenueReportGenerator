using Newtonsoft.Json;

namespace RevenueReportGenerator.Contract;

internal record TransactionsResponse
{
    public IEnumerable<TransactionDetails> TransactionDetails { get; init; }
    public DateTime StartDate { get; init; }
    public int Page { get; init; }
    public int TotalItems { get; init; }
    public int TotalPages { get; init; }
}

internal record TransactionDetails
{
    public TransactionInfo TransactionInfo { get; init; }
}

internal record TransactionInfo
{
    [JsonProperty("transaction_id")]
    public string Id { get; init; }
    [JsonProperty("transaction_initiation_date")]
    public DateTime Date { get; init; }
    [JsonProperty("transaction_amount")]
    public TransactionAmount Amount { get; init; }
    [JsonProperty("transaction_status")]
    public string Status { get; init; }
    [JsonProperty("transaction_subject")]
    public string? Subject { get; init; }
    [JsonProperty("transaction_note")]
    public string? Note { get; init; }
}

internal record TransactionAmount
{
    public string CurrencyCode { get; init; }
    public double Value { get; init; }
}
