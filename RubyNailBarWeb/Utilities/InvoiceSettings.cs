namespace RubyNailBarWeb.Utilities
{
    public class InvoiceSettings
    {
        public decimal GoodsAndServicesTaxInPercent { get; set; } = 0.5m;
        public decimal ProvincialSalesTaxInPercent { get; set; } = 0.7m;
        public decimal AppliedDiscountInPercent { get; set; } = 0;
        public List<string> BookingTypes { get; set; } = new List<string>();
    }
}
