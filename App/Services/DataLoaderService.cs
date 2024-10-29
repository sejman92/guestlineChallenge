using System.Text.Json;
using GuestlineChallenge.Models;

namespace GuestlineChallenge.Services;

public interface IDataLoaderService<T>
{
    IEnumerable<T> LoadData();
}

public class HotelDataLoaderService : IDataLoaderService<Hotel>
{
    private readonly string _filePath;

    public HotelDataLoaderService(string filePath)
    {
        _filePath = filePath;
    }

    public IEnumerable<Hotel> LoadData() => JsonSerializer.Deserialize<List<Hotel>>(File.ReadAllText(_filePath),
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
}

public class BookingDataLoaderService : IDataLoaderService<Booking>
{
    private readonly string _filePath;

    public BookingDataLoaderService(string filePath)
    {
        _filePath = filePath;
    }
    
    public IEnumerable<Booking> LoadData() => JsonSerializer.Deserialize<List<Booking>>(File.ReadAllText(_filePath), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
}