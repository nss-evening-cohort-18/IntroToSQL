using IntroToSQL.Models;

namespace IntroToSQL.Interfaces
{
    public interface ICustomerRepository
    {
        public IEnumerable<Customer> GetAll();
        public IEnumerable<Customer> DapperGetAll();
        public IEnumerable<Customer> GetAllByFilter(bool isVerified);
    }
}
