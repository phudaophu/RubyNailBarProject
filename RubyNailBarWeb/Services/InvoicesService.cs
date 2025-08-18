using RubyNailBarWeb.Models;
using RubyNailBarWeb.Repositories;
using RubyNailBarWeb.Services.Implements;

namespace RubyNailBarWeb.Services
{
    public class InvoicesService :IInvoicesService  
    {

        private readonly InvoicesRepository _invoicesRepository; 
        public InvoicesService(InvoicesRepository invoicesRepository)
        {
            this._invoicesRepository = invoicesRepository;
        }


        public int AddInvoiceService(Invoice invoice)
        {
            return _invoicesRepository.AddInvoice(invoice);
        }

        public List<Invoice> GetInvoicesService()
        {
            return _invoicesRepository.GetInvoices();
        }

        public Invoice GetInvoiceByIdService(int invoiceId)
        {
            if (invoiceId <= 0)
            {
                throw new ArgumentException("Invoice ID must be greater than zero.", nameof(invoiceId));
            }
            return _invoicesRepository.GetInvoiceById(invoiceId) ?? new Invoice();
        }

        public void UpdateInvoiceService(int invoiceId, Invoice invoice)
        {
             _invoicesRepository.UpdateInvoice(invoiceId, invoice);   
        }

        public List<Invoice>? SearchInvoicesByInvoiceDateService(DateOnly fromDate, DateOnly? toDate = null)
        {
            return _invoicesRepository.SearchInvoicesByInvoiceDate(fromDate, toDate);   
        }


    }


}
