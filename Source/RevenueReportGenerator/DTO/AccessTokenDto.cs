namespace RevenueReportGenerator.DTO;

internal record AccessTokenDto
{
    public string TokenType { get; init; }
    public string Value { get; init; }
}
