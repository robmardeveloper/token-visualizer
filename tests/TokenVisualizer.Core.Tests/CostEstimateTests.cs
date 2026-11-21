using TokenVisualizer.Core;
using Xunit;

namespace TokenVisualizer.Core.Tests;

public sealed class CostEstimateTests
{
    [Theory]
    [InlineData(1_000_000, "3", "3")]
    [InlineData(2_500, "3", "0.0075")]
    [InlineData(0, "15", "0")]
    public void For_MultipliesTokensByThePricePerMillion(int tokens, string price, string expected)
    {
        var cost = CostEstimate.For(tokens, decimal.Parse(price, System.Globalization.CultureInfo.InvariantCulture));

        Assert.Equal(decimal.Parse(expected, System.Globalization.CultureInfo.InvariantCulture), cost);
    }

    [Fact]
    public void For_NegativePrice_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => CostEstimate.For(10, -1m));
    }
}
