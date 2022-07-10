using Mapster;

namespace RevenueReportGenerator.DTO;

internal record AccessTokenDto
{
    public string TokenType { get; init; }
    [AdaptMember("AccessToken")]
    public string Value { get; init; }
}
