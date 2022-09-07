using System;
using backend_p3.Model;
using backend_p3.Dto;


namespace backend_p3.Data
{
    public interface IDbRepo
    {
        IEnumerable<Customer> GetAllCustomers();
        Task<List<Customer>> GetAllCustomersAsync();
        Task<List<Customer>> GetAllCustomersCache();

        Customer GetCustomerByID(int id);
        Customer AddCustomer(Customer customer);
        Customer removeCustomer(int id);
    }
}

