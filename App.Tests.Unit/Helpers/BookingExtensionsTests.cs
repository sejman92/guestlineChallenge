using GuestlineChallenge.Helpers;
using GuestlineChallenge.Models;
using GuestlineChallenge.ValueObjects;

namespace App.Tests.Unit.Helpers;

[TestFixture]
public class BookingExtensionsTests
{
    private List<Booking> _bookings;

    [SetUp]
    public void SetUp()
    {
        _bookings = new List<Booking>
        {
            new Booking { HotelId = "H1", RoomType = "RT1", Arrival = new DateTime(2023, 10, 1), Departure = new DateTime(2023, 10, 5) },
            new Booking { HotelId = "H2", RoomType = "RT2", Arrival = new DateTime(2023, 10, 3), Departure = new DateTime(2023, 10, 7) },
            new Booking { HotelId = "H1", RoomType = "RT2", Arrival = new DateTime(2023, 10, 6), Departure = new DateTime(2023, 10, 8) }
        };
    }

    [Test]
    public void FilterByHotelId_ValidHotelId_ReturnsFilteredBookings()
    {
        // Act
        var result = _bookings.FilterByHotelId("H1");

        // Assert
        Assert.AreEqual(2, result.Count());
        Assert.IsTrue(result.All(b => b.HotelId == "H1"));
    }

    [Test]
    public void FilterByRoomType_ValidRoomTypeCode_ReturnsFilteredBookings()
    {
        // Act
        var result = _bookings.FilterByRoomType("RT2");

        // Assert
        Assert.AreEqual(2, result.Count());
        Assert.IsTrue(result.All(b => b.RoomType == "RT2"));
    }

    [TestCase("20230920", "20230924", 0)]
    [TestCase("20231001", null, 1)]
    [TestCase("20231003", null, 2)]
    [TestCase("20231005", null, 1)]
    [TestCase("20231006", null, 2)]
    [TestCase("20231007", null, 1)]
    [TestCase("20231008", null, 0)]
    [TestCase("20230930", "20231010", 3)]
    [TestCase("20231001", "20231004", 2)]
    [TestCase("20231004", "20231010", 3)]
    public void FilterByDateRange_ValidDateRange_ReturnsFilteredBookings(string baseStartDate, string baseEndDate, int expectedNumberOfBookings)
    {
        // Arrange
        var startDate = StartDate.Create(baseStartDate);
        var endDate = EndDate.Create(baseEndDate);

        // Act
        var result = _bookings.FilterByDateRange(startDate, endDate);

        // Assert
        Assert.AreEqual(expectedNumberOfBookings, result.Count());
    }
}
