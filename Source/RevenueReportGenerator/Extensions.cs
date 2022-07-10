using RevenueReportGenerator.PayPal.Contract;

namespace RevenueReportGenerator;

internal static class Extensions
{
    internal static string ToPayPalFormat(this DateTime dateTime)
        => $"{new DateTimeOffset(dateTime):yyyy-MM-ddTHH:mm:sszz}{new DateTimeOffset(dateTime).Offset:mm}";

    internal static bool IsPositiveAndCompleted(this TransactionDetails transactionDetails)
    {
        const string transactionCompletedStatus = "S";

        return
            transactionDetails.TransactionInfo?.Id is not null &&
            transactionDetails.PayerInfo?.Id is not null && 
            transactionDetails.TransactionInfo?.Amount?.Value > 0 &&
            transactionDetails.TransactionInfo?.Status == transactionCompletedStatus;
    }
}
