namespace GuestlineChallenge.Models;

public class Hotel
{
    public string Id { get; init; }
    public string Name { get; set; }
    public IReadOnlyList<RoomType> RoomTypes { get; init; }
    public IReadOnlyCollection<Room> Rooms { get; init; }
}