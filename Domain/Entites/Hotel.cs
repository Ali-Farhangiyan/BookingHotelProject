using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Hotel
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = null!;

        public string Description { get; private set; } = null!;

        public string Slug { get; private set; } = null!;

        public int NumberOfStar { get; private set; } 

        public string City { get; private set; } = null!;

        public string Address { get; private set; } = null!;

        private readonly List<Room> rooms = new List<Room>();
        public ICollection<Room> Rooms => rooms.AsReadOnly();


        private readonly List<Image> images = new List<Image>();
        public ICollection<Image> Images => images.AsReadOnly();

        private readonly List<HotelFeature> hotelFeatures = new List<HotelFeature>();
        public ICollection<HotelFeature> HotelFeatures => hotelFeatures.AsReadOnly();

        public Hotel()
        {
            // ef core
        }

        public Hotel(string name, string description, int numberOfStar, string city, string address)
        {
            Name = name;
            Description = description;
            NumberOfStar = numberOfStar;
            City = city;
            Address = address;
            AddSlug();
        }

        private void AddSlug()
        {
            Slug = string.Join('-', Name.Split(" "));
        }

        public void AddRoom(Room room)
        {
            rooms.Add(room);
        }

        public void AddRoom(ICollection<Room> listOfRooms)
        {
            rooms.AddRange(listOfRooms);
        }

        public void AddImage(Image image)
        {
            images.Add(image);
        }

        public void AddImage(ICollection<Image> listOfImages)
        {
            images.AddRange(listOfImages);
        }

        public void AddHotelFeatures(ICollection<HotelFeature> listOfHotelFeatures)
        {
            hotelFeatures.AddRange(listOfHotelFeatures);
        }

        public void AddHotelFeatures(HotelFeature hotelFeature)
        {
            hotelFeatures.Add(hotelFeature);
        }

        public void UpdateHotel(string name, string description, int numberOfStar, string city, string address)
        {
            Name = name;
            Description = description;
            NumberOfStar = numberOfStar;
            City = city;
            Address = address;
            AddSlug();
        }

    }
}
