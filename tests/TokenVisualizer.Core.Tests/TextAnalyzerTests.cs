using TokenVisualizer.Core;
using Xunit;

namespace TokenVisualizer.Core.Tests;

public sealed class TextAnalyzerTests
{
    [Fact]
    public void Analyze_PlainEnglish_GivesOneSegmentPerToken()
    {
        var analysis = TextAnalyzer.Analyze(TokenizerCatalog.Get("o200k_base"), "Hello world");

        Assert.Equal(["Hello", " world"], analysis.Segments.Select(segment => segment.Text));
        Assert.All(analysis.Segments, segment => Assert.Single(segment.TokenIds));
        Assert.Equal(2, analysis.Tokens);
    }

    [Fact]
    public void Analyze_EmojiSplitInBytes_JoinsItsTokensInOneSegment()
    {
        // cl100k_base splits the pizza emoji into byte-sized tokens that cover the same characters.
        var analysis = TextAnalyzer.Analyze(TokenizerCatalog.Get("cl100k_base"), "Eat \U0001F355.");

        var pizza = Assert.Single(analysis.Segments, segment => segment.Text.Contains("\U0001F355", StringComparison.Ordinal));
        Assert.True(pizza.TokenIds.Count > 1);
        Assert.Equal(analysis.Tokens, analysis.Segments.Sum(segment => segment.TokenIds.Count));
    }

    [Theory]
    [InlineData("o200k_base")]
    [InlineData("cl100k_base")]
    public void Analyze_AnyText_SegmentsRebuildTheTextExactly(string tokenizer)
    {
        const string text = "La niña comió \U0001F355 en Cádiz.\n\tvar x = 1; // 東京";

        var analysis = TextAnalyzer.Analyze(TokenizerCatalog.Get(tokenizer), text);

        Assert.Equal(text, string.Concat(analysis.Segments.Select(segment => segment.Text)));
        Assert.Equal(analysis.Tokens, analysis.Segments.Sum(segment => segment.TokenIds.Count));
    }

    [Fact]
    public void Analyze_CountsCharactersAsPeopleDo()
    {
        // An emoji with a skin tone is two code points and four chars, but one character on screen.
        var analysis = TextAnalyzer.Analyze(TokenizerCatalog.Get("o200k_base"), "Café \U0001F355 \U0001F44D\U0001F3FD");

        Assert.Equal(8, analysis.Characters);
        Assert.Equal(3, analysis.Words);
    }

    [Fact]
    public void Analyze_EmptyText_HasNoTokensAndNoRatio()
    {
        var analysis = TextAnalyzer.Analyze(TokenizerCatalog.Get("o200k_base"), "");

        Assert.Empty(analysis.Segments);
        Assert.Equal(0, analysis.Tokens);
        Assert.Null(analysis.TokensPerWord);
    }

    [Fact]
    public void TokensPerWord_IsTokensDividedByWords()
    {
        var analysis = new TextAnalysis([], Tokens: 6, Characters: 20, Words: 4);

        Assert.Equal(1.5, analysis.TokensPerWord);
    }
}
