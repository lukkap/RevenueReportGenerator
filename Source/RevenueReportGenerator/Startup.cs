using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using RevenueReportGenerator.Contract;

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
                        var clientId = Environment.GetEnvironmentVariable("paypal_clientid");
                        var clientSecret = Environment.GetEnvironmentVariable("paypal_clientsecret");

                        var authenticationString = $"{clientId}:{clientSecret}";
                        var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));

                        client.BaseAddress = new Uri(PayPalApiBaseUrl);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
                    });

                services.AddTransient<IAuthorizationService, PayPalAuthorizationService>();
            })
            .Build();
    }
}
