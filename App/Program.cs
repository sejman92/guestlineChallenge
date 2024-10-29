using GuestlineChallenge;
using GuestlineChallenge.Commands;
using GuestlineChallenge.Commands.Handlers;
using GuestlineChallenge.Models;
using GuestlineChallenge.Services;
using Microsoft.Extensions.DependencyInjection;

var optionsManager = new OptionsManager(args);

var serviceProvider = new ServiceCollection()
    .AddSingleton<IDataLoaderService<Hotel>, HotelDataLoaderService>(_ => new HotelDataLoaderService(optionsManager.Options.HotelsFilePath))
    .AddSingleton<IDataLoaderService<Booking>, BookingDataLoaderService>(_ => new BookingDataLoaderService(optionsManager.Options.BookingsFilePath))
    .AddSingleton<IReservationService, ReservationService>(
        provider =>
        {
            var hotelDataLoaderService = provider.GetRequiredService<IDataLoaderService<Hotel>>();
            var bookingDataLoaderService = provider.GetRequiredService<IDataLoaderService<Booking>>();
            return new ReservationService(hotelDataLoaderService, bookingDataLoaderService);
        })
    .AddSingleton<ICommandHandler<AvailabilityCommand>, AvailabilityCommandHandler>()
    .AddSingleton<ICommandProcessor, CommandProcessor>()
    .BuildServiceProvider();

var commandProcessor = serviceProvider.GetService<ICommandProcessor>();

string command;

Console.WriteLine("Enter commands (blank line to exit):");

while (!string.IsNullOrWhiteSpace(command = Console.ReadLine()))
{
    try
    {
        commandProcessor.ProcessCommand(command);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
    
    Console.WriteLine("Enter commands (blank line to exit):");
}