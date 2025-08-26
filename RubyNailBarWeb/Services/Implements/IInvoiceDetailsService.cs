using RubyNailBarWeb.Models;

namespace RubyNailBarWeb.Services.Implements
{
    public interface IInvoiceDetailsService
    {
        List<InvoiceDetail>? GetInvoiceDetailsByInvoiceIdService(int invoiceId);

        List<InvoiceDetail> GetValidInvoiceDetailsService();

        int AddInvoiceDetailService(InvoiceDetail invoiceDetail);

        List<InvoiceDetail> GetInvoiceDetailsService();

        void UpdateInvoiceDetailService(int invoiceDetailId, InvoiceDetail invoiceDetail);

    }
}