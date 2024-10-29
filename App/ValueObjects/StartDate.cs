using GuestlineChallenge.Helpers;

namespace GuestlineChallenge.ValueObjects;

public class StartDate : ValueObject
{
    public DateTime Value { get; }

    public static StartDate Create(string startDate)
    {
        if (string.IsNullOrWhiteSpace(startDate))
        {
            throw new ArgumentException($"{nameof(StartDate)} cannot be null or empty");
        }

        if (!startDate.TryParse(out var result))
        {
            throw new ArgumentException($"{nameof(startDate)} is not a valid date nor format");
        }

        return new StartDate(result);
    }
    
    private StartDate(DateTime value)
    {
        Value = value;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}