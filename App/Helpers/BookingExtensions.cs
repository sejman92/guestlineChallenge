using GuestlineChallenge.Models;
using GuestlineChallenge.ValueObjects;

namespace GuestlineChallenge.Helpers;

public static class BookingExtensions
{
    public static IEnumerable<Booking> FilterByHotelId(this IEnumerable<Booking> bookings, string hotelId)
    {
        return bookings.Where(b => b.HotelId.Equals(hotelId, StringComparison.CurrentCultureIgnoreCase));
    }

    public static IEnumerable<Booking> FilterByRoomType(this IEnumerable<Booking> bookings, string roomTypeCode)
    {
        return bookings.Where(b => string.Equals(b.RoomType, roomTypeCode, StringComparison.CurrentCultureIgnoreCase));
    }

    public static IEnumerable<Booking> FilterByDateRange(this IEnumerable<Booking> bookings, StartDate startDate, EndDate endDate)
    {
        return endDate.HasValue 
            ? bookings.Where(b => b.Arrival < endDate.Value && b.Departure > startDate.Value) 
            : bookings.Where(b => b.Arrival <= startDate.Value && b.Departure >= startDate.Value);
    }
}