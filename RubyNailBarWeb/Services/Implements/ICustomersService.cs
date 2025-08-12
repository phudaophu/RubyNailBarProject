using RubyNailBarWeb.Models;

namespace RubyNailBarWeb.Services.Implements
{
    public interface ICustomersService
    {

        bool IsCustomerPhoneNoExistService(string phoneNo, int? excludedCustomerId = null);
        int AddCustomerService(Customer customer);
        Customer? GetCustomerByIdService(int customerId);
        List<Customer> GetCustomersService();
        List<Customer>? SearchCustomerService(string keyString);
        void UpdateCustomerService(int customerId, Customer customer);
    }
}