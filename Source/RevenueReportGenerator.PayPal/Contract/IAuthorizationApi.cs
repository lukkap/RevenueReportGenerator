namespace RevenueReportGenerator.PayPal.Contract;

public interface IAuthorizationApi
{
    [Headers("Content-Type: application/x-www-form-urlencoded")]
    [Post("/v1/oauth2/token")]
    Task<string> GetAccessToken([Body(BodySerializationMethod.UrlEncoded)] FormUrlEncodedContent grantType);
}
