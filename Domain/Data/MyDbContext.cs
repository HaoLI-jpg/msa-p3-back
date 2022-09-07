using System;
using backend_p3.Model;
using Microsoft.EntityFrameworkCore;

namespace backend_p3.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }


    }
}