using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TimPortfolio.Models
{
    public class Contact
    {
        [Key]

        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter your first name")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 letters")]
        [Display (Name = "First Name: ")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 letters")]
        [Display (Name = "Last Name: ")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Valid email address is required")]
        [Display (Name = "Email: ")]
        public string Email { get; set; }


        public string Comment {get; set; }

        
    }
}