namespace GuestlineChallenge.Models;

public class RoomType
{
    public string Code { get; private set; }
    public string Description { get; private set; }
    public IReadOnlyCollection<string> Amenities { get; private set; }
    public IReadOnlyCollection<string> Features { get; private set; }
}