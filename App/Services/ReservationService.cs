using GuestlineChallenge.Exceptions;
using GuestlineChallenge.Helpers;
using GuestlineChallenge.Models;
using GuestlineChallenge.ValueObjects;

namespace GuestlineChallenge.Services;

public interface IReservationService
{
    int GetNumberOfAvailableRooms(string hotelId, StartDate startDate, EndDate endDate, string roomTypeCode);
    List<string> GetRoomTypesNeededForGuestAllocation(string hotelId, StartDate startDate, EndDate endDate, int guestCount);
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

    public int GetNumberOfAvailableRooms(string hotelId, StartDate startDate, EndDate endDate, string roomType)
    {
        var result = new List<Room>();
        var hotelRooms = GetHotelRooms(hotelId);
        
        result = hotelRooms.FilterByRoomType(roomType).ToList();

        var bookedRoomsForGivenHotelAndRoomTypeAndDateRange = _bookings
            .FilterByHotelId(hotelId)
            .FilterByRoomType(roomType)
            .FilterByDateRange(startDate, endDate);
        
        return result.Count - bookedRoomsForGivenHotelAndRoomTypeAndDateRange.Count();
    }
    
    public List<string> GetRoomTypesNeededForGuestAllocation(string hotelId, StartDate startDate, EndDate endDate, int guestCount)
    {
        var result = new List<string>();
        var hotelRooms = GetHotelRooms(hotelId);
        var availableRooms = hotelRooms.GetAvailableRooms(_bookings.FilterByHotelId(hotelId), startDate, endDate);

        while (guestCount > 0)
        {
            var neededRoom = guestCount > 1 ? Consts.DoubleRoomTypeCode : Consts.SingleRoomTypeCode;

            var isRoomAvailable = availableRooms.Any(r => r.RoomType == neededRoom);

            switch (neededRoom)
            {
                case "DBL":
                    if (isRoomAvailable) // 2 persons can be allocated in a double room
                    {
                        result.Add(Consts.DoubleRoomTypeCode);
                        availableRooms = availableRooms.RemoveByRoomType(Consts.DoubleRoomTypeCode);
                        guestCount -= 2;
                    }
                    else //try allocate single room for 1 person
                    {
                        if (availableRooms.Any(r => r.RoomType == Consts.SingleRoomTypeCode))
                        {
                            result.Add(Consts.SingleRoomTypeCode);
                            availableRooms = availableRooms.RemoveByRoomType(Consts.SingleRoomTypeCode);
                            guestCount--;
                        }
                        else throw new Exception("Allocation is not possible");
                    }
                    break;
                case "SGL":
                    if (isRoomAvailable) // 1 person can be allocated in a single room
                    {
                        result.Add(Consts.SingleRoomTypeCode);
                        availableRooms = availableRooms.RemoveByRoomType(Consts.SingleRoomTypeCode);
                        guestCount--;
                    }
                    else // try allocate double room for 1 person
                    {
                        if (availableRooms.Any(r => r.RoomType == Consts.DoubleRoomTypeCode)) 
                        {
                            result.Add(Consts.PartiallyFilledDoubleRoomTypeCode);
                            availableRooms = availableRooms.RemoveByRoomType(Consts.DoubleRoomTypeCode);
                            guestCount--; 
                        }
                        else throw new Exception("Allocation is not possible");
                    }
                    break;
                default:
                    throw new Exception("Unsupported room type!");
            }
        }
        
        return result;
    }

    private IEnumerable<Room> GetHotelRooms(string hotelId)
    {
        var hotel = _hotels.FirstOrDefault(h => h.Id == hotelId);

        if (hotel == null)
        {
            throw new NotFoundException($"Hotel with id {hotelId} not found");
        }

        return hotel.Rooms;
    }
}