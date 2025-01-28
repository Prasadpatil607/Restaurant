namespace RestaurantDemo.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string? FeedbackMessage {  get; set; }
        public string? Name {  get; set; }
        public int? Ratings {  get; set; }
    }
}
