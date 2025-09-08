using Microsoft.EntityFrameworkCore;
using RubyNailBarWeb.Models;

namespace RubyNailBarWeb.Repositories
{
    public class CustomerServicesRepository
    {
        private readonly IDbContextFactory<NailsDbContext> contextFactory;

        public CustomerServicesRepository(IDbContextFactory<NailsDbContext> _contextFactory)
        {
            this.contextFactory = _contextFactory;
        }

        public List<CustomerService> GetCustomerServices()
        {
            using var db = this.contextFactory.CreateDbContext();
            return db.CustomerServices.ToList();
        }




    }
}
