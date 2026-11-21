using System.Collections.Concurrent;
using Microsoft.ML.Tokenizers;

namespace TokenVisualizer.Core;

/// <summary>
/// The tokenizers that ship with Microsoft.ML.Tokenizers and run in the browser. Claude's tokenizer is not public, so
/// it is not here.
/// </summary>
public static class TokenizerCatalog
{
    public static IReadOnlyList<TokenizerInfo> All { get; } =
    [
        new("o200k_base", "o200k_base", "OpenAI GPT-4o and later models"),
        new("cl100k_base", "cl100k_base", "OpenAI GPT-4 and GPT-3.5 Turbo"),
    ];

    // Loading a vocabulary takes a moment, so each one is loaded once and kept.
    private static readonly ConcurrentDictionary<string, Lazy<Tokenizer>> Loaded = new(StringComparer.Ordinal);

    public static Tokenizer Get(string id)
    {
        if (!All.Any(info => info.Id == id))
        {
            throw new ArgumentException($"Unknown tokenizer '{id}'.", nameof(id));
        }

        return Loaded.GetOrAdd(id, key => new Lazy<Tokenizer>(() => TiktokenTokenizer.CreateForEncoding(key))).Value;
    }
}
