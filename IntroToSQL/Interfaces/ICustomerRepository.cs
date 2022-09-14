using IntroToSQL.Models;

namespace IntroToSQL.Interfaces
{
    public interface ICustomerRepository
    {
        public List<Customer> GetAll();
        public List<Customer> GetFullCustomers();
    }
}
