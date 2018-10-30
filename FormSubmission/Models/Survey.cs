using System.ComponentModel.DataAnnotations;
namespace RazorFun
{
    public class Trail
    {   
        //Name input has validation included
        [Required]
        [MinLength(3)]
        [Display(Name = "First Name: ")]
        public string FirstName {get; set;}

        [Required]
        [MinLength(3)]
        [Display(Name = "Last Name: ")]
        public string LastName {get; set;}

        [Required]
        [Range(1, 200)]
        [Display(Name = "Age")]
        public int Age{get; set;}

        [Required]
        [MinLength(7)]
        [Display(Name = "Email Address")]
        public string Email{get;set;}

        [Required]
        [MinLength(8)]
        [Display(Name = "Password")]
        public string Password{get;set;}
    }
}