namespace RestaurantDemo.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string? ItemName { get; set; }
        public string? ItemDescription {  get; set; }
        public string? ItemPrice {  get; set; }
        public bool IsBreakfast {  get; set; }
        public bool IsLunch { get; set; }
        public bool IsDinner { get; set; }
        public bool IsDessert { get; set; }
        public bool IsDrink { get; set; }
    }
}
