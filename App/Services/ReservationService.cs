using GuestlineChallenge.Exceptions;
using GuestlineChallenge.Helpers;
using GuestlineChallenge.Models;
using GuestlineChallenge.ValueObjects;

namespace GuestlineChallenge.Services;

public interface IReservationService
{
    int GetAvailability(string hotelId, StartDate startDate, EndDate endDate, string roomTypeCode);
    List<string> GetRoomTypesForGuests(string hotelId, StartDate startDate, EndDate endDate, int guestCount);
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

    public int GetAvailability(string hotelId, StartDate startDate, EndDate endDate, string roomType)
    {
        var allFilteredRooms = 0;
        var hotel = GetHotel(hotelId);
        
        if (hotel.Rooms != null)
        {
            allFilteredRooms = hotel.Rooms.FilterByRoomType(roomType).Count();
        }

        var totalBookings = GetTotalBookings(hotelId, roomType, startDate, endDate);

        return allFilteredRooms - totalBookings;
    }
    
    public List<string> GetRoomTypesForGuests(string hotelId, StartDate startDate, EndDate endDate, int guestCount)
    {
        throw new NotImplementedException();
    }

    private Hotel GetHotel(string hotelId)
    {
        var hotel = _hotels.FirstOrDefault(h => h.Id == hotelId);

        if (hotel == null)
        {
            throw new NotFoundException($"Hotel with id {hotelId} not found");
        }

        return hotel;
    }

    private int GetTotalBookings(string hotelId, string roomTypeCode, StartDate startDate, EndDate endDate)
    {
        return _bookings
            .FilterByHotelId(hotelId)
            .FilterByRoomType(roomTypeCode)
            .FilterByDateRange(startDate, endDate)
            .Count();
    }
}