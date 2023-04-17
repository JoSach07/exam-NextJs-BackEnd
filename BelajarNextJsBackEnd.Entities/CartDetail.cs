namespace BelajarNextJsBackEnd.Entities
{
    public class CartDetail
    {
        public string Id { get; set; } = string.Empty;

        public string CartId { get; set; } = string.Empty;

        public string FoodItemId { get; set; } = string.Empty;

        public int Qty { get; set; }

        public Cart Cart { get; set; } = null;

        public FoodItem FoodItem { get; set; } = null;

        public DateTimeOffset CreatedAt { get; set; }
    }
}