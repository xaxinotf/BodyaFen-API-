namespace BodyaFen_API_.Models
{
    public class ExchangeRate
    {
        public string? BaseCurrency { get; set; }
        public string? Currency { get; set; }
        public double SaleRateNb { get; set; }
        public double PurchaseRateNb { get; set; }
        public double SaleRate { get; set; }
        public double PurchaseRate { get; set; }
    }
}
