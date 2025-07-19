using RubyNailBarWeb.Models;
namespace RubyNailBarWeb.Services.Implements
{
    public interface IStoresService
    {
        Store GetStoreById(int storeId);
        List<Store> GetStores();
    }

}