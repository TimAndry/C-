using System.ComponentModel.DataAnnotations;
namespace LoginRegistration.Models
{
    public class Reservation
    {
        public int ReservationId {get; set;}

        public int UserId{get; set;}
        public User User{get; set;}

        public int WeddingId{get; set;}
        public Wedding Wedding{get; set;}

    }
}