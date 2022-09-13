namespace IntroToSQL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime DatePlaced { get; set; }
        public DateTime DateCompleted { get; set; }
        public List<OrderTransaction> OrderTransactions { get; set; }
    }
}
