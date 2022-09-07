using System;
using backend_p3.Dto;
using backend_p3.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace backend_p3.Data
{
    public class DbRepo : IDbRepo
    {
        private readonly MyDbContext _dbContext;
        private readonly IMemoryCache _memoryCache;


        public DbRepo(MyDbContext dbContext, IMemoryCache memoryCache)
        {
            _dbContext = dbContext;
            _memoryCache = memoryCache;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            IEnumerable<Customer> customers = _dbContext.Customers.ToList<Customer>();
            return customers;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            List<Customer> customers = await _dbContext.Customers.ToListAsync<Customer>();
            await Task.Delay(3000);
            return customers;
        }

        public async Task<List<Customer>> GetAllCustomersCache()
        {
            List<Customer> customers;

            customers = _memoryCache.Get<List<Customer>>("customers");

            if (customers is null)
            {
                customers = await _dbContext.Customers.ToListAsync<Customer>();
                await Task.Delay(3000);

                _memoryCache.Set("customers", customers, TimeSpan.FromSeconds(30));
            }


            return customers;
        }

        public Customer GetCustomerByID(int id)
        {
            Customer customer = _dbContext.Customers.FirstOrDefault(e => e.Id == id);
            return customer;
        }

        public Customer AddCustomer(Customer customer)
        {
            EntityEntry<Customer> e = _dbContext.Customers.Add(customer);
            Customer c = e.Entity;
            _dbContext.SaveChanges();
            return c;
        }

        public Customer removeCustomer(int id)
        {
            Customer c = _dbContext.Customers.FirstOrDefault((e) => e.Id == id);
            if(c is not null)
            {
                _dbContext.Remove(c);
                _dbContext.SaveChanges();
            }
            return c;
        }
    }
}

