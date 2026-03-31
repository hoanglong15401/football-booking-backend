using Microsoft.EntityFrameworkCore;
using FootballBooking.Api.Entities;

namespace FootballBooking.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<FootballField> FootballFields => Set<FootballField>();
        public DbSet<Booking> Bookings => Set<Booking>();
    }
}
