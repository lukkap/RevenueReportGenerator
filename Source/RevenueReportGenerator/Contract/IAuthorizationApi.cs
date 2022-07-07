using Refit;

namespace RevenueReportGenerator.Contract;

internal interface IAuthorizationApi
{
    [Headers("Content-Type: application/x-www-form-urlencoded")]
    [Post("/v1/oauth2/token")]
    Task<string> GetAccessToken([Body(BodySerializationMethod.UrlEncoded)] FormUrlEncodedContent grantType);
}
