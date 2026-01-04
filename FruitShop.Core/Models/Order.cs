namespace FruitShop.Core.Models;

/// <summary>
/// Represents an order.
/// </summary>
public record Order
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Order"/> class.
    /// </summary>
    /// <param name="items"></param>
    /// <param name="gstRate">Decimal percentage (0..100)</param>
    public Order(IReadOnlyCollection<OrderItem> items, decimal gstRate)
    {
        Items = items;
        Summary = CalculateTotal(Items.Sum(item => item.Price), gstRate);
    }

    /// <summary>
    /// The items in the order.
    /// </summary>
    public IReadOnlyCollection<OrderItem> Items { get; }

    /// <summary>
    /// The total cost of the order, including GST.
    /// </summary>
    public OrderTotal Summary { get; }

    /// <summary>
    /// Calculates the total cost of the order, including GST.
    /// </summary>
    /// <param name="subTotal"></param>
    /// <param name="gstRate"></param>
    /// <returns></returns>
    private static OrderTotal CalculateTotal(decimal subTotal, decimal gstRate)
    {
        var tax = Math.Round(subTotal * gstRate, 2);
        return new(
            subTotal,
            tax,
            subTotal + tax
        );
    }
}

/// <summary>
/// The items in the order.
/// </summary>
/// <param name="Name"></param>
/// <param name="Quantity"></param>
/// <param name="Price"></param>
public record OrderItem(string Name, int Quantity, decimal Price);

/// <summary>
/// The total cost of the order, including GST.
/// </summary>
/// <param name="SubTotal">Subtotal</param>
/// <param name="Tax">Total tax</param>
/// <param name="Total">Total amount including tax</param>
public record OrderTotal(decimal SubTotal, decimal Tax, decimal Total);