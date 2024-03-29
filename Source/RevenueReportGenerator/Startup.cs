﻿using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using RevenueReportGenerator.DTO;
using RevenueReportGenerator.PayPal.Contract;
using RevenueReportGenerator.Services.Authorization;
using RevenueReportGenerator.Services.Report;

namespace RevenueReportGenerator;

internal class Startup
{
    private const string PayPalApiBaseUrl = "https://api-m.paypal.com";

    internal static IHost CreateHost(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureHostConfiguration(configurationBuilder =>
            {
                configurationBuilder.AddEnvironmentVariables();
                Mappings.ConfigureTypeAdapters();
            })
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
                services.AddTransient<IReportGenerator, CsvReportGenerator>();
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
        var authorizationService = services.GetService<IAuthorizationService>();
        var accessToken = authorizationService.GetAccessToken().Result;

        if (accessToken is null)
            throw new Exception("Access token missing!");

        return accessToken;
    }
}
