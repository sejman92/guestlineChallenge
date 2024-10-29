using GuestlineChallenge.Commands;
using GuestlineChallenge.Commands.Handlers;
using GuestlineChallenge.Services;

namespace GuestlineChallenge;

public interface ICommandProcessor
{
    void ProcessCommand(string command);
}

public class CommandProcessor : ICommandProcessor
{
    private readonly ICommandHandler<AvailabilityCommand> _availabilityCommandHandler;
    private readonly ICommandHandler<RoomTypesCommand> _roomTypesCommandHandler;
    
    public CommandProcessor(ICommandHandler<AvailabilityCommand> availabilityCommandHandler, ICommandHandler<RoomTypesCommand> roomTypesCommandHandler)
    {
        _availabilityCommandHandler = availabilityCommandHandler;
        _roomTypesCommandHandler = roomTypesCommandHandler;
    }
    
    public void ProcessCommand(string command)
    {
        if (string.IsNullOrWhiteSpace(command))
        {
            Console.WriteLine("Command cannot be null or empty");
            return;
        }

        if (command.StartsWith("Availability"))
        {
            var (hotelId, startDate, endDate, roomType) = ParseAvailabilityCommand(command);

            _availabilityCommandHandler.Handle(
                AvailabilityCommand.Create(hotelId, startDate, endDate, roomType));
        }
        else if (command.StartsWith("RoomTypes"))
        {
            var (hotelId, startDate, endDate, guestCount) = ParseRoomTypesCommand(command);
            _roomTypesCommandHandler.Handle(RoomTypesCommand.Create(hotelId, startDate, endDate, guestCount));
        }
        else Console.WriteLine("Unknown command");
    }

    private static (string, string, string, string) ParseAvailabilityCommand(string command)
    {
        var commandData = command.Split('(', ')')[1].Split(',');
        if (commandData.Length == 3)
        {
            var dates = commandData[1].Split('-');
            
            return (commandData[0].Trim(), dates[0].Trim(), dates.Length > 1 ? dates[1].Trim() : null, commandData[2].Trim());
        }

        throw new Exception(
            "Invalid command format.");
    }
    
    private static (string, string, string, int) ParseRoomTypesCommand(string command)
    {
        var commandData = command.Split('(', ')')[1].Split(',');
        if (commandData.Length == 3)
        {
            var dates = commandData[1].Split('-');
            
            return (commandData[0].Trim(), dates[0].Trim(), dates.Length > 1 ? dates[1].Trim() : null, int.Parse(commandData[2].Trim()));
        }

        throw new Exception("Invalid command format.");
    }
}