using FruitShop.Core.Models;

namespace FruitShop.Core;

/// <summary>
/// Represents a service for placing orders.
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// Creates an order based on the given request.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<Order> PlaceOrder(OrderRequest request);
}