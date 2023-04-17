namespace BelajarNextJsBackEnd.Entities
{
    public class FoodItem
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string RestaurantId { get; set; } = string.Empty;

        public Restaurant Restaurant { get; set; } = null;

        public List<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

        public DateTimeOffset CreatedAt { get; set; }
    }
}