using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class City
    {
        public int Id { get; private set; }

        public string CityName { get; private set; } = null!;

        private readonly List<Hotel> hotels = new List<Hotel>();
        public ICollection<Hotel> Hotels => hotels.AsReadOnly();

        public City(string cityName)
        {
            CityName = cityName;
        }
        public void AddHotel(Hotel hotel)
        {
            hotels.Add(hotel);
        }
        public City()
        {

        }
        public void UpdateCity(string cityName)
        {
            CityName = cityName;
        }
    }
}
