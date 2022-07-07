using RevenueReportGenerator.DTO;

namespace RevenueReportGenerator.Services;

internal interface IAuthorizationService
{
    Task<TokenInfoDto> GetAccessToken();
}
