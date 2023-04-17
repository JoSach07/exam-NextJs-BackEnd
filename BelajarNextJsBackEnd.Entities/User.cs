namespace BelajarNextJsBackEnd.Entities
{
    public class User
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public List<Cart> Carts { get; set; } = new List<Cart>();

        public DateTimeOffset CreatedAt { get; set; }
    }
}