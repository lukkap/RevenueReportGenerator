using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RevenueReportGenerator.Contract;
using RevenueReportGenerator.DTO;

namespace RevenueReportGenerator.Services;

internal class PayPalAuthorizationService : IAuthorizationService
{
    private readonly IAuthorizationApi _payPalAuthorizationApi;

    public PayPalAuthorizationService(IAuthorizationApi payPalAuthorizationApi)
    {
        _payPalAuthorizationApi = payPalAuthorizationApi;
    }

    public async Task<TokenInfoDto> GetAccessToken()
    {
        var grantType = new KeyValuePair<string, string>("grant_type", "client_credentials");
        var content = new FormUrlEncodedContent(new[] { grantType });
        var responseString = await _payPalAuthorizationApi.GetAccessToken(content);

        return JsonConvert.DeserializeObject<TokenInfoDto>(responseString,
            new JsonSerializerSettings {
                ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() }
            });
    }
}
