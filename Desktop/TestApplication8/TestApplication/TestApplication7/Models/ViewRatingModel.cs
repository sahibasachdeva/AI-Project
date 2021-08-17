namespace TestApplication7.Models
{
    public class ViewRatingModel
    {
        public RatingModel Ratemodel { get; set; }
        public decimal EstimatedCost { get; set; }

        public string Error { get; set; }
        public string Message { get; set; }

    }
}