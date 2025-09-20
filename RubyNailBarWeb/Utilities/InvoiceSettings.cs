namespace RubyNailBarWeb.Utilities
{
    public class InvoiceSettings
    {
        public decimal GoodsAndServicesTax { get; set; } = 0.5m;
        public decimal ProvincialSalesTax { get; set; } = 0.7m;
        public List<string> BookingTypes { get; set; } = new List<string>();
    }
}
