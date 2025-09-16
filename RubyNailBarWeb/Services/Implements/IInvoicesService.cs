using RubyNailBarWeb.Models;

namespace RubyNailBarWeb.Services.Implements
{
    public interface IInvoicesService
    {
        public decimal GetGoodsAndServicesTaxValue();

        public decimal GetProvincialSalesTaxValue();

        int GetNumberOfInvoiceDetailByInvoiceIdService(int invoiceId);
        int GetNumberOfNotFinishedInvoiceDetailByInvoiceIdService(int invoiceId);
        List<Invoice> GetExistInvoicesOrderByCreatedDatetimeDescService();
        int AddInvoiceService(Invoice invoice);
        List<Invoice> GetInvoicesService();
        Invoice GetInvoiceByIdService(int invoiceId);
        void UpdateInvoiceService(int invoiceId, Invoice invoice);
        public List<Invoice>? SearchInvoicesByCustomerInfoService(string keyString);
        List<Invoice>? SearchInvoicesByInvoiceDateService(DateOnly fromDate, DateOnly? toDate = null);

    }
}