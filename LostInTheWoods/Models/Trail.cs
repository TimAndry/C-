using System;
using System.ComponentModel.DataAnnotations;
namespace LostInTheWoods.Models
{
    public class Trail
    {
        [Required]
        [Display (Name = "Trail Name: ")]
        public string TrailName {get; set;}
        [Required]
        [Display (Name = "Trail Length: ")]
        public float TrailLength {get; set;}
        [Required]
        [Display (Name = "Description: ")]
        public string TrailDescription {get; set;}
        [Required]
        [Display (Name = "Elevation Change: ")]
        public int ElevationChange {get; set;}

        public int idTrails { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }

}