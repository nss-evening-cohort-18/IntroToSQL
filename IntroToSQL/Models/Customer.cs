using System.Data.SqlClient;

namespace IntroToSQL.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsVerified { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastOnline { get; set; }
        public List<Order> Orders { get; set; } = new();

    }
}
