using RubyNailBarWeb.Models;
using RubyNailBarWeb.Services.Implements;
using RubyNailBarWeb.Repositories;
namespace RubyNailBarWeb.Services
{
    public class InvoiceDetailsService : IInvoiceDetailsService
    {

        private readonly InvoiceDetailsRepository _invoiceDetailsRepository;  

        public InvoiceDetailsService(InvoiceDetailsRepository invoiceDetailsRepository)
        {
            this._invoiceDetailsRepository = invoiceDetailsRepository;
        }

        public int AddInvoiceDetailService(InvoiceDetail invoiceDetail)
        {
            return _invoiceDetailsRepository.AddInvoiceDetail(invoiceDetail);
        }

        public void UpdateInvoiceDetailService(int invoiceDetailId, InvoiceDetail invoiceDetail)
        {
            _invoiceDetailsRepository.UpdateInvoiceDetail(invoiceDetailId, invoiceDetail);    
        }

        public List<InvoiceDetail> GetInvoiceDetailsService()
        {
            return _invoiceDetailsRepository.GetInvoiceDetails();
        }

        public List<InvoiceDetail>? GetInvoiceDetailsByInvoiceIdService(int invoiceId) 
        {
            return _invoiceDetailsRepository.GetInvoiceDetailsByInvoiceId(invoiceId);
        }

        public List<InvoiceDetail> GetValidInvoiceDetailsService()
        {
            return _invoiceDetailsRepository.GetValidInvoiceDetails();
        }



    }
}
