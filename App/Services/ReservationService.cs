using GuestlineChallenge.Models;

namespace GuestlineChallenge.Services;

public interface IReservationService
{
    int GetAvailability(string hotelId, DateTime startDate, DateTime endDate, RoomType roomType);
    List<string> GetRoomTypesForGuests(string hotelId, DateTime startDate, DateTime endDate, int guestCount);
}

public class ReservationService : IReservationService
{
    private readonly IEnumerable<Hotel> _hotels;
    private readonly IEnumerable<Booking> _bookings;

    public ReservationService(IDataLoaderService<Hotel> hotelDataLoaderService, IDataLoaderService<Booking> bookingDataLoaderService)
    {
        _hotels = hotelDataLoaderService.LoadData();
        _bookings = bookingDataLoaderService.LoadData();
    }
    
    public int GetAvailability(string hotelId, DateTime startDate, DateTime endDate, RoomType roomType)
    {
        throw new NotImplementedException();
    }

    public List<string> GetRoomTypesForGuests(string hotelId, DateTime startDate, DateTime endDate, int guestCount)
    {
        throw new NotImplementedException();
    }
}