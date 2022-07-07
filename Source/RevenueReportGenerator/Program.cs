using Microsoft.Extensions.DependencyInjection;
using RevenueReportGenerator;

var host = Startup.CreateHost(args);
var paymentService = ActivatorUtilities.CreateInstance<PayPalAuthorizationService>(host.Services);

var tokenInfo = await paymentService.GetAccessToken();

Console.WriteLine(tokenInfo.ToString());
