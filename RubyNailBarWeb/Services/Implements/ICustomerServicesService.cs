using RubyNailBarWeb.Models;

namespace RubyNailBarWeb.Services.Implements
{
    public interface ICustomerServicesService
    {
        List<CustomerService>? GetCustomerServiceByTypeService(string customerServiceType);
        List<string> GetCustomerServiceTypeService();
        List<CustomerService> GetActiveCustomerServicesService();
        CustomerService? GetCustomerServiceByIdService(int customerServiceId);
        List<CustomerService> GetCustomerServicesService();
        List<CustomerService>? SearchCustomerServiceService(string keyString);
        void UpdateCustomerServiceService(int customerServiceId, CustomerService customerService);
    }
}