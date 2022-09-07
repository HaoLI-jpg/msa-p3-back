using System;
using backend_p3.Data;
using backend_p3.Dto;
using backend_p3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

[Route("api")]
[ApiController]
public class CustomerController : Controller
{
    private readonly IDbRepo _repo;
    public CustomerController(IDbRepo repo)
    {
        _repo = repo;
    }

    [HttpGet("allCustomers")]
    public IActionResult GetCustomers()
    {
        IEnumerable<Customer> customers = _repo.GetAllCustomers();
        return Ok(customers);
    }

    [HttpGet("allCustomersAsync")]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomersAsync()
    {
        IEnumerable<Customer> customers =await _repo.GetAllCustomersAsync();
        return Ok(customers);
    }

    [HttpGet("allCustomersCache")]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomersCache()
    {
        IEnumerable<Customer> customers = await _repo.GetAllCustomersCache();
        return Ok(customers);
    }

    [HttpGet("Customer/{id}")]
    public IActionResult GetCustomerByID(int id)
    {
        Customer c = _repo.GetCustomerByID(id);

        if (c == null)
        {
            return NotFound();
        }

        return Ok(c);
    }

    [HttpPost("AddCustomer")]
    public IActionResult AddCustomer(CustomerInput data)
    {
        Customer input = new() { FirstName = data.FirstName, LastName = data.LastName, Email = data.Email };
        Customer c = _repo.AddCustomer(input);
        return Ok(c);
    }

    [HttpPost("RemoveCustomer/{id}")]
    public IActionResult removeCustomer(int id)
    {
        Customer result = _repo.removeCustomer(id);
        return Ok(result);
    }
}


