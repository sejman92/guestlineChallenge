using GuestlineChallenge.Models;
using GuestlineChallenge.ValueObjects;

namespace GuestlineChallenge.Helpers;

public static class RoomExtensions
{
    public static IEnumerable<Room> FilterByRoomType(this IEnumerable<Room> rooms, string roomTypeCode)
    {
        return rooms.Where(r => r.RoomType == roomTypeCode);
    }

    public static IEnumerable<Room> RemoveByRoomType(this IEnumerable<Room> rooms, string roomTypeCode)
    {
        var roomToRemove = rooms.FirstOrDefault(r => r.RoomType == roomTypeCode);
        return roomToRemove != null 
            ? rooms.Where(r => r.RoomId != roomToRemove.RoomId) 
            : rooms;
    }

    public static IEnumerable<Room> GetAvailableRooms(this IEnumerable<Room> rooms, IEnumerable<Booking> bookings, StartDate startDate, EndDate endDate)
    {
        var reservedRooms = bookings.FilterByDateRange(startDate, endDate);

        foreach (var r in reservedRooms)
        {
            rooms = rooms.RemoveByRoomType(r.RoomType);
        }

        return rooms;
    }
}