using TokenVisualizer.Core;
using Xunit;

namespace TokenVisualizer.Core.Tests;

public sealed class SampleTextsTests
{
    [Fact]
    public void All_HaveTextAndDistinctNames()
    {
        Assert.All(SampleTexts.All, sample => Assert.False(string.IsNullOrWhiteSpace(sample.Text)));
        Assert.Distinct(SampleTexts.All.Select(sample => sample.Name));
    }

    [Fact]
    public void SpanishParagraph_TakesMoreTokensThanTheEnglishOne()
    {
        // The point of having both: the same meaning costs more tokens outside English.
        var tokenizer = TokenizerCatalog.Get("cl100k_base");
        var english = TextAnalyzer.Analyze(tokenizer, SampleTexts.All[0].Text);
        var spanish = TextAnalyzer.Analyze(tokenizer, SampleTexts.All[1].Text);

        Assert.True(spanish.Tokens > english.Tokens);
    }
}
