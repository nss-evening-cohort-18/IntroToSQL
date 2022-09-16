using System.Data.SqlClient;
using IntroToSQL.Interfaces;
using IntroToSQL.Models;

namespace IntroToSQL.Repositories;

public class CustomerRepository : BaseRepository, ICustomerRepository
{
    public CustomerRepository(IConfiguration configuration) : base(configuration) { }

    public List<Customer> GetAll()
    {
        using (SqlConnection c = Connection)
        {
            string customersSql = @"SELECT Id,
                                           [Name],
                                           [Address],
                                           Phone,
                                           Email,
                                           IsVerified,
                                           Created,
                                           LastOnline
                                    FROM Customer";

            c.Open();
            using (SqlCommand cmd = c.CreateCommand())
            {
                List<Customer> customers = new List<Customer>();

                cmd.CommandText = customersSql;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Address = reader.GetString(reader.GetOrdinal("Address")),
                            Phone = reader.GetString(reader.GetOrdinal("Phone")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            IsVerified = reader.GetBoolean(reader.GetOrdinal("IsVerified")),
                            Created = reader.GetDateTime(reader.GetOrdinal("Created")),
                            LastOnline = reader.GetDateTime(reader.GetOrdinal("LastOnline")),
                            Orders = new List<Order>(),
                        };
                        customers.Add(customer);
                    }
                    return customers;
                }
            }
        }
    }

    public List<Customer> GetFullCustomers()
    {
        using (SqlConnection c = Connection)
        {
            string customersSql = @"SELECT c.Id,c.[Name],c.[Address],c.Phone,c.Email,c.IsVerified, c.Created, c.LastOnline,
       o.Id AS OrderId,
	   o.CustomerId,
	   o.DatePlaced,
	   o.DateCompleted
FROM Customer c
LEFT JOIN [Order] o ON o.CustomerId = c.Id ";

            c.Open();
            using (SqlCommand cmd = c.CreateCommand())
            {
                Dictionary<int, Customer> customerDictionary = new();

                cmd.CommandText = customersSql;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Customer currentCustomer;

                    while (reader.Read())
                    {
                        var currentRowId = reader.GetInt32(reader.GetOrdinal("Id"));
                        
                        if (!customerDictionary.TryGetValue(currentRowId, out currentCustomer))
                        {
                            currentCustomer = new Customer()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                IsVerified = reader.GetBoolean(reader.GetOrdinal("IsVerified")),
                                Created = reader.GetDateTime(reader.GetOrdinal("Created")),
                                LastOnline = reader.GetDateTime(reader.GetOrdinal("LastOnline")),
                                Orders = new List<Order>(),
                            };
                            customerDictionary.Add(currentCustomer.Id, currentCustomer);
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("OrderId")))
                        {
                            Order order = new Order()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("OrderId")),
                                CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                                DatePlaced = reader.GetDateTime(reader.GetOrdinal("DatePlaced")),
                                DateCompleted = reader.IsDBNull(reader.GetOrdinal("DateCompleted")) ? null : reader.GetDateTime(reader.GetOrdinal("DateCompleted")),
                            };
                            currentCustomer.Orders.Add(order);
                        }
                    }
                    return customerDictionary.Values.ToList();
                }
            }
        }
    }
}
