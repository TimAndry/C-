using Microsoft.EntityFrameworkCore;

namespace RESTauranter.Models
{
    public class RestauranterContext : DbContext
    {
        public RestauranterContext(DbContextOptions<RestauranterContext> options) : base(options) {}
        public DbSet<Restaurant> Review {get; set;}
    }
}