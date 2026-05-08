namespace RestaurantApp.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int UserId { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int Total { get; private set; }
        public Item Item { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}