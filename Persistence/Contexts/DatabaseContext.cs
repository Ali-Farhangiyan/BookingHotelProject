using Application.ContextInterfaces;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
        {

        }

        public DbSet<Hotel> Hotels { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<Image> Images { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<RoomFeature> RoomFeatures { get; set; } = null!;
        public DbSet<HotelFeature> HotelFeatures { get; set; } = null!;
    }
}
