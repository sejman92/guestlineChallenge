namespace GuestlineChallenge.Models;

public class Booking
{
    public string HotelId { get; set; }
    public DateTime Arrival { get; set; }
    public DateTime Departure { get; set; }
    public RoomType RoomType { get; set; }
    public string RoomRate { get; set; }
}