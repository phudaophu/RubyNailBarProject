using RubyNailBarWeb.Models;
namespace RubyNailBarWeb.Services.Implements
{
    public interface IStoresService
    {
        Store GetStoreByIdService(int storeId);
        List<Store> GetStoresService();
    }

}