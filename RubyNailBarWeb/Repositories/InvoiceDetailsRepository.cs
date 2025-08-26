using Microsoft.EntityFrameworkCore;
using RubyNailBarWeb.Components.Pages;
using RubyNailBarWeb.Models;

namespace RubyNailBarWeb.Repositories
{
    public class InvoiceDetailsRepository
    {

        private readonly IDbContextFactory<NailsDbContext> contextFactory;

        public InvoiceDetailsRepository(IDbContextFactory<NailsDbContext> _contextFactory)
        {
            this.contextFactory = _contextFactory;
        }

        public int AddInvoiceDetail(InvoiceDetail invoiceDetail)
        {
            using var db = this.contextFactory.CreateDbContext();
            db.InvoiceDetails.Add(invoiceDetail);   
            db.SaveChanges();
            return invoiceDetail.InvoiceDetailId;
        }

        public void UpdateInvoiceDetail(int invoiceDetailId, InvoiceDetail invoiceDetail)
        {
            if (invoiceDetail is null)
            {
                throw new ArgumentNullException("Loi: UpdateInvoiceDetail() provided invoiceDetail is null " + nameof(invoiceDetail));
            }
            if (invoiceDetailId != invoiceDetail.InvoiceDetailId) return;


            using var db = this.contextFactory.CreateDbContext();
            var invoiceDetailToUpdate = db.InvoiceDetails.Find(invoiceDetailId);
            if (invoiceDetailToUpdate != null)
            {
                invoiceDetailToUpdate.InvoiceId  = invoiceDetail.InvoiceId;
                invoiceDetailToUpdate.ServiceId  = invoiceDetail.ServiceId;
                invoiceDetailToUpdate.UserId     = invoiceDetail.UserId;
                invoiceDetailToUpdate.ServiceFee = invoiceDetail.ServiceFee;
                invoiceDetailToUpdate.Tip        = invoiceDetail.Tip;  
                invoiceDetailToUpdate.ModifiedDatetime  = invoiceDetail.ModifiedDatetime;
                invoiceDetailToUpdate.StartDatetime     = invoiceDetail.StartDatetime;  
                invoiceDetailToUpdate.EndDatetime       = invoiceDetail.EndDatetime;
                invoiceDetailToUpdate.IsDeleted         = invoiceDetail.IsDeleted;  
                invoiceDetailToUpdate.IsFinished        = invoiceDetail.IsFinished;    

                db.SaveChanges();
            }
        }

        public List<InvoiceDetail> GetInvoiceDetails()
        {
            using var db = this.contextFactory.CreateDbContext();
            return db.InvoiceDetails.Include(id=>id.User)
                                    .ToList();
        }

        public List<InvoiceDetail> GetValidInvoiceDetails()
        {
            using var db = this.contextFactory.CreateDbContext();
            IQueryable<InvoiceDetail> invoiceQuery = db.InvoiceDetails.AsNoTracking()
                                                                        .Include(id => id.User)
                                                                         .Where(id => id.IsDeleted == false);
                                                                         
            return invoiceQuery.ToList();
        }

        public List<InvoiceDetail>? GetInvoiceDetailsByInvoiceId(int invoiceId)
        {
            using var db = this.contextFactory.CreateDbContext();
            IQueryable<InvoiceDetail> invoiceQuery = db.InvoiceDetails.AsNoTracking()
                                                                        .Include(id => id.User)
                                                                        .Include(id => id.Service)
                                                                         .Where(id => id.IsDeleted == false)
                                                                          .Where(id => id.InvoiceId == invoiceId);
            return invoiceQuery.ToList();
        }







    }
}
