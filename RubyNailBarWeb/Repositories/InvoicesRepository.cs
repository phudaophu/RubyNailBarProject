using Microsoft.EntityFrameworkCore;
using RubyNailBarWeb.Models;
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

        public List<Invoice> GetInvoices()
        {
            using var db = this.contextFactory.CreateDbContext();
            return db.Invoices
                //.Include(c => c.BirthdayNotificationLogs)
                //.Include(c => c.CustomerPointLogs)
                //.Include(c => c.GiftCards)
                //.Include(c => c.Invoices)
                .ToList();
        }

        public Invoice? GetInvoiceById(int invoiceId)
        {
            using var db = this.contextFactory.CreateDbContext();
            var invoice = db.Invoices.Find(invoiceId);
            if (invoice is not null)
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
                db.SaveChanges();
            }
        }

        public List<Invoice>? SearchInvoicesByInvoiceDate(DateOnly fromDate, DateOnly? toDate = null)
        {
            using var db = this.contextFactory.CreateDbContext();

            if (toDate.HasValue && toDate.Value < fromDate)
            {
                throw new ArgumentException("Loi: input toDate > fromDate");
            }

            IQueryable<Invoice> invoiceQuery = db.Invoices.AsNoTracking()
                                                            .Where(iq => iq.InvoiceDate >= fromDate);

            if (toDate.HasValue)
            {
                invoiceQuery = invoiceQuery.Where(iq => iq.InvoiceDate < toDate.Value.AddDays(1));
            }


            return invoiceQuery.OrderBy(iq => iq.InvoiceDate).ToList(); 
        }



        //var invoiceList = db.Customers.Where(c =>
        //(c.Name != null && c.Name.ToLower().IndexOf(keyString.ToLower()) >= 0) ||
        //(c.Email != null && c.Email.ToLower().IndexOf(keyString.ToLower()) >= 0) ||
        //(c.PhoneNo != null && c.PhoneNo.ToLower().IndexOf(keyString.ToLower()) >= 0)).ToList();




    }
}
