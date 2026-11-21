namespace TokenVisualizer.Core;

/// <summary>A text the page can load with one click.</summary>
public sealed record SampleText(string Name, string Text);

/// <summary>
/// Samples chosen to show where token counts differ: the same paragraph in two languages, code, a test log and JSON.
/// </summary>
public static class SampleTexts
{
    public static IReadOnlyList<SampleText> All { get; } =
    [
        new("English paragraph",
            "Language models do not read letters or words. They read tokens: pieces of text taken from a fixed "
            + "vocabulary. Common English words are often a single token, while rare words are split into several."),
        new("Same paragraph in Spanish",
            "Los modelos de lenguaje no leen letras ni palabras. Leen tokens: trozos de texto sacados de un "
            + "vocabulario fijo. Las palabras comunes en inglés suelen ser un solo token, mientras que las palabras "
            + "poco frecuentes se parten en varios."),
        new("C# code",
            """
            public sealed class OrderService(IOrderRepository repository)
            {
                public async Task<decimal> GetTotalAsync(int orderId, CancellationToken cancellationToken)
                {
                    var order = await repository.FindAsync(orderId, cancellationToken)
                        ?? throw new InvalidOperationException($"Order {orderId} was not found.");
                    return order.Lines.Sum(line => line.Quantity * line.UnitPrice);
                }
            }
            """),
        new("dotnet test log",
            """
              Determining projects to restore...
              All projects are up-to-date for restore.
              Shop.Core -> /home/runner/work/shop/src/Shop.Core/bin/Release/net10.0/Shop.Core.dll
              Shop.Core.Tests -> /home/runner/work/shop/tests/Shop.Core.Tests/bin/Release/net10.0/Shop.Core.Tests.dll
            failed Shop.Core.Tests.OrderServiceTests.GetTotalAsync_UnknownOrder_Throws (12ms)
              Assert.Throws() Failure: No exception was thrown
              Expected: typeof(System.InvalidOperationException)
                at Shop.Core.Tests.OrderServiceTests.GetTotalAsync_UnknownOrder_Throws() in OrderServiceTests.cs:41

            Test run summary: Failed! - total: 48, failed: 1, succeeded: 47, skipped: 0, duration: 1.8s
            """),
        new("JSON",
            """
            {
              "orderId": 1042,
              "customer": { "name": "Ana López", "email": "ana@example.com" },
              "lines": [
                { "sku": "BOOK-001", "quantity": 2, "unitPrice": 19.90 },
                { "sku": "PEN-017", "quantity": 10, "unitPrice": 1.25 }
              ],
              "paid": true
            }
            """),
    ];
}
