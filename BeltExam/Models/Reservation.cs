using System.ComponentModel.DataAnnotations;
namespace LoginReg.Models
{
    public class Reservation
    {
        public int ReservationId {get; set;}

        public int UserId{get; set;}
        public User User{get; set;}

        public int ActivityId{get; set;}
        public Activity Activity{get; set;}

    }
}