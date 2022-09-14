using System.Data.SqlClient;
using IntroToSQL.Interfaces;
using IntroToSQL.Models;

namespace IntroToSQL.Repositories;

public class OrderRepository : BaseRepository, IOrderRepository
{
    public OrderRepository(IConfiguration configuration): base(configuration) { }

    public IEnumerable<Order> GetAll()
    {
        using (var c = Connection)
        {
            c.Open();
            using (var cmd = c.CreateCommand())
            {
                List<Order> orders = new List<Order>();
                cmd.CommandText = "SELECT * FROM [Order]";
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order order = new Order()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                            DatePlaced = reader.GetDateTime(reader.GetOrdinal("DatePlaced")),
                            DateCompleted = reader.GetDateTime(reader.GetOrdinal("DateCompleted")),
                        };
                        orders.Add(order);
                    }
                    return orders;
                }
            }
        }
    }

    public IEnumerable<Order> GetByCustomerId(int id)
    {
        using (var c = Connection)
        {
            c.Open();
            using (var cmd = c.CreateCommand())
            {
                List<Order> orders = new List<Order>();
                
                cmd.CommandText = "SELECT * FROM [Order] WHERE CustomerId = @CustomerId";
                cmd.Parameters.Add(new SqlParameter("@CustomerId", id));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //var completedDateColumn = reader.GetOrdinal("DateCompleted");
                        //var isNullDate = reader.IsDBNull(completedDateColumn);
                        //DateTime? valueForDate;
                        //if (isNullDate) 
                        //{
                        //    valueForDate = null;
                        //}
                        //else
                        //{
                        //    valueForDate = reader.GetDateTime(completedDateColumn);
                        //}


                        Order order = new Order()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                            DatePlaced = reader.GetDateTime(reader.GetOrdinal("DatePlaced")),
                            DateCompleted = reader.IsDBNull(reader.GetOrdinal("DateCompleted")) ? null : reader.GetDateTime(reader.GetOrdinal("DateCompleted")),
                        };
                        orders.Add(order);
                    }
                    return orders;
                }
            }
        }
    }
}
