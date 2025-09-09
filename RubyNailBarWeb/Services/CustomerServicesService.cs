using RubyNailBarWeb.Models;
using RubyNailBarWeb.Repositories;
using RubyNailBarWeb.Services.Implements;

namespace RubyNailBarWeb.Services
{
    public class CustomerServicesService : ICustomerServicesService
    {
        private readonly CustomerServicesRepository _customerServicesRepository;

        public CustomerServicesService(CustomerServicesRepository customerServicesRepository)
        {
            _customerServicesRepository = customerServicesRepository;
        }


        public List<CustomerService> GetCustomerServices()
        {
            return _customerServicesRepository.GetCustomerServices();
        }

        public List<CustomerService> GetActiveCustomerServices()
        {
            return _customerServicesRepository.GetActiveCustomerServices();
        }

        public CustomerService? GetCustomerServiceById(int customerServiceId)
        {
            return _customerServicesRepository.GetCustomerServiceById(customerServiceId);
        }

        public List<CustomerService> GetCustomerServicesByType(string serviceType)
        {
            return _customerServicesRepository.GetCustomerServicesByType(serviceType);
        }


        public void UpdateCustomerService(int customerServiceId, CustomerService customerService)
        {
            _customerServicesRepository.UpdateCustomerService(customerServiceId, customerService);
        }

        public List<CustomerService>? SearchCustomerService(string keyString)
        {
            return _customerServicesRepository.SearchCustomerService(keyString);
        }




    }
}
