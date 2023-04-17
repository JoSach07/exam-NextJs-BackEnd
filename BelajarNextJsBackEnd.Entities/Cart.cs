namespace BelajarNextJsBackEnd.Entities
{
    public class Cart
    {
        public string Id { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public string RestaurantId { get; set; } = string.Empty;

        public User User { get; set; } = null;

        public Restaurant Restaurant { get; set; } = null;

        public List<CartDetail> CartDetails = new List<CartDetail>();

        public DateTimeOffset CreatedAt { get; set; }
    }
}