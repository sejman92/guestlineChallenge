namespace GuestlineChallenge.Models;

public class Hotel
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public IReadOnlyList<RoomType> RoomTypes { get; private set; }
    public IReadOnlyCollection<Room> Rooms { get; private set; }
}