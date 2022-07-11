namespace RevenueReportGenerator.DTO;

internal record EarningHistoryDto
{
    public int Year { get; init; }
    public int Month { get; init; }
    public IEnumerable<EarningDto> Earnings { get; init; }
}
