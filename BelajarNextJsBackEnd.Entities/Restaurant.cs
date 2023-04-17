namespace BelajarNextJsBackEnd.Entities
{
    public class Restaurant
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public List<FoodItem> FoodItems { get; set; } = new List<FoodItem>();

        public List<Cart> Carts { get; set; } = new List<Cart>();

        public DateTimeOffset CreatedAt { get; set; }
    }
}