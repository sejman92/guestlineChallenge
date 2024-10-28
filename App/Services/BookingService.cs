using GuestlineChallenge.Models;

namespace GuestlineChallenge.Services;

public interface IBookingService
{
    int GetAvailability(string hotelId, DateTime startDate, DateTime endDate, RoomType roomType);
    List<string> GetRoomTypes(string hotelId, DateTime startDate, DateTime endDate, int guestCount);
}

public class BookingService : IBookingService
{
    private readonly IEnumerable<Hotel> _hotels;
    private readonly IEnumerable<Booking> _bookings;

    public BookingService(HotelDataLoaderService hotelDataLoaderService, BookingDataLoaderService bookingDataLoaderService)
    {
        _hotels = hotelDataLoaderService.LoadData();
        _bookings = bookingDataLoaderService.LoadData();
    }

    public int GetAvailability(string hotelId, DateTime startDate, DateTime endDate, RoomType roomType)
    {
        throw new NotImplementedException();
    }

    public List<string> GetRoomTypes(string hotelId, DateTime startDate, DateTime endDate, int guestCount)
    {
        throw new NotImplementedException();
    }
}