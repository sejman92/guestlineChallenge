using GuestlineChallenge.ValueObjects;

namespace GuestlineChallenge.Commands;

public class RoomTypesCommand : ICommand
{
    public string HotelId { get; }
    public StartDate StartDate { get; }
    public EndDate EndDate { get; }
    public int GuestCount { get; }
    
    public static RoomTypesCommand Create(string hotelId, string startDate, string endDate, int guestCount)
    {
        var startDateValue = StartDate.Create(startDate);
        var endDateValue = EndDate.Create(endDate);
        
        Validate(hotelId, startDateValue, endDateValue, guestCount);
        
        return new RoomTypesCommand(hotelId, startDateValue, endDateValue, guestCount);
    }
    
    private static void Validate(string hotelId, StartDate startDate, EndDate endDate, int guestCount)
    {
        if (string.IsNullOrWhiteSpace(hotelId))
        {
            throw new ArgumentException($"{nameof(HotelId)} cannot be null or empty");
        }

        if (endDate.HasValue && startDate.Value >= endDate.Value)
        {
            throw new ArgumentException($"{nameof(StartDate)} must be earlier than {nameof(EndDate)}");
        }
        
        if (guestCount <= 0)
        {
            throw new ArgumentException($"{nameof(GuestCount)} must be greater than 0");
        }
    }

    private RoomTypesCommand(string hotelId, StartDate startDate, EndDate endDate, int guestCount)
    {
        HotelId = hotelId;
        StartDate = startDate;
        EndDate = endDate;
        GuestCount = guestCount;
    }
}