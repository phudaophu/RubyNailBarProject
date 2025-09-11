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

        public List<CustomerService>? GetCustomerServiceByTypeService(string customerServiceType)
        {
            return _customerServicesRepository.GetCustomerServicesByType(customerServiceType);   
        }

        public List<string> GetCustomerServiceTypeService()
        {
            return _customerServicesRepository.GetCustomerServiceTypes();
        }

        public List<CustomerService> GetCustomerServicesService()
        {
            return _customerServicesRepository.GetCustomerServices();
        }

        public List<CustomerService> GetActiveCustomerServicesService()
        {
            return _customerServicesRepository.GetActiveCustomerServices();
        }

        public CustomerService? GetCustomerServiceByIdService(int customerServiceId)
        {
            return _customerServicesRepository.GetCustomerServiceById(customerServiceId);
        }


        public void UpdateCustomerServiceService(int customerServiceId, CustomerService customerService)
        {
            _customerServicesRepository.UpdateCustomerService(customerServiceId, customerService);
        }

        public List<CustomerService>? SearchCustomerServiceService(string keyString)
        {
            return _customerServicesRepository.SearchCustomerService(keyString);
        }

    }
}
