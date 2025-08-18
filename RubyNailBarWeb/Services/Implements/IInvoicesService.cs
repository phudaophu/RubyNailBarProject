using RubyNailBarWeb.Models;

namespace RubyNailBarWeb.Services.Implements
{
    public interface IInvoicesService
    {

        int AddInvoiceService(Invoice invoice);
        List<Invoice> GetInvoicesService();
        Invoice GetInvoiceByIdService(int invoiceId);
        void UpdateInvoiceService(int invoiceId, Invoice invoice);
        List<Invoice>? SearchInvoicesByInvoiceDateService(DateOnly fromDate, DateOnly? toDate = null);

    }
}