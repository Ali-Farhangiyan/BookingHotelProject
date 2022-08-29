namespace Domain.Entites
{
    public class RoomFeature
    {
        public int Id { get;private set; }

        public string Name { get; private set; } = null!;

        public string Icon { get; private set; } = null!;

        public int RoomId { get; private set; }

        public Room Room { get; private set; } = null!;

        public RoomFeature()
        {
            // ef
        }

        public RoomFeature(string name, string icon, int roomId)
        {
            Name = name;
            Icon = icon;
            RoomId = roomId;
        }
    }
}
