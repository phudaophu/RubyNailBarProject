using Microsoft.EntityFrameworkCore;
using RubyNailBarWeb.Components.Pages;
using RubyNailBarWeb.Models;

namespace RubyNailBarWeb.Repositories
{
    public class PaymentMethodRepository
    {

        private readonly IDbContextFactory<NailsDbContext> contextFactory;
        public PaymentMethodRepository(IDbContextFactory<NailsDbContext> _contextFactory)
        {
            this.contextFactory = _contextFactory;
        }

        public List<PaymentMethod> GetPendingPayments()
        {
            using var db = this.contextFactory.CreateDbContext();
            IQueryable <PaymentMethod> paymentQuery = db.PaymentMethods.AsNoTracking()
                                                          .Where(p => p.IsPayment == true);
            return paymentQuery.ToList();   
        }

        public List<PaymentMethod> GetPaymentMethods() 
        {
            using var db = this.contextFactory.CreateDbContext();
            return db.PaymentMethods.OrderBy(p=>p.IsPayment).ToList();
        }

        public PaymentMethod? GetPaymentMethodById(int paymentMethodId) 
        {
            using var db = this.contextFactory.CreateDbContext();

            if (paymentMethodId <= 0) { throw new ArgumentException("Loi: GetPaymentMethodById() paymentMethodId must have a valid value > 0 "); }

            return db.PaymentMethods.Find(paymentMethodId);
        }

    }
}
