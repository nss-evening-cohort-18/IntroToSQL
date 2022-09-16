using IntroToSQL.Interfaces;
using IntroToSQL.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IntroToSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private IItemRepository _itemRepo;
        public ItemsController(IItemRepository itemRepository)
        {
            _itemRepo = itemRepository;
        }

        // GET: api/<ItemsController>
        [HttpGet]
        public List<Item> Get(string? q = null)
        {
            if (q == null)
            {
                return _itemRepo.GetAll();
            }
            else
            {
                return _itemRepo.Search(q);
            }


        }
    }
}
