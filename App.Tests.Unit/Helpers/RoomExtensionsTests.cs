using GuestlineChallenge.Helpers;
using GuestlineChallenge.Models;
using GuestlineChallenge.ValueObjects;

namespace App.Tests.Unit.Helpers;

[TestFixture]
public class RoomExtensionsTests
{
    private List<Room> _rooms;
    private List<Booking> _bookings;

    [SetUp]
    public void Setup()
    {
        // Set up sample room data
        _rooms = new List<Room>
        {
            new Room { RoomId = "1", RoomType = "SGL" },
            new Room { RoomId = "2", RoomType = "DBL" },
            new Room { RoomId = "3", RoomType = "SGL" },
            new Room { RoomId = "4", RoomType = "DBL" }
        };

        // Set up sample bookings
        _bookings = new List<Booking>
        {
            new Booking { HotelId = "1", RoomType = "DBL", Arrival = new DateTime(2024, 10, 28), Departure = new DateTime(2024, 10, 30) },
            new Booking { HotelId = "1", RoomType = "SGL", Arrival = new DateTime(2024, 10, 28), Departure = new DateTime(2024, 10, 29) },
        };
    }

    [Test]
    public void FilterByRoomType_ReturnsOnlyMatchingRoomType()
    {
        // Act
        var filteredRooms = _rooms.FilterByRoomType("DBL").ToList();

        // Assert
        Assert.That(filteredRooms.Count, Is.EqualTo(2));
        Assert.That(filteredRooms.All(r => r.RoomType == "DBL"));
    }

    [Test]
    public void RemoveByRoomType_RemovesFirstMatchingRoomType()
    {
        // Act
        var resultRooms = _rooms.RemoveByRoomType("DBL").ToList();

        // Assert
        Assert.That(resultRooms.Count, Is.EqualTo(3));
        Assert.That(resultRooms.Any(r => r.RoomType == "DBL"), Is.True);
        Assert.That(resultRooms.Count(r => r.RoomType == "DBL"), Is.EqualTo(1));
    }

    [Test]
    public void RemoveByRoomType_NoMatch_ReturnsOriginalRooms()
    {
        // Act
        var resultRooms = _rooms.RemoveByRoomType("NonExistentType").ToList();

        // Assert
        Assert.That(resultRooms.Count, Is.EqualTo(_rooms.Count));
        CollectionAssert.AreEquivalent(resultRooms, _rooms);
    }

    [Test]
    public void GetAvailableRooms_ExcludesRoomsInBookingRange()
    {
        // Arrange
        var startDate = StartDate.Create("20241028");
        var endDate = EndDate.Create("20241029");

        // Act
        var availableRooms = _rooms.GetAvailableRooms(_bookings, startDate, endDate).ToList();

        // Assert
        Assert.That(availableRooms.Count, Is.EqualTo(2));
        Assert.That(availableRooms.Count(r => r.RoomType == "SGL"), Is.EqualTo(1));
        Assert.That(availableRooms.Count(r => r.RoomType == "DBL"), Is.EqualTo(1));
    }

    [Test]
    public void GetAvailableRooms_NoBookingsInRange_ReturnsAllRooms()
    {
        // Arrange
        var startDate = StartDate.Create("20241101");
        var endDate = EndDate.Create("20241102");

        // Act
        var availableRooms = _rooms.GetAvailableRooms(_bookings, startDate, endDate).ToList();

        // Assert
        Assert.That(availableRooms.Count, Is.EqualTo(_rooms.Count));
        CollectionAssert.AreEquivalent(availableRooms, _rooms);
    }
}   
