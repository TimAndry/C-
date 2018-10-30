using System.ComponentModel.DataAnnotations;
namespace RESTauranter.Models
{
    public class Restaurant
    {
        [Key]

        
        public int Id { get; set; }
        
        [Required]
        [Display (Name = "Reviewer Name: ")]
        public string ReviewerName { get; set; }
        
        [Required]
        [Display (Name = "Restaurant Name: ")]
        public string RestaurantName { get; set; }
        
        [Required]
        [Display (Name = "Rating: ")]
        public string StarRating {get; set;}
        
        [Required]
        [Display (Name = "Comment: ")]
        public string ReviewComment { get; set; }
        
        [Required]
        [Display (Name = "Date of visit: ")]
        public string ReviewDate { get; set; }
    }
}