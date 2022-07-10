namespace RevenueReportGenerator.PayPal.Contract;

public record TokenDetailsResponse
{
    public string Scope { get; set; }
    public string AccessToken { get; set; }
    public string TokenType { get; set; }
    public string AppId { get; set; }
    public int ExpiresIn { get; set; }
    public string Nonce { get; set; }

    public override string ToString()
    {
        return $"{TokenType} {AccessToken}";
    }
}
