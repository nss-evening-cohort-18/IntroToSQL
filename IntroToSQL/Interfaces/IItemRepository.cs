using IntroToSQL.Models;

namespace IntroToSQL.Interfaces
{
    public interface IItemRepository
    {
        List<Item> GetAll();
        List<Item> Search(string searchValue);
    }
}
