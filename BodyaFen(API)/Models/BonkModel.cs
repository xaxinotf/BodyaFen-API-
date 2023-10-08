namespace BodyaFen_API_.Models
{
    public class BonkModel
    {
        public string? Date { get; set; }
        public string? Bank { get; set; }
        public int BaseCurrency { get; set; }
        public string? BaseCurrencyLit { get; set; }
        public ExchangeRate[]? ExchangeRate { get; set; }
    }
}
