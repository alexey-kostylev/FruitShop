namespace FruitShop.Core.Models;

/// <summary>
/// Order request containing items to be ordered.
/// </summary>
public record OrderRequest
{
    public IReadOnlyCollection<OrderRequestItem> Items { get; init; } = [];
}

/// <summary>
/// Request item for an order.
/// </summary>
/// <param name="Name"></param>
/// <param name="Quantity">Quantity of the item. Grams or number of fruits</param>
public record OrderRequestItem(string Name, int Quantity);