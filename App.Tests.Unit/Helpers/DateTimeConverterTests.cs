using System.Text.Json;
using GuestlineChallenge.Helpers;

namespace App.Tests.Unit.Helpers;

[TestFixture]
public class DateTimeConverterTests
{
    private DateTimeConverter _sut;
    private JsonSerializerOptions _options;

    [SetUp]
    public void Setup()
    {
        _sut = new DateTimeConverter();
        _options = new JsonSerializerOptions
        {
            Converters = { _sut }
        };
    }

    [Test]
    public void Write_CorrectlyFormatsDate()
    {
        // Arrange
        var date = new DateTime(2024, 10, 30); // Example date
        var expectedString = date.ToString(Consts.DateFormat);

        // Act
        var json = JsonSerializer.Serialize(date, _options);

        // Assert
        Assert.That(json, Is.EqualTo($"\"{expectedString}\""));
    }

    [Test]
    public void Read_ParsesCorrectFormattedDate()
    {
        // Arrange
        var dateString = "20241030";
        var json = $"\"{dateString}\"";
        var expectedDate = DateTime.ParseExact(dateString, Consts.DateFormat, null);

        // Act
        var result = JsonSerializer.Deserialize<DateTime>(json, _options);

        // Assert
        Assert.That(result, Is.EqualTo(expectedDate));
    }

    [Test]
    public void Read_InvalidDateFormat_ThrowsJsonException()
    {
        // Arrange
        var invalidDate = "\"30-10-2024\"";

        // Act & Assert
        var ex = Assert.Throws<JsonException>(() =>
            JsonSerializer.Deserialize<DateTime>(invalidDate, _options));

        Assert.That(ex.Message, Contains.Substring($"Date format should be {Consts.DateFormat}"));
    }
}