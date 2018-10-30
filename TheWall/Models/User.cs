using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace TheWall.Models
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
        [Display (Name = "Email: ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password must be entered")]
        [Display (Name = "Password: ")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password must be re-entered for validation")]
        [Display (Name ="Confirm Password: ")]
        public string ConfirmPassword {get; set;}

        public List<Message> Messages {get; set;}
        public List<Comment> Comments {get; set;}

        public User(){
            Messages = new List<Message>();
            Comments = new List<Comment>();
        }
    }
}
