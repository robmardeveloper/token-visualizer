using TokenVisualizer.Core;
using Xunit;

namespace TokenVisualizer.Core.Tests;

public sealed class TokenizerCatalogTests
{
    [Fact]
    public void Get_EveryListedTokenizer_Loads()
    {
        Assert.All(TokenizerCatalog.All, info => Assert.NotNull(TokenizerCatalog.Get(info.Id)));
    }

    [Fact]
    public void Get_SameTokenizerTwice_ReturnsTheSameInstance()
    {
        Assert.Same(TokenizerCatalog.Get("o200k_base"), TokenizerCatalog.Get("o200k_base"));
    }

    [Fact]
    public void Get_UnknownTokenizer_Throws()
    {
        Assert.Throws<ArgumentException>(() => TokenizerCatalog.Get("claude"));
    }
}
