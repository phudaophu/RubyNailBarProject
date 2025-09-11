using Microsoft.EntityFrameworkCore;
using RubyNailBarWeb.Components.Pages;
using RubyNailBarWeb.Models;

namespace RubyNailBarWeb.Repositories
{
    public class CustomerServicesRepository
    {
        private readonly IDbContextFactory<NailsDbContext> contextFactory;

        public CustomerServicesRepository(IDbContextFactory<NailsDbContext> _contextFactory)
        {
            this.contextFactory = _contextFactory;
        }

        public void UpdateCustomerService(int customerServiceId, CustomerService customerService)
        {
            if (customerService is null)
            {
                throw new ArgumentNullException("Loi: UpdateCustomerService() customerService is null " + nameof(customerService));
            }

            if (customerServiceId != customerService.CustomerServiceId) return;

            using var db = this.contextFactory.CreateDbContext();

            var customerServiceToUpdate = db.CustomerServices.Find(customerServiceId);
            if (customerServiceToUpdate != null)
            {
                customerServiceToUpdate.Name                 = customerService.Name;
                customerServiceToUpdate.Description          = customerService.Description;
                customerServiceToUpdate.VnName               = customerService.VnName;
                customerServiceToUpdate.Fee                  = customerService.Fee;
                customerServiceToUpdate.DurationMinutes      = customerService.DurationMinutes;
                customerServiceToUpdate.IsActive             = customerService.IsActive;
                customerServiceToUpdate.AdditionalParameterA = customerService.AdditionalParameterA;
                customerServiceToUpdate.FormularA            = customerService.FormularA;
                customerServiceToUpdate.ServiceType          = customerService.ServiceType;

                db.SaveChanges();
            }




        }

        public List<CustomerService> GetCustomerServices()
        {
            using var db = this.contextFactory.CreateDbContext();
            return db.CustomerServices.ToList();
        }

        public List<string> GetCustomerServiceTypes()
        {
            using var db = this.contextFactory.CreateDbContext();

            return db.CustomerServices.AsNoTracking()
                                    .Where(cs => cs.ServiceType != null)           
                                    .Select(cs => cs.ServiceType!.Trim())           
                                    .Where(s => s != "")                            
                                    .Distinct()                                    
                                    .OrderBy(s => s)                                
                                    .ToList();
        }



        public List<CustomerService> GetActiveCustomerServices()
        {
            using var db = this.contextFactory.CreateDbContext();

            IQueryable<CustomerService> customerServiceQuery = db.CustomerServices.AsNoTracking()
                                                                 .Where(cs => cs.IsActive == true);

            return customerServiceQuery.ToList();
        }

        public CustomerService? GetCustomerServiceById(int customerServiceId)
        {
            using var db = this.contextFactory.CreateDbContext();
            var customerService =  db.CustomerServices.Find(customerServiceId);
            if (customerService is not null)
            {
                return customerService;
            }
            else
            {
                return new CustomerService();
            }
        }

        //public List<CustomerService> GetCustomerServicesByType(string serviceType) 
        //{
        //    using var db = this.contextFactory.CreateDbContext();
        //    if (string.IsNullOrEmpty(serviceType)) return new List<CustomerService>();


        //    IQueryable<CustomerService> customerServiceQuery = db.CustomerServices.AsNoTracking()
        //                                                        .Where(cs => cs.ServiceType != null
        //                                                                && cs.ServiceType == serviceType)
        //                                                         .Where(cs => cs.IsActive == true);


        //    return customerServiceQuery.ToList();
        //}

        public List<CustomerService>? GetCustomerServicesByType(string customerServiceType)
        {
            using var db = this.contextFactory.CreateDbContext();
            if (string.IsNullOrEmpty(customerServiceType)) return new List<CustomerService>();

            IQueryable<CustomerService> customerServiceQuery = db.CustomerServices.AsNoTracking()
                                                                                    .Where(cs => (cs.ServiceType != null && cs.ServiceType.ToLower().IndexOf(customerServiceType.ToLower()) >= 0))
                                                                                    .Where(cs => cs.IsActive == true);


            return customerServiceQuery.ToList();
        }

        public List<CustomerService>? SearchCustomerService(string keyString)
        {
            using var db = this.contextFactory.CreateDbContext();
            if (string.IsNullOrEmpty(keyString)) return new List<CustomerService>();

            IQueryable<CustomerService> customerServiceQuery = db.CustomerServices.AsNoTracking()
                                                                                    .Where(cs => (cs.Name != null && cs.Name.ToLower().IndexOf(keyString.ToLower()) >= 0))
                                                                                    .Where(cs => (cs.ServiceType != null && cs.ServiceType.ToLower().IndexOf(keyString.ToLower()) >= 0))
                                                                                    .Where (cs => cs.IsActive == true);    


            return customerServiceQuery.ToList();
        }



    }
}
