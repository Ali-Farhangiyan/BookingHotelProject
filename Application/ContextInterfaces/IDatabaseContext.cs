using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ContextInterfaces
{
    public interface IDatabaseContext
    {
        DbSet<Hotel> Hotels { get; set; }
        DbSet<Room> Rooms { get; set; }
        DbSet<Image> Images { get; set; }
        DbSet<Booking> Bookings { get; set; }
        DbSet<RoomFeature> RoomFeatures { get; set; }
        DbSet<HotelFeature> HotelFeatures { get; set; }

        DbSet<RoomBookingDate> RoomBookingDate { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<Payment> Payments { get; set; }
        DbSet<Comment> Comments { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
