using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace LoginReg.Models
{
    public class User
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
        [EmailAddress (ErrorMessage = "Invalid email address")]
        [Display (Name = "Email: ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password must be entered")]
        [MinLength (8, ErrorMessage = "Password mut be at least 8 characters")]
        [RegularExpression (@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage="Password must contain at least 1 number 1 letter and 1 special character")]
        [Display (Name = "Password: ")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password must be re-entered for validation")]
        [Display (Name ="Confirm Password: ")]
        public string ConfirmPassword {get; set;}


        public List<Reservation> Activities {get; set;}

        //used to iterate through a list incase of a null value
        public User(){
            Activities = new List<Reservation>();
        }
    }
}
