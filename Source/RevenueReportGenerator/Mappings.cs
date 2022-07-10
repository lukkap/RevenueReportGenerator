using Mapster;
using RevenueReportGenerator.DTO;
using RevenueReportGenerator.PayPal.Contract;

namespace RevenueReportGenerator;

internal class Mappings
{
    public static void ConfigureTypeAdapters()
    {
        TypeAdapterConfig<TransactionDetails, EarningDto>
            .NewConfig()
            .IgnoreNullValues(true)
            .Map(dest => dest.TransactionId, src => src.TransactionInfo.Id)
            .Map(dest => dest.Date, src => src.TransactionInfo.Date)
            .Map(dest => dest.Amount, src => src.TransactionInfo.Amount.Value)
            .Map(dest => dest.CurrencyCode, src => src.TransactionInfo.Amount.CurrencyCode)
            .Map(dest => dest.TransactionSubject, src => src.TransactionInfo.Subject)
            .Map(dest => dest.TransactionNote, src => src.TransactionInfo.Note)
            .Map(dest => dest.PayerId, src => src.PayerInfo.Id)
            .Map(dest => dest.PayerName, src => src.PayerInfo.Name.AlternateFullName)
            .Map(dest => dest.PayerEmailAddress, src => src.PayerInfo.EmailAddress)
            .Map(dest => dest.PayerCountryCode, src => src.PayerInfo.CountryCode);
    }
}
