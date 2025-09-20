using RubyNailBarWeb.Models;

namespace RubyNailBarWeb.Services.Implements
{
    public interface IInvoicesService
    {
        List<string> GetInvoiceBookingTypesService();
        List<Invoice> GetWatchListInvoicesOrderByPaymentStatusCreatedDatetimeDescService();
        public decimal GetGoodsAndServicesTaxValue();
        public decimal GetProvincialSalesTaxValue();
        int GetNumberOfInvoiceDetailByInvoiceIdService(int invoiceId);
        int GetNumberOfNotFinishedInvoiceDetailByInvoiceIdService(int invoiceId);
        List<Invoice> GetExistInvoicesOrderByPaymentStatusAndCreatedDatetimeDescService();
        int AddInvoiceService(Invoice invoice);
        List<Invoice> GetInvoicesService();
        Invoice GetInvoiceByIdService(int invoiceId);
        void UpdateInvoiceService(int invoiceId, Invoice invoice);

        List<Invoice>? SearchWatchListInvoiceByCustomerInfoService(string keyString);
        List<Invoice>? SearchInvoicesByCustomerInfoService(string keyString);
        List<Invoice>? SearchInvoicesByInvoiceDateService(DateOnly fromDate, DateOnly? toDate = null);

    }
}