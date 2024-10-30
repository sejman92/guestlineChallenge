using GuestlineChallenge.ValueObjects;

namespace App.Tests.Unit.ValueObjects;

[TestFixture]
public class StartDateTests
{
    [Test]
    public void Create_ValidDateString_ReturnsStartDate()
    {
        // Arrange
        var dateString = "20231001";

        // Act
        var result = StartDate.Create(dateString);

        // Assert
        Assert.AreEqual(new DateTime(2023, 10, 1), result.Value);
    }

    [TestCase("invalid-date")]
    [TestCase("2024-01-02")]
    [TestCase("20242222")]
    public void Create_InvalidDateString_ThrowsArgumentException(string dateString)
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => StartDate.Create(dateString));
        Assert.AreEqual("StartDate is not a valid date nor format", ex.Message);
    }
    
    [TestCase(null)]
    [TestCase("")]
    [TestCase("          ")]
    [TestCase("      \n    ")]
    [TestCase("      \t    ")]
    public void Create_NullOrEmpty_ThrowsArgumentException(string dateString)
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => StartDate.Create(dateString));
        Assert.AreEqual("StartDate cannot be null or empty", ex.Message);
    }
}
