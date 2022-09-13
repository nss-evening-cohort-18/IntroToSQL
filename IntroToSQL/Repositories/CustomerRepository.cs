using System.Data.SqlClient;
using IntroToSQL.Interfaces;
using IntroToSQL.Models;

namespace IntroToSQL.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly string _connectionString;
    private readonly string _otherConnectionString;
    private SqlConnection MyConnection
    {
        get { return new SqlConnection(_connectionString); }
    }

    private SqlConnection Connection => new SqlConnection(_connectionString);
    private SqlConnection OtherConnection => new SqlConnection(_otherConnectionString);

    public CustomerRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("BriansSuperSecretConnection");
    }

    public List<Customer> GetAll()
    {
        using (SqlConnection c = OtherConnection)
        {
            c.Open();
            using (SqlCommand cmd = c.CreateCommand())
            {
                cmd.CommandText = @"
                                    SELECT Id,
                                           [Name],
                                           [Address],
                                           Phone,
                                           Email,
                                           IsVerified,
                                           Created,
                                           LastOnline
                                    FROM Customer";
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
}
