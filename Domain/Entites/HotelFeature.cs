using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class HotelFeature
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = null!;

        public string Icon { get; private set; } = null!;

        public int HotelId { get; private set; }

        public Hotel Hotel { get; private set; } = null!;

        public HotelFeature()
        {
            // ef
        }

        public HotelFeature(string name, string icon, int hotelId)
        {
            Name = name;
            Icon = icon;
            HotelId = hotelId;
        }
    }
}
