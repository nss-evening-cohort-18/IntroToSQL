using System.Data.SqlClient;
using Dapper;
using IntroToSQL.Interfaces;
using IntroToSQL.Models;

namespace IntroToSQL.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly string _connectionString;
    private readonly string _getAllSql;
    private SqlConnection Connection => new SqlConnection(_connectionString);

    public CustomerRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("BriansSuperSecretConnection");
        _getAllSql = @"SELECT *
                       FROM Customer";
    }

    public IEnumerable<Customer> GetAll()
    {
        using (SqlConnection c = Connection)
        {
            c.Open();
            using (SqlCommand cmd = c.CreateCommand())
            {
                cmd.CommandText = _getAllSql;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    List<Customer> customers = new List<Customer>();
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

    public IEnumerable<Customer> DapperGetAll()
    {
        string sql = "SELECT * FROM Customer INNER JOIN [Order] ON [Order].CustomerId = Customer.Id;";
        using (var conn = Connection)
        {
            Dictionary<int, Customer> customers = new();
            return conn.Query<Customer, Order, Customer>(sql, (customer, order) => {
                Customer currentCustomer;

                if (!customers.TryGetValue(customer.Id, out currentCustomer))
                {
                    currentCustomer = customer;
                    customers.Add(currentCustomer.Id, currentCustomer);
                }
                
                currentCustomer.Orders.Add(order);
                return currentCustomer;
            });
            
        }
    }

    public IEnumerable<Customer> GetAllByFilter(bool isVerified)
    {
        string sql = "SELECT * FROM Customer LEFT JOIN [Order] ON [Order].CustomerId = Customer.Id WHERE IsVerified = @IsVerified;";
        using (var conn = Connection)
        {
            Dictionary<int, Customer> customers = new();
            return conn.Query<Customer, Order, Customer>(sql, (customer, order) => {
                Customer currentCustomer;

                if (!customers.TryGetValue(customer.Id, out currentCustomer))
                {
                    currentCustomer = customer;
                    customers.Add(currentCustomer.Id, currentCustomer);
                }

                currentCustomer.Orders.Add(order);
                return currentCustomer;
            }, new { IsVerified = isVerified });

        }
    }
}
