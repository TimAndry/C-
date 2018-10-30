using Microsoft.EntityFrameworkCore;
 
namespace LoginRegistration.Models
{
    public class LRContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public LRContext(DbContextOptions<LRContext> options) : base(options) { }
        public DbSet<User> Users {get; set;}
        public DbSet<Wedding> Weddings {get; set;}
        public DbSet<Reservation> Reservations {get; set;}
    }
}
