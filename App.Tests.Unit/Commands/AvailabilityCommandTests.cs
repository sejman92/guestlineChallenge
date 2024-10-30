using GuestlineChallenge.Commands;
using GuestlineChallenge.ValueObjects;

namespace App.Tests.Unit.Commands;

[TestFixture]
public class AvailabilityCommandTests
{
    [TestCase("h1", "20231001", null, "S")]
    [TestCase("very_long_hotel_id", "20231001", "20231010", "very_long_room_type_code")]
    [TestCase("h", "20231001", "20231002", "SGL")]
    [TestCase("h1", "20231001", "20241001", "DBL")]
    public void Create_ValidParameters_ReturnsAvailabilityCommand(string hotelId, string baseStartDate, string baseEndDate, string roomTypeCode)
    {
        // Arrange
        var startDate = StartDate.Create(baseStartDate);
        var endDate = EndDate.Create(baseEndDate);

        // Act
        var command = AvailabilityCommand.Create(hotelId, baseStartDate, baseEndDate, roomTypeCode);

        // Assert
        Assert.NotNull(command);
        Assert.AreEqual(hotelId, command.HotelId);
        Assert.AreEqual(startDate, command.StartDate);
        Assert.AreEqual(endDate, command.EndDate);
        Assert.AreEqual(roomTypeCode, command.RoomTypeCode);
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
        var roomTypeCode = "RT1";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => AvailabilityCommand.Create(hotelId, startDate, endDate, roomTypeCode));
        Assert.AreEqual("HotelId cannot be null or empty", ex.Message);
    }

    [TestCase("20231010", "20231010")]
    [TestCase("20231010", "20231010")]
    public void Create_StartDateAfterEndDate_ThrowsArgumentException(string startDate, string endDate)
    {
        // Arrange
        var hotelId = "H123";
        var roomTypeCode = "RT1";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => AvailabilityCommand.Create(hotelId, startDate, endDate, roomTypeCode));
        Assert.AreEqual("StartDate must be earlier than EndDate", ex.Message);
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("    ")]
    [TestCase("\n\t")]
    public void Create_InvalidRoomTypeCode_ThrowsArgumentException(string roomTypeCode)
    {
        // Arrange
        var hotelId = "h1";
        var startDate = "20231001";
        var endDate = "20231010";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => AvailabilityCommand.Create(hotelId, startDate, endDate, roomTypeCode));
        Assert.AreEqual("RoomTypeCode cannot be null or empty", ex.Message);
    }
}