using GuestlineChallenge.ValueObjects;

namespace App.Tests.Unit.ValueObjects;

[TestFixture]
public class EndDateTests
{
    [Test]
    public void Create_ValidDateString_ReturnsEndDateDate()
    {
        // Arrange
        var dateString = "20231001";

        // Act
        var result = EndDate.Create(dateString);

        // Assert
        Assert.AreEqual(new DateTime(2023, 10, 1), result.Value);
    }

    [TestCase("invalid-date")]
    [TestCase("2024-01-02")]
    [TestCase("20242222")]
    public void Create_InvalidDateString_ThrowsArgumentException(string dateString)
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => EndDate.Create(dateString));
        Assert.AreEqual("EndDate is not a valid date nor format", ex.Message);
    }
    
    [TestCase(null)]
    [TestCase("")]
    [TestCase("          ")]
    [TestCase("      \n    ")]
    [TestCase("      \t    ")]
    public void Create_NullOrWhitespace_ReturnsEndDate(string dateString)
    {
        // Act
        var result = EndDate.Create(dateString);

        // Assert
        Assert.That(result.HasValue, Is.False);
    }
}