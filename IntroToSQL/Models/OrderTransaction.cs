namespace IntroToSQL.Models
{
    public class OrderTransaction
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int Qty { get; set; }
        public decimal ExtendedPrice { get; set; }
    }
}
