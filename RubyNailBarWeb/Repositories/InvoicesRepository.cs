using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using RubyNailBarWeb.Models;
using System.Net;
using System.Runtime.ConstrainedExecution;

namespace RubyNailBarWeb.Repositories
{



    public class InvoicesRepository 
    {

        private readonly IDbContextFactory<NailsDbContext> contextFactory;
        public InvoicesRepository(IDbContextFactory<NailsDbContext> _contextFactory)
        {
            this.contextFactory = _contextFactory;
        }


        public int AddInvoice(Invoice invoice)
        {
            using var db = this.contextFactory.CreateDbContext();
            db.Invoices.Add(invoice);
            db.SaveChanges();
            return invoice.InvoiceId;
        }

        public List<Invoice> GetExistInvoicesOrderByCreatedDatetimeDesc()
        {
            using var db = this.contextFactory.CreateDbContext();
            return db.Invoices
                .Include(i => i.Manager)
                .Include(i => i.Store)
                .Include(i => i.Customer)
                .Include(i => i.InvoiceDetails).ThenInclude(i => i.Service)
                .Include(i => i.InvoiceDetails).ThenInclude(i => i.User)
                .Where(i => i != null && i.IsDeleted == false)
                .OrderByDescending(i => i.CreatedDatetime)
                .ToList();
                
        }

        public List<Invoice> GetInvoices()
        {
            using var db = this.contextFactory.CreateDbContext();
            return db.Invoices
                .Include(c => c.Manager)
                .Include(c => c.Store)
                .Include(c => c.Customer)
                .ToList();
        }

        public Invoice? GetInvoiceById(int invoiceId)
        {
            using var db = this.contextFactory.CreateDbContext();
            var invoice = db.Invoices.Find(invoiceId);
            if (invoice is not null && invoice.IsDeleted == false)
            {
                return invoice;
            }
            else
            {
                return new Invoice();

            }
        }

        public void UpdateInvoice(int invoiceId, Invoice invoice)
        {
            if (invoice is null)
            {
                throw new ArgumentNullException("Loi: UpdateInvoice() provided invoice is null " + nameof(invoice));
            }
            if (invoiceId != invoice.InvoiceId) return;
            

            using var db = this.contextFactory.CreateDbContext();
            var invoiceToUpdate = db.Invoices.Find(invoiceId);
            if (invoiceToUpdate != null)
            {
                invoiceToUpdate.InvoiceDate = invoice.InvoiceDate;
                invoiceToUpdate.StoreId = invoice.StoreId;
                invoiceToUpdate.ManagerId = invoice.ManagerId;
                invoiceToUpdate.CustomerId = invoice.CustomerId;
                invoiceToUpdate.ServicesAmount = invoice.ServicesAmount;
                invoiceToUpdate.TaxAmount = invoice.TaxAmount;
                invoiceToUpdate.TipAmount = invoice.TipAmount;
                invoiceToUpdate.PaymentMethodId = invoice.PaymentMethodId;
                invoiceToUpdate.IsDeleted = invoice.IsDeleted;    
                db.SaveChanges();
            }
        }

        public List<Invoice>? SearchInvoicesByCustomerInfo(string keyString)
        {
            using var db = this.contextFactory.CreateDbContext();

            if (string.IsNullOrWhiteSpace(keyString))
            {
               // throw new ArgumentNullException("Loi: provided keyword is null");
               return new List<Invoice>();  
            }

            IQueryable<Invoice> invoiceQuery = db.Invoices.AsNoTracking()
                                                          .Include(iq => iq.Customer)
                                                          .Include(iq=> iq.Store)
                                                          .Where(iq => iq.IsDeleted == false);

            invoiceQuery = invoiceQuery.Where(iq =>
                                                    (iq.Customer != null && iq.Customer.Name != null && iq.Customer.Name.ToLower().IndexOf(keyString.ToLower()) >= 0) ||
                                                    (iq.Customer != null && iq.Customer.Email != null && iq.Customer.Email.ToLower().IndexOf(keyString.ToLower()) >= 0) ||
                                                    (iq.Customer != null && iq.Customer.PhoneNo != null && iq.Customer.PhoneNo.ToLower().IndexOf(keyString.ToLower()) >= 0));


            return invoiceQuery.OrderByDescending(i=>i.CreatedDatetime).ToList();   
        }



        public List<Invoice>? SearchInvoicesByInvoiceDate(DateOnly fromDate, DateOnly? toDate = null)
        {
            using var db = this.contextFactory.CreateDbContext();

            if (toDate.HasValue && toDate.Value < fromDate)
            {
                throw new ArgumentException("Loi: input toDate > fromDate");
            }

            IQueryable<Invoice> invoiceQuery = db.Invoices.AsNoTracking()
                                                          .Include(iq => iq.Customer)
                                                          .Include(iq => iq.Store)
                                                          .Where(iq => iq.IsDeleted == false);

             invoiceQuery = invoiceQuery.Where(iq => iq.InvoiceDate >= fromDate);

            if (toDate.HasValue)
            {
                invoiceQuery = invoiceQuery.Where(iq => iq.InvoiceDate < toDate.Value.AddDays(1));
            }

            return invoiceQuery.OrderBy(iq => iq.InvoiceDate).ToList(); 
        }



    }
}
