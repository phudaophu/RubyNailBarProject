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

        public Store GetStoreById(int storeId)
        {
            return storesRepository.GetStoreById(storeId);
        }
    }
}
