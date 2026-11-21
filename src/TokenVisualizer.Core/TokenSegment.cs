namespace TokenVisualizer.Core;

/// <summary>
/// A piece of the text and the tokens that cover it. Usually one token, but a character such as an emoji or an
/// accented letter can be split into several tokens of a few bytes each; those tokens share the same piece of text.
/// </summary>
public sealed record TokenSegment(string Text, IReadOnlyList<int> TokenIds);
