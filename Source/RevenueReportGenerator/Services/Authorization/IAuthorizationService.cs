using RevenueReportGenerator.DTO;

namespace RevenueReportGenerator.Services.Authorization;

internal interface IAuthorizationService
{
    Task<AccessTokenDto> GetAccessToken();
}
