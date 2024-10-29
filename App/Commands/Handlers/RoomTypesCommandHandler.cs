using GuestlineChallenge.Services;

namespace GuestlineChallenge.Commands.Handlers;

public class RoomTypesCommandHandler : ICommandHandler<RoomTypesCommand>
{
    private readonly IReservationService _reservationService;

    public RoomTypesCommandHandler(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }
    
    public void Handle(RoomTypesCommand command)
    {
        var roomTypes = _reservationService.GetRoomTypesNeededForGuestAllocation(command.HotelId, command.StartDate, command.EndDate, command.GuestCount);
        Console.WriteLine(roomTypes.Count == 0 ? "Allocation is not possible" : $"{string.Join(", ", roomTypes)}");
    }
}