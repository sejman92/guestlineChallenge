using GuestlineChallenge.Services;

namespace GuestlineChallenge;

public interface ICommandProcessor
{
    void ProcessCommand(string command);
}

public class CommandProcessor : ICommandProcessor
{
    private readonly IReservationService _reservationService;

    public CommandProcessor(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }
    
    public void ProcessCommand(string command)
    {
        if (string.IsNullOrWhiteSpace(command)) return;

        if (command.StartsWith("Availability"))
        {
            Console.WriteLine(command);
        }
        else Console.WriteLine("Unknown command");
    }
}