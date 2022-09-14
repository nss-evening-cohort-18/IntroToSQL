using IntroToSQL.Interfaces;
using IntroToSQL.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IntroToSQL.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    public CustomersController(ICustomerRepository customerRepo, IOrderRepository orderRepo)
    {
        _customerRepo = customerRepo;
        _orderRepo = orderRepo;
    }

    private ICustomerRepository _customerRepo;
    private IOrderRepository _orderRepo;

    // GET: api/<Customers>
    [HttpGet]
    public List<Customer> Get()
    {
        return GetAllCustomersPlainById();
    }

    private List<Customer> GetAllCustomersPlain()
    {
        var customers = _customerRepo.GetAll();
        var orders = _orderRepo.GetAll();

        foreach (Customer customer in customers)
        {
            customer.Orders = orders.Where(o => o.CustomerId == customer.Id).ToList();
        }

        return customers;
    }

    private List<Customer> GetAllCustomersPlainById()
    {
        var customers = _customerRepo.GetAll();

        foreach (Customer customer in customers)
        {
            var orders = _orderRepo.GetByCustomerId(customer.Id).ToList();
            customer.Orders = orders;
        }

        return customers;
    }

    private List<Customer> GetAllCustomersSingleQuery()
    {
        return _customerRepo.GetFullCustomers();
    }
}
