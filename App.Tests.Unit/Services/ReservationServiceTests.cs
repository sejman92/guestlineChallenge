using GuestlineChallenge.Models;
using GuestlineChallenge.Services;
using GuestlineChallenge.ValueObjects;
using Moq;

namespace App.Tests.Unit.Services;

[TestFixture]
public class ReservationServiceTests
{
    private Mock<IDataLoaderService<Hotel>> _hotelDataLoaderServiceMock;
    private Mock<IDataLoaderService<Booking>> _bookingDataLoaderServiceMock;
    private IReservationService _reservationService;

    [SetUp]
    public void SetUp()
    {
        _hotelDataLoaderServiceMock = new Mock<IDataLoaderService<Hotel>>();
        _bookingDataLoaderServiceMock = new Mock<IDataLoaderService<Booking>>();
    }

    [Test]
    public void GetNumberOfAvailableRooms_ValidParameters_ReturnsCorrectCount()
    {
        // Arrange
        var hotelId = "H1";
        var startDate = StartDate.Create("20231001");
        var endDate = EndDate.Create("20231005");
        var roomType = "RT1";

        var hotels = new List<Hotel>
        {
            new()
            {
                Id = hotelId,
                Rooms = new List<Room>
                {
                    new () { RoomType = roomType },
                    new () { RoomType = roomType }
                }
            }
        };

        var bookings = new List<Booking>
        {
            new() { HotelId = hotelId, RoomType = roomType, Arrival = new DateTime(2023, 10, 2), Departure = new DateTime(2023, 10, 4) }
        };

        _hotelDataLoaderServiceMock.Setup(h => h.LoadData()).Returns(hotels);
        _bookingDataLoaderServiceMock.Setup(b => b.LoadData()).Returns(bookings);
        _reservationService = new ReservationService(_hotelDataLoaderServiceMock.Object, _bookingDataLoaderServiceMock.Object);
        
        // Act
        var result = _reservationService.GetNumberOfAvailableRooms(hotelId, startDate, endDate, roomType);

        // Assert
        Assert.AreEqual(1, result);
    }

    [TestCase(1, new [] {"SGL"})]
    [TestCase(2, new [] {"DBL"})]
    [TestCase(3, new [] {"DBL","SGL"})]
    [TestCase(4, new [] {"DBL","DBL"})]
    [TestCase(5, new [] {"DBL","DBL", "SGL"})]
    public void GetRoomTypesNeededForGuestAllocation_ValidParameters_ReturnsCorrectRoomTypes_WhenNoBookings(int guestCount, string[] expectedRooms)
    {
        // Arrange
        var hotelId = "H1";
        var startDate = StartDate.Create("20231001");
        var endDate = EndDate.Create("20231005");

        var hotels = new List<Hotel>
        {
            new Hotel
            {
                Id = hotelId,
                Rooms = new List<Room>
                {
                    new Room { RoomType = "DBL", RoomId = "1"},
                    new Room { RoomType = "SGL", RoomId = "2"},
                    new Room { RoomType = "DBL", RoomId = "3"}
                }
            }
        };

        var bookings = new List<Booking>();

        _hotelDataLoaderServiceMock.Setup(h => h.LoadData()).Returns(hotels);
        _bookingDataLoaderServiceMock.Setup(b => b.LoadData()).Returns(bookings);
        _reservationService = new ReservationService(_hotelDataLoaderServiceMock.Object, _bookingDataLoaderServiceMock.Object);

        // Act
        var result = _reservationService.GetRoomTypesNeededForGuestAllocation(hotelId, startDate, endDate, guestCount);

        // Assert
        Assert.AreEqual(expectedRooms.Length, result.Count);
        CollectionAssert.AreEquivalent(expectedRooms, result);
    }
    
    [TestCase(1, "20241001", "20241003", new [] {"DBL!"})]
    [TestCase(2, "20241001", "20241003", new [] {"DBL"})]
    [TestCase(3, "20241001", "20241003", new [] {"DBL","DBL!"})]
    [TestCase(3, "20241001", null, new [] {"DBL","SGL"})]
    [TestCase(5, "20241001", "20241002", new [] {"DBL","DBL", "SGL"})]
    public void GetRoomTypesNeededForGuestAllocation_ValidParameters_ReturnsCorrectRoomTypes_WhenBookings(int guestCount, string baseStartDate, string baseEndDate, string[] expectedRooms)
    {
        // Arrange
        var hotelId = "H1";
        var startDate = StartDate.Create(baseStartDate);
        var endDate = EndDate.Create(baseEndDate);

        var hotels = new List<Hotel>
        {
            new ()
            {
                Id = hotelId,
                Rooms = new List<Room>
                {
                    new () { RoomType = "DBL", RoomId = "1"},
                    new () { RoomType = "SGL", RoomId = "2"},
                    new () { RoomType = "DBL", RoomId = "3"}
                }
            }
        };

        var bookings = new List<Booking>
        {
            new() { HotelId = hotelId, RoomType = "SGL", Arrival = new DateTime(2024, 10, 2), Departure = new DateTime(2024, 10, 4) }
        };

        _hotelDataLoaderServiceMock.Setup(h => h.LoadData()).Returns(hotels);
        _bookingDataLoaderServiceMock.Setup(b => b.LoadData()).Returns(bookings);
        _reservationService = new ReservationService(_hotelDataLoaderServiceMock.Object, _bookingDataLoaderServiceMock.Object);

        // Act
        var result = _reservationService.GetRoomTypesNeededForGuestAllocation(hotelId, startDate, endDate, guestCount);

        // Assert
        Assert.AreEqual(expectedRooms.Length, result.Count);
        CollectionAssert.AreEquivalent(expectedRooms, result);
    }
}