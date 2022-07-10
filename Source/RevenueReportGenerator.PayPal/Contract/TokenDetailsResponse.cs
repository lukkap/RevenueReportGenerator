namespace RevenueReportGenerator.PayPal.Contract;

public record TokenDetailsResponse
{
    public string Scope { get; init; }
    public string AccessToken { get; init; }
    public string TokenType { get; init; }
    public string AppId { get; init; }
    public int ExpiresIn { get; init; }
    public string Nonce { get; init; }

    public override string ToString()
    {
        return $"{TokenType} {AccessToken}";
    }
}
