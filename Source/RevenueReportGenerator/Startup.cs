using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using RevenueReportGenerator.Contract;
using RevenueReportGenerator.DTO;
using RevenueReportGenerator.Services;

namespace RevenueReportGenerator;

internal class Startup
{
    private const string PayPalApiBaseUrl = "https://api-m.paypal.com";

    internal static IHost CreateHost(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureHostConfiguration(configurationBuilder => 
                configurationBuilder.AddEnvironmentVariables())
            .ConfigureServices((_, services) =>
            {
                services.AddRefitClient<IAuthorizationApi>()
                    .ConfigureHttpClient(client =>
                    {
                        client.BaseAddress = new Uri(PayPalApiBaseUrl);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", GetEncodedPayPalClientCredentials());
                    });
                services.AddRefitClient<IPayPalApi>()
                    .ConfigureHttpClient((services, client) =>
                    {
                        var accessToken = GetAccessToken(services);
                        client.BaseAddress = new Uri(PayPalApiBaseUrl);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(accessToken.TokenType, accessToken.Value);
                    });
                services.AddTransient<IAuthorizationService, PayPalAuthorizationService>();
            })
            .Build();
    }

    private static string GetEncodedPayPalClientCredentials()
    {
        var clientId = Environment.GetEnvironmentVariable("paypal_clientid");
        var clientSecret = Environment.GetEnvironmentVariable("paypal_clientsecret");

        var authenticationString = $"{clientId}:{clientSecret}";
        var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));

        return base64EncodedAuthenticationString;
    }

    private static AccessTokenDto GetAccessToken(IServiceProvider services)
    {
        var payPalAuthorizationService = services.GetService<IAuthorizationService>();
        var accessToken = payPalAuthorizationService.GetAccessToken().Result;

        return accessToken;
    }
}
