using GuestlineChallenge;
using GuestlineChallenge.Commands;
using GuestlineChallenge.Commands.Handlers;
using Moq;

namespace App.Tests.Unit;

[TestFixture]
public class CommandProcessorTests
{
    private Mock<ICommandHandler<AvailabilityCommand>> _availabilityCommandHandlerMock;
    private Mock<ICommandHandler<RoomTypesCommand>> _roomTypesCommandHandlerMock;

    [SetUp]
    public void SetUp()
    {
        _availabilityCommandHandlerMock = new Mock<ICommandHandler<AvailabilityCommand>>();
        _roomTypesCommandHandlerMock = new Mock<ICommandHandler<RoomTypesCommand>>();
    }
    
    [Test]
    public void ProcessCommand_UnknownCommand_ThrowsException()
    {
        // Arrange
        var command = "UnknownCommand(h1,20240303, Rabbit)";
        var commandProcessor = new CommandProcessor(_availabilityCommandHandlerMock.Object, _roomTypesCommandHandlerMock.Object);
        
        // Act & Assert
        var ex = Assert.Throws<Exception>(() => commandProcessor.ProcessCommand(command));
        Assert.AreEqual("Unknown command", ex.Message);
    }
    
    [TestCase("Availability(H1, 20240101, 20240104, SGL)")]
    [TestCase("Availability(H1, 20240101, 20240104, SGL, DBL)")]
    [TestCase("RoomTypes(H1, 20240101, 20240104, 4)")]
    [TestCase("RoomTypes(H1, 20240101, 20240104, 4-5)")]
    [TestCase("RoomTypes(H1, 20240101, 20240104, -5)")]
    public void ProcessCommand_InvalidCommandFormat_ThrowsException(string command)
    {
        // Arrange
        var commandProcessor = new CommandProcessor(_availabilityCommandHandlerMock.Object, _roomTypesCommandHandlerMock.Object);
        
        // Act & Assert
        var ex = Assert.Throws<Exception>(() => commandProcessor.ProcessCommand(command));
        Assert.AreEqual("Invalid command format", ex.Message);
    }
    
    [TestCase("Availability(H1, 20240101-20240104, SGL)")]
    [TestCase("Availability(H2, 20240101-20240104, DBL)")]
    [TestCase("Availability(H3, 20240101, DBL)")]
    public void ProcessCommand_AvailabilityCommand_HandlerCalled(string command)
    {
        // Arrange
        var commandProcessor = new CommandProcessor(_availabilityCommandHandlerMock.Object, _roomTypesCommandHandlerMock.Object);
        
        // Act
        commandProcessor.ProcessCommand(command);
        
        // Assert
        _availabilityCommandHandlerMock.Verify(h => h.Handle(It.IsAny<AvailabilityCommand>()), Times.Once);
    }
    
    [TestCase("RoomTypes(H1, 20240101-20240104, 4)")]
    [TestCase("RoomTypes(H1, 20240101, 4)")]
    [TestCase("RoomTypes(H3, 20240101-20240104, 1)")]
    public void ProcessCommand_RoomTypesCommand_HandlerCalled(string command)
    {
        // Arrange
        var commandProcessor = new CommandProcessor(_availabilityCommandHandlerMock.Object, _roomTypesCommandHandlerMock.Object);
        
        // Act
        commandProcessor.ProcessCommand(command);
        
        // Assert
        _roomTypesCommandHandlerMock.Verify(h => h.Handle(It.IsAny<RoomTypesCommand>()), Times.Once);
    }
}