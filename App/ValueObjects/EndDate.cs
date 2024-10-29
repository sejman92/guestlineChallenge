using GuestlineChallenge.Helpers;

namespace GuestlineChallenge.ValueObjects;

public class EndDate : ValueObject
{
    public DateTime? Value { get; }
    public bool HasValue => Value.HasValue;

    public static EndDate Create(string endDate)
    {
        if (string.IsNullOrWhiteSpace(endDate))
        {
            return new EndDate(null);
        }

        if (!endDate.TryParse(out var result))
        {
            throw new ArgumentException($"{nameof(endDate)} is not a valid date nor format");
        }

        return new EndDate(result);
    }
    
    private EndDate(DateTime? value)
    {
        Value = value;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}