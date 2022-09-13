using IntroToSQL.Interfaces;
using IntroToSQL.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IntroToSQL.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    public CustomersController(ICustomerRepository customerRepo)
    {
        _customerRepo = customerRepo;
    }

    private ICustomerRepository _customerRepo;

    // GET: api/<Customers>
    [HttpGet]
    public List<Customer> Get()
    {
        return _customerRepo.GetAll();
    }

    // GET api/<Customer>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<Customer>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<Customer>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<Customer>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
