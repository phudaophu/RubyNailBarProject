using RubyNailBarWeb.Models;
using RubyNailBarWeb.Repositories;
using RubyNailBarWeb.Services.Implements;
namespace RubyNailBarWeb.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly CustomersRepository _customersRepository;
        public CustomersService(CustomersRepository customersRepository)
        {
            this._customersRepository = customersRepository;
        }

        public void AddCustomerService(Customer customer)
        {
            _customersRepository.AddCustomer(customer);
        }

        public List<Customer> GetCustomersService()
        {
            return _customersRepository.GetCustomers();
        }

        public Customer? GetCustomerByIdService(int customerId)
        {
            var customer = _customersRepository.GetCustomerById(customerId);
            if (customer != null)
            {
                return customer;
            }
            else
            {
                return new Customer();
            }
        }

        public void UpdateCustomerService(int customerId, Customer customer)
        {
            _customersRepository.UpdateCustomer(customerId, customer);
        }

        public List<Customer>? SearchCustomerService(string keyString)
        {
            return _customersRepository.SearchCustomer(keyString);

        }





    }
}
