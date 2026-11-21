namespace TokenVisualizer.Core;

/// <summary>How a tokenizer splits a text, and the counts the page shows.</summary>
public sealed record TextAnalysis(IReadOnlyList<TokenSegment> Segments, int Tokens, int Characters, int Words)
{
    /// <summary>Tokens per word, or null when the text has no words.</summary>
    public double? TokensPerWord => Words == 0 ? null : (double)Tokens / Words;
}
