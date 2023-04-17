namespace BelajarNextJsBackEnd.Models
{
    public class FoodItemDetailModel
    {
        public string Id { set; get; } = "";

        public string Name { set; get; } = "";

        public decimal Price { get; set; }

        public string RestaurantId { set; get; } = "";

        public string RestaurantName { set; get; } = "";
    }
}
