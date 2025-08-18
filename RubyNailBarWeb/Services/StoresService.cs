using RubyNailBarWeb.Models;
using RubyNailBarWeb.Repositories;
using RubyNailBarWeb.Services.Implements;

namespace RubyNailBarWeb.Services
{
    public class StoresService : IStoresService
    {
        private readonly  StoresRepository storesRepository;
        public StoresService(StoresRepository storesRepository)
        {
            this.storesRepository = storesRepository;
        }

        public Store GetStoreByIdService(int storeId)
        {
            return storesRepository.GetStoreById(storeId);
        }

        public List<Store> GetStoresService()
        {
            var stores = storesRepository.GetStores();
            if (stores is null)
            {
                return new List<Store>();
            }
            return stores;
        }
    }
}
