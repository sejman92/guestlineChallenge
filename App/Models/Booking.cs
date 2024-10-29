using System.Text.Json.Serialization;
using GuestlineChallenge.Helpers;

namespace GuestlineChallenge.Models;

public class Booking
{
    public string HotelId { get; init; }
    
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Arrival { get; init; }
    
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Departure { get; init; }
    public string RoomType { get; init; }
}