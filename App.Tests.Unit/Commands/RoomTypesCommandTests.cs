using GuestlineChallenge.Commands;
using GuestlineChallenge.ValueObjects;

namespace App.Tests.Unit.Commands;

[TestFixture]
public class RoomTypesCommandTests
{
    [TestCase("h1", "20231001", null, 1)]
    [TestCase("very_long_hotel_id", "20231001", "20231010", 10)]
    [TestCase("h", "20231001", "20231002", 99999999)]
    public void Create_ValidParameters_ReturnsRoomTypesCommand(string hotelId, string baseStartDate, string baseEndDate, int guestCount)
    {
        // Arrange
        var startDate = StartDate.Create(baseStartDate);
        var endDate = EndDate.Create(baseEndDate);
        
        // Act
        var command = RoomTypesCommand.Create(hotelId, baseStartDate, baseEndDate, guestCount);

        // Assert
        Assert.NotNull(command);
        Assert.AreEqual(hotelId, command.HotelId);
        Assert.AreEqual(startDate, command.StartDate);
        Assert.AreEqual(endDate, command.EndDate);
        Assert.AreEqual(guestCount, command.GuestCount);
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("    ")]
    [TestCase("\n\t")]
    public void Create_InvalidHotelId_ThrowsArgumentException(string hotelId)
    {
        // Arrange
        var startDate = "20231001";
        var endDate = "20231010";
        var guestCount = 5;
        
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => RoomTypesCommand.Create(hotelId, startDate, endDate, guestCount));
        Assert.AreEqual("HotelId cannot be null or empty", ex.Message);
    }
    
    [TestCase("20231010", "20231010")]
    [TestCase("20231010", "20231010")]
    public void Create_StartDateAfterEndDate_ThrowsArgumentException(string startDate, string endDate)
    {
        // Arrange
        var hotelId = "H123";
        var guestCount = 4;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => RoomTypesCommand.Create(hotelId, startDate, endDate, guestCount));
        Assert.AreEqual("StartDate must be earlier than EndDate", ex.Message);
    }
    
    [TestCase(-1)]
    [TestCase(0)]
    public void Create_InvalidGuestCount_ThrowsArgumentException(int guestCount)
    {
        // Arrange
        var hotelId = "h1";
        var startDate = "20231001";
        var endDate = "20231010";
        
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => RoomTypesCommand.Create(hotelId, startDate, endDate, guestCount));
        Assert.AreEqual("GuestCount must be greater than 0", ex.Message);
    }
}