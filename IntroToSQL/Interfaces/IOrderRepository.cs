using IntroToSQL.Models;

namespace IntroToSQL.Interfaces
{
    public interface IOrderRepository
    {
        public IEnumerable<Order> GetAll();
        public IEnumerable<Order> GetByCustomerId(int id);
    }
}
