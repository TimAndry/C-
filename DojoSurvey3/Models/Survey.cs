using System.ComponentModel.DataAnnotations;
namespace RazorFun
{
    public class Survey
    {   
        //Name input has validation included
        [Required]
        [MinLength(3)]
        [Display(Name = "Full Name: ")]
        public string Name {get; set;}

        public string Language{get; set;}
        public string Location{get;set;}
        public string Comment{get;set;}
    }
}