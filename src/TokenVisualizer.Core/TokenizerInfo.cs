namespace TokenVisualizer.Core;

/// <summary>A tokenizer the page offers, with an honest note on which models use it.</summary>
public sealed record TokenizerInfo(string Id, string Name, string UsedBy);
