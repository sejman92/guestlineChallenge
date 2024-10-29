using GuestlineChallenge.Services;

namespace GuestlineChallenge.Commands.Handlers;

public class AvailabilityCommandHandler : ICommandHandler<AvailabilityCommand>
{
    private readonly IReservationService _reservationService;

    public AvailabilityCommandHandler(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    public void Handle(AvailabilityCommand command)
    {
        var availability = _reservationService.GetAvailability(command.HotelId, command.StartDate, command.EndDate, command.RoomTypeCode);
        Console.WriteLine($"Availability for {command.HotelId} for specified time range is: {availability}");
    }
}