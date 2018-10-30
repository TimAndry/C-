using Microsoft.EntityFrameworkCore;
 
namespace TimPortfolio.Models
{
    public class LRContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public LRContext(DbContextOptions<LRContext> options) : base(options) { }
        public DbSet<Contact> Contacts {get; set;}
    }
}