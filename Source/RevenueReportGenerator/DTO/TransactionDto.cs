using Newtonsoft.Json;

namespace RevenueReportGenerator.DTO;

internal record TransactionDto
{
    public IEnumerable<TransactionDetailsDto> TransactionDetails { get; init; }
    public DateTime StartDate { get; init; }
    public int Page { get; init; }
    public int TotalItems { get; init; }
    public int TotalPages { get; init; }
}

internal record TransactionDetailsDto
{
    public TransactionInfoDto TransactionInfo { get; init; }
}

internal record TransactionInfoDto
{
    [JsonProperty("transaction_id")]
    public string Id { get; init; }
    [JsonProperty("transaction_initiation_date")]
    public DateTime Date { get; init; }
    [JsonProperty("transaction_amount")]
    public TransactionAmountDto Amount { get; init; }
    [JsonProperty("transaction_status")]
    public string Status { get; init; }
    [JsonProperty("transaction_subject")]
    public string? Subject { get; init; }
    [JsonProperty("transaction_note")]
    public string? Note { get; init; }

}

internal record TransactionAmountDto
{
    public string CurrencyCode { get; set; }
    public double Value { get; set; }
}
