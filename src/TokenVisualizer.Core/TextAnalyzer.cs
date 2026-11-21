using System.Globalization;
using Microsoft.ML.Tokenizers;

namespace TokenVisualizer.Core;

public static class TextAnalyzer
{
    public static TextAnalysis Analyze(Tokenizer tokenizer, string text)
    {
        ArgumentNullException.ThrowIfNull(tokenizer);
        ArgumentNullException.ThrowIfNull(text);

        var tokens = tokenizer.EncodeToTokens(text, out _);
        return new TextAnalysis(ToSegments(tokens, text), tokens.Count, CountCharacters(text), CountWords(text));
    }

    /// <summary>
    /// Joins tokens whose text overlaps into one segment, so every character is shown once. Text no token covers
    /// (it should not happen) becomes a segment without tokens instead of being lost.
    /// </summary>
    private static List<TokenSegment> ToSegments(IReadOnlyList<EncodedToken> tokens, string text)
    {
        var segments = new List<TokenSegment>();
        var ids = new List<int>();
        int start = 0, end = 0;

        foreach (var token in tokens)
        {
            var (offset, length) = token.Offset.GetOffsetAndLength(text.Length);
            if (ids.Count > 0 && offset < end)
            {
                ids.Add(token.Id);
                end = Math.Max(end, offset + length);
                continue;
            }

            Flush();
            if (offset > end)
            {
                segments.Add(new TokenSegment(text[end..offset], []));
            }

            start = offset;
            end = offset + length;
            ids.Add(token.Id);
        }

        Flush();
        if (end < text.Length)
        {
            segments.Add(new TokenSegment(text[end..], []));
        }

        return segments;

        void Flush()
        {
            if (ids.Count > 0)
            {
                segments.Add(new TokenSegment(text[start..end], [.. ids]));
                ids.Clear();
            }
        }
    }

    /// <summary>Characters as a person counts them: an emoji or an accented letter is one.</summary>
    private static int CountCharacters(string text) => new StringInfo(text).LengthInTextElements;

    private static int CountWords(string text) =>
        text.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries).Length;
}
