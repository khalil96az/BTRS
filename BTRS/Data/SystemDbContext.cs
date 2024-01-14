using BTRS.Models;
using Microsoft.EntityFrameworkCore;

namespace BTRS.Data
{
    public class SystemDbContext : DbContext
    {
        public SystemDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Passenger> passenger { set; get; }
        public DbSet<Bus> bus { set; get; }
        public DbSet<BusTrip> busTrip { set; get; }
        public DbSet<Admin> admin { set; get; }
        public DbSet<Passenger_User> passenger_Users { set; get; }
    }
}
