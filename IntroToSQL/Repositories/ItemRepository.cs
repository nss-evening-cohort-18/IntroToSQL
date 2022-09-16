using System.Data.SqlClient;
using IntroToSQL.Interfaces;
using IntroToSQL.Models;

namespace IntroToSQL.Repositories
{
    public class ItemRepository : BaseRepository, IItemRepository
    {
        public ItemRepository(IConfiguration config) : base(config) { }

        public List<Item> GetAll()
        {
            using (var c = Connection)
            {
                c.Open();
                using (var cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Item";
                    var items = new List<Item>();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Item item = new Item
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Cost = reader.GetDecimal(reader.GetOrdinal("Cost")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                Status = reader.GetString(reader.GetOrdinal("Status")),
                                LastUpdated = reader.GetDateTime(reader.GetOrdinal("LastUpdated")),
                                StockQty = reader.GetInt32(reader.GetOrdinal("StockQty")),
                            };
                            items.Add(item);
                        }
                        return items;
                    }
                }
            }
        }

        public List<Item> Search(string searchValue)
        {
            using (var c = Connection)
            {
                c.Open();
                using (var cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Item WHERE [Name] LIKE '%' + @SearchValue + '%'";
                    cmd.Parameters.Add(new SqlParameter("@SearchValue", searchValue));
                    var items = new List<Item>();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Item item = new Item
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Cost = reader.GetDecimal(reader.GetOrdinal("Cost")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                Status = reader.GetString(reader.GetOrdinal("Status")),
                                LastUpdated = reader.GetDateTime(reader.GetOrdinal("LastUpdated")),
                                StockQty = reader.GetInt32(reader.GetOrdinal("StockQty")),
                            };
                            items.Add(item);
                        }
                        return items;
                    }
                }
            }
        }
    }
}
