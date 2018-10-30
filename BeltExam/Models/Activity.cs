using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
namespace LoginReg.Models
{
    public class Activity
    {
        [Key]

        public int ActivityId { get; set; }
        
        public int UserId { get; set;}
        public string Coordinator {get; set;}


        [Required (ErrorMessage = "What is the name of this event")]
        [MinLength (2, ErrorMessage = "Title must be at least 2 characters")]
        [Display (Name = "Title: ")]
        public string Title { get; set; }


        [Required (ErrorMessage = "What date will this event be held")]
        [Display (Name = "Date: ")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "How long will this event last?")]
        [Display (Name = "Duration: ")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Select the type of time needed for this event")]
        [Display (Name = "TimeType")]
        public string TimeType {get; set;}

        [Required(ErrorMessage = "Include a description for this event")]
        [MinLength(10, ErrorMessage="Description must be at least 10 characters")]
        [Display (Name = "Description")]
        public string Description {get; set;}
        
        public List<Reservation> Users {get; set;}

        //used to iterate through a list incase of a null value
        public Activity(){
            Users = new List<Reservation>();
        }
    }
}
