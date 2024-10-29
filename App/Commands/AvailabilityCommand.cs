using GuestlineChallenge.ValueObjects;

namespace GuestlineChallenge.Commands;

public class AvailabilityCommand : ICommand
{
    public string HotelId { get;}
    public StartDate StartDate { get; }
    public EndDate EndDate { get; }
    public string RoomTypeCode { get; }
    
    public static AvailabilityCommand Create(string hotelId, string startDate, string endDate, string roomTypeCode)
    {
        var startDateValue = StartDate.Create(startDate);
        var endDateValue = EndDate.Create(endDate);
        
        Validate(hotelId, startDateValue, endDateValue, roomTypeCode);
        
        return new AvailabilityCommand(hotelId, startDateValue, endDateValue, roomTypeCode);
    }

    private static void Validate(string hotelId, StartDate startDate, EndDate endDate, string roomTypeCode)
    {
        if (string.IsNullOrWhiteSpace(hotelId))
        {
            throw new ArgumentException($"{nameof(HotelId)} cannot be null or empty");
        }
        
        if (endDate.HasValue && startDate.Value >= endDate.Value)
        {
            throw new ArgumentException($"{nameof(StartDate)} must be earlier than {nameof(EndDate)}");
        }

        if (string.IsNullOrWhiteSpace(roomTypeCode))
        {
            throw new ArgumentException($"{nameof(RoomTypeCode)} cannot be null or empty");
        }
    }
    
    private AvailabilityCommand(string hotelId, StartDate startDate, EndDate endDate, string roomTypeCode)
    {
        HotelId = hotelId;
        StartDate = startDate;
        EndDate = endDate;
        RoomTypeCode = roomTypeCode;
    }
}