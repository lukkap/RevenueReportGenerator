using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RevenueReportGenerator.DTO;
using RevenueReportGenerator.PayPal.Contract;

namespace RevenueReportGenerator.Services.Authorization;

internal class PayPalAuthorizationService : IAuthorizationService
{
    private readonly IAuthorizationApi _payPalAuthorizationApi;

    public PayPalAuthorizationService(IAuthorizationApi payPalAuthorizationApi)
    {
        _payPalAuthorizationApi = payPalAuthorizationApi;
    }

    public async Task<AccessTokenDto> GetAccessToken()
    {
        var grantType = new KeyValuePair<string, string>("grant_type", "client_credentials");
        var content = new FormUrlEncodedContent(new[] { grantType });
        var responseString = await _payPalAuthorizationApi.GetAccessToken(content);

        var response = JsonConvert.DeserializeObject<TokenDetailsResponse>(responseString,
            new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() }
            });

        // TODO: Map response to AccessTokenDto

        throw new NotImplementedException();
    }
}
