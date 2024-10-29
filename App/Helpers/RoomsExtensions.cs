using GuestlineChallenge.Models;

namespace GuestlineChallenge.Helpers;

public static class RoomsExtensions
{
    public static IEnumerable<Room> FilterByRoomType(this IEnumerable<Room> rooms, string roomTypeCode)
    {
        return rooms.Where(r => r.RoomType == roomTypeCode);
    }
}