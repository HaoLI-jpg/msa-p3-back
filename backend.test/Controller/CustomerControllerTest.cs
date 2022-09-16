using System;
using backend_p3.Data;
using backend_p3.Dto;
using backend_p3.Model;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace xUnitTesting.Controller
{
    public class CustomerControllerTest
    {
        private readonly IDbRepo _repo;

        public CustomerControllerTest()
        {

            _repo = A.Fake<IDbRepo>();
        }

        [Fact]
        public void CustomerModel()
        {
            CustomerInput data = new() { Email = "test", FirstName = "test", LastName = "test" };
            Customer input = new() { FirstName = data.FirstName, LastName = data.LastName, Email = data.Email };
            input.Should().BeEquivalentTo(data);

        }

        [Fact]
        public void CustomerController_GetCustomers_ReturnOk()
        {
            var customerList = A.Fake<List<Customer>>();

            var controller = new CustomerController(_repo);

            var result = controller.GetCustomers();

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));            
        }

        [Fact]
        public void CustomerController_GetCustomerByID_ReturnOk()
        {
            int id = 1;

            var controller = new CustomerController(_repo);
            var result = controller.GetCustomerByID(id);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void CustomerController_AddCustomer_ReturnOk()
        {
            var c = A.Fake<Customer>();
            var cCreate = A.Fake<CustomerInput>();
            var controller = new CustomerController(_repo);

            var result = controller.AddCustomer(cCreate);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));

            var getCustomers = controller.GetCustomers();
            getCustomers.Should().NotBeNull();

        }

        [Fact]
        public void CustomerController_RemoveCustomer_ReturnOk()
        {
            var id = 1;
            var c = A.Fake<Customer>();
            var controller = new CustomerController(_repo);

            var result = controller.removeCustomer(id);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

    }
}

