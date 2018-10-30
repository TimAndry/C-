using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace LoginRegistration.Models
{
    public class User
    {
        [Key]

        public int UserId { get; set; }

        [Required]
        [Display (Name = "First Name: ")]
        public string FirstName { get; set; }

        [Required]
        [Display (Name = "Last Name: ")]
        public string LastName { get; set; }

        [Required]
        [Display (Name = "Email: ")]
        public string Email { get; set; }

        [Required]
        [Display (Name = "Password: ")]
        public string Password { get; set; }

        [Required]
        [Display (Name ="Confirm Password: ")]
        public string ConfirmPassword {get; set;}

        public List<Reservation> Weddings {get; set;}

        //used to iterate through a list incase of a null value
        public User(){
            Weddings = new List<Reservation>();
        }
    }
}
