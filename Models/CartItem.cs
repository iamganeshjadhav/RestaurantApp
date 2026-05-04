namespace RestaurantApp.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int ItemId { get; set; }  

        public Item Item { get; set; } = null!;

        public int Quantity { get; set; }
        public int Total => Item.Price * Quantity;
    }
}