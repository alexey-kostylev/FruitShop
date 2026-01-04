
namespace FruitShop.Core;

public interface IPricingStrategy
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="amount">Amount of fruit (weight in grams or number of fruits)</param>
    /// <param name="basePrice">Base price per unit (per fruit or per kilogram)</param>
    /// <returns></returns>
    Task<decimal> GetCost(int amount, decimal basePrice);
}