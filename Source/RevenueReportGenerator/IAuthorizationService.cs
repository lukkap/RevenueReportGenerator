using RevenueReportGenerator.DTO;

namespace RevenueReportGenerator;

internal interface IAuthorizationService
{
    Task<TokenInfoDto> GetAccessToken();
}
