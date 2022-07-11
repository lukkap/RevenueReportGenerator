using Microsoft.Extensions.DependencyInjection;
using RevenueReportGenerator;
using RevenueReportGenerator.Services.Report;
using RevenueReportGenerator.Services.Transactions;

var host = Startup.CreateHost(args);
var payPalService = ActivatorUtilities.CreateInstance<PayPalService>(host.Services);
var reportGenerator = ActivatorUtilities.CreateInstance<CsvReportGenerator>(host.Services);

var earningHistory = await payPalService.GetEarningTransactions(DateTime.Now.Year, DateTime.Now.Month - 1);
var filePath = reportGenerator.GenerateEarningsReport(earningHistory);

Console.WriteLine($"Earnings report created: {filePath}");
