namespace RevenueReportGenerator.PayPal.Contract;

public record TransactionsResponse
{
    public IEnumerable<TransactionDetails> TransactionDetails { get; init; }
    public DateTime StartDate { get; init; }
    public int Page { get; init; }
    public int TotalItems { get; init; }
    public int TotalPages { get; init; }
}

public record TransactionDetails
{
    public TransactionInfo? TransactionInfo { get; init; }
    public PayerInfo? PayerInfo { get; init; }
}

public record TransactionInfo
{
    [JsonProperty("transaction_id")]
    public string Id { get; init; }
    [JsonProperty("transaction_initiation_date")]
    public DateTime? Date { get; init; }
    [JsonProperty("transaction_amount")]
    public TransactionAmount? Amount { get; init; }
    [JsonProperty("transaction_status")]
    public string? Status { get; init; }
    [JsonProperty("transaction_subject")]
    public string? Subject { get; init; }
    [JsonProperty("transaction_note")]
    public string? Note { get; init; }
}

public record TransactionAmount
{
    public string CurrencyCode { get; init; }
    public double Value { get; init; }
}

public record PayerInfo
{
    [JsonProperty("account_id")]
    public string Id { get; init; }
    [JsonProperty("payer_name")]
    public PayerName? Name { get; init; }
    public string? EmailAddress { get; init; }
    public string? CountryCode { get; init; }
}

public record PayerName
{
    public string AlternateFullName { get; init; }
}
