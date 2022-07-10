namespace RevenueReportGenerator.DTO;

internal class EarningDto
{
    public string TransactionId { get; init; }
    public DateTime Date { get; init; }
    public double Amount { get; init; }
    public string CurrencyCode { get; init; }
    public string? TransactionSubject { get; init; }
    public string? TransactionNote { get; init; }
    public string PayerId { get; init; }
    public string? PayerName { get; init; }
    public string? PayerEmailAddress { get; init; }
    public string? PayerCountryCode { get; init; }
}
