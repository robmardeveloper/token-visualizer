namespace TokenVisualizer.Core;

/// <summary>
/// What a number of tokens costs at a price the user types. No prices live in the code: they change too often.
/// </summary>
public static class CostEstimate
{
    public static decimal For(int tokens, decimal pricePerMillionTokens)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(tokens);
        ArgumentOutOfRangeException.ThrowIfNegative(pricePerMillionTokens);
        return tokens * pricePerMillionTokens / 1_000_000m;
    }
}
