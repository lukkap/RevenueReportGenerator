using Microsoft.Extensions.DependencyInjection;
using RevenueReportGenerator;
using RevenueReportGenerator.Services;

var host = Startup.CreateHost(args);
var payPalService = ActivatorUtilities.CreateInstance<PayPalService>(host.Services);

await payPalService.GetTransactions(new DateTime(2022, 6, 1), new DateTime(2022, 7, 1).AddTicks(-1));
