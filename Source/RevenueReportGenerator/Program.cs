using Microsoft.Extensions.DependencyInjection;
using RevenueReportGenerator;
using RevenueReportGenerator.Services.Transactions;

var host = Startup.CreateHost(args);
var payPalService = ActivatorUtilities.CreateInstance<PayPalService>(host.Services);

await payPalService.GetEarningTransactions(new DateTime(2022, 6, 1), new DateTime(2022, 7, 1).AddTicks(-1));
