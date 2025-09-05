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

        public int GetNumberOfNotFinishedInvoiceDetailByInvoiceIdService(int invoiceId)
        {
            var invoice = _invoicesRepository.GetInvoiceById(invoiceId) ?? new Invoice();
            if (invoice is null || invoice.InvoiceDetails.Count == 0 || !invoice.InvoiceDetails.Any(id => id.IsFinished == false)) return 0;
            return invoice.InvoiceDetails.Where(id=>id.IsFinished == false).Count();

        }
        public int GetNumberOfInvoiceDetailByInvoiceIdService(int invoiceId)
        {
            var invoice = _invoicesRepository.GetInvoiceById(invoiceId) ?? new Invoice();
            if (invoice is null || invoice.InvoiceDetails.Count == 0 ) return 0;
            return invoice.InvoiceDetails.Count();

        }

        public int AddInvoiceService(Invoice invoice)
        {
            return _invoicesRepository.AddInvoice(invoice);
        }
        public List<Invoice> GetExistInvoicesOrderByCreatedDatetimeDescService()
        {
            return _invoicesRepository.GetExistInvoicesOrderByCreatedDatetimeDesc();
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

        public  List<Invoice>? SearchInvoicesByCustomerInfoService(string keyString)
        {
            return _invoicesRepository.SearchInvoicesByCustomerInfo(keyString);
        }

        public List<Invoice>? SearchInvoicesByInvoiceDateService(DateOnly fromDate, DateOnly? toDate = null)
        {
            return _invoicesRepository.SearchInvoicesByInvoiceDate(fromDate, toDate);   
        }


    }


}
