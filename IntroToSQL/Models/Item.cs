namespace IntroToSQL.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public DateTime LastUpdated { get; set; }
        public int StockQty { get; set; }
    }
}
