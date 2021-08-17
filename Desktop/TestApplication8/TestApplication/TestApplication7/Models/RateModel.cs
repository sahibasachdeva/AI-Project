using System.ComponentModel.DataAnnotations;

namespace TestApplication7.Models
{
    public class RatingModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "length cannot be zero")]
        public int Length { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "width cannot be zero")]
        public int Width { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "height cannot be zero")]
        public int Height { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "weight cannot be zero")]
        public int Weight { get; set; }
        [Required]
        [RegularExpression(@"^((?!.*L4N 3P8.*).)*$", ErrorMessage = "You cannot rate shop this postal code")]
        public string PostalCode { get; set; }




    }
}