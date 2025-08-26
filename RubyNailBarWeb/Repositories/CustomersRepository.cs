using Microsoft.EntityFrameworkCore;
using RubyNailBarWeb.Components.Pages;
using RubyNailBarWeb.Models;

namespace RubyNailBarWeb.Repositories
{
    public class CustomersRepository
    {
        private readonly IDbContextFactory<NailsDbContext> contextFactory;

        public CustomersRepository(IDbContextFactory<NailsDbContext> _contextFactory)
        {
            this.contextFactory = _contextFactory;
        }


        public bool IsCustomerPhoneNoExist(string phoneNo, int? excludedCustomerId = null)
        {
            using var db = this.contextFactory.CreateDbContext();
            return db.Customers.Any(customer => customer.PhoneNo == phoneNo && customer.CustomerId != excludedCustomerId);
        }


        public int AddCustomer(Customer customer)
        {
            using var db = this.contextFactory.CreateDbContext();
            db.Customers.Add(customer);
            db.SaveChanges();
            return customer.CustomerId; 
        }

        public List<Customer> GetCustomers()
        {
            using var db = this.contextFactory.CreateDbContext();
            return db.Customers
                //.Include(c => c.BirthdayNotificationLogs)
                //.Include(c => c.CustomerPointLogs)
                //.Include(c => c.GiftCards)
                //.Include(c => c.Invoices)
                .ToList();
        }   

        public Customer? GetCustomerById(int customerId)
        {
            using var db = this.contextFactory.CreateDbContext();
            var customer = db.Customers.Find(customerId);
            if (customer is not null)
            {
                return customer;
            }
            else
            {
                return new Customer();

            }
        }

        public void UpdateCustomer(int customerId, Customer customer)
        {
            if (customer is null)
            {
                throw new ArgumentNullException("Loi: UpdateCustomer() customer is null " + nameof(customer));
            }
            if (customerId != customer.CustomerId) return;

            using var db = this.contextFactory.CreateDbContext();
            var customerToUpdate = db.Customers.Find(customerId);
            if (customerToUpdate != null)
            {
                customerToUpdate.Name = customer.Name;
                customerToUpdate.Email = customer.Email;
                customerToUpdate.PhoneNo = customer.PhoneNo;
                customerToUpdate.Birthday = customer.Birthday;
                customerToUpdate.Points = customer.Points;
                customerToUpdate.TransactionCount = customer.TransactionCount;
                customerToUpdate.CustomerLevel = customer.CustomerLevel;
                customerToUpdate.ImageUrl = customer.ImageUrl;
                customerToUpdate.Address1 = customer.Address1;
                customerToUpdate.ModifiedDatetime = DateTime.Now;
                customerToUpdate.LifetimeSpend = customer.LifetimeSpend;
                customerToUpdate.CivicAddress = customer.CivicAddress;
                customerToUpdate.CityName = customer.CityName;
                customerToUpdate.ProvinceName = customer.ProvinceName;
                customerToUpdate.PostalCode = customer.PostalCode;
                customerToUpdate.CountryName = customer.CountryName;
                db.SaveChanges();
            }
        }

        public List<Customer>? SearchCustomer(string keyString)
        {
            using var db = this.contextFactory.CreateDbContext();
            if (string.IsNullOrEmpty(keyString)) return new List<Customer>();

            IQueryable<Customer> customerQuery = db.Customers.AsNoTracking().Where(c =>
                                                                                    (c.Name != null && c.Name.ToLower().IndexOf(keyString.ToLower()) >= 0) ||
                                                                                    (c.Email != null && c.Email.ToLower().IndexOf(keyString.ToLower()) >= 0) ||
                                                                                    (c.PhoneNo != null && c.PhoneNo.ToLower().IndexOf(keyString.ToLower()) >= 0));
            

            return customerQuery.OrderBy(cq => cq.CustomerId).ToList();
        }
    }
}
