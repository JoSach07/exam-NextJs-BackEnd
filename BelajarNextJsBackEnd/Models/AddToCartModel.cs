namespace BelajarNextJsBackEnd.Models
{
    public class AddToCartModel
    {
        public string RestaurantId { get; set; } = string.Empty;

        public string FoodItemId { set; get; } = string.Empty;

        public int Qty { set; get; }
    }
}
