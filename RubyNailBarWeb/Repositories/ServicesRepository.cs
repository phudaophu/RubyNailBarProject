using Microsoft.EntityFrameworkCore;
using RubyNailBarWeb.Models;

namespace RubyNailBarWeb.Repositories
{
    public class ServicesRepository
    {
        private readonly IDbContextFactory<NailsDbContext> contextFactory;

        public ServicesRepository(IDbContextFactory<NailsDbContext> _contextFactory)
        {
            this.contextFactory = _contextFactory;
        }

        public List<Service> GetServices()
        {
            using var db = this.contextFactory.CreateDbContext();
            return db.Services.ToList();
        }






    }
}
