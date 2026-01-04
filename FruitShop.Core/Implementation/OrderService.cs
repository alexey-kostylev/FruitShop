using System.Collections.Concurrent;
using System.Collections.Generic;

using FruitShop.Core.Models;

namespace FruitShop.Core.Implementation;

/// <summary>
///  Represents a service for placing orders.
/// </summary>
/// <param name="storeService">The service used to access store items.</param>
/// <param name="pricingService">The service used to determine pricing strategies.</param>
internal class OrderService(IStoreService storeService, IPricingService pricingService) : IOrderService
{
    private const decimal GSTRate = 0.1m;
    private readonly IStoreService _storeService = storeService;
    private readonly IPricingService _pricingService = pricingService;

    /// <summary>
    /// Places an order based on the given request.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<Order> PlaceOrder(OrderRequest request)
    {
        ConcurrentDictionary<PricingMode, IPricingStrategy> pricingStrategies = new();
        var items = new List<OrderItem>();
        foreach (var item in request.Items)
        {
            var fruit = await _storeService.GetItem(item.Name) ??
                throw new InvalidOperationException($"Unknown fruit: {item.Name}");

            var pricingStrategy = pricingStrategies.GetOrAdd(
                fruit.PricingMode,
                _ => _pricingService.GetPricingStrategy(fruit.PricingMode)
            );

            var fruitCost = await pricingStrategy.GetCost(item.Quantity, fruit.BasePrice);
            items.Add(new OrderItem(item.Name, item.Quantity, fruitCost));
        }

        return new Order(items, GSTRate);
    }
}
