using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace LoginRegistration.Models
{
    public class Wedding
    {
        [Key]

        public int WeddingId { get; set; }


        [Required]
        [Display (Name = "Spouse 1: ")]
        public string WedderOne { get; set; }

        [Required]
        [Display (Name = "Spouse 2: ")]
        public string WedderTwo { get; set; }

        [Required]
        [Display (Name = "Date: ")]
        public string WeddingDate { get; set; }

        [Required]
        [Display (Name = "Address: ")]
        public string WeddingAddress { get; set; }

        public List<Reservation> Wedders {get; set;}

        //used to iterate through a list incase of a null value
        public Wedding(){
            Wedders = new List<Reservation>();
        }
    }
}
