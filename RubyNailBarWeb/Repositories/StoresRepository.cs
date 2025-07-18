using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RubyNailBarWeb.Models;

namespace RubyNailBarWeb.Repositories
{
    public class StoresRepository
    {
        private readonly IDbContextFactory<NailsDbContext> contextFactory;
        public StoresRepository(IDbContextFactory<NailsDbContext> _contextFactory)
        {
            this.contextFactory = _contextFactory;

        }

        public Store GetStoreById(int storeId)
        {
            using var db = this.contextFactory.CreateDbContext();
            var storeResult = db.Stores.Find(storeId);
            if (storeResult is not null)
            {
                return storeResult;
            }
            return new Store();
        }

        public List<Store>? GetStores() 
        {
            using var db = this.contextFactory.CreateDbContext();
            return db.Stores.ToList();
        }




    }
}
