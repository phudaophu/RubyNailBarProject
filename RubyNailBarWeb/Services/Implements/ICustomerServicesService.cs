using RubyNailBarWeb.Models;

namespace RubyNailBarWeb.Services.Implements
{
    public interface ICustomerServicesService
    {
        List<CustomerService> GetActiveCustomerServices();
        CustomerService? GetCustomerServiceById(int customerServiceId);
        List<CustomerService> GetCustomerServices();
        List<CustomerService> GetCustomerServicesByType(string serviceType);
        List<CustomerService>? SearchCustomerService(string keyString);
        void UpdateCustomerService(int customerServiceId, CustomerService customerService);
    }
}