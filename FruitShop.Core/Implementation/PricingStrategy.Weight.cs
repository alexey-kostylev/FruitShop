namespace FruitShop.Core.Implementation;
internal class PricingStrategyWeight : IPricingStrategy
{
    /// <summary>
    /// Amount is in grams
    /// </summary>
    /// <param name="amount">Amount in grams</param>
    /// <param name="basePrice">Base price per kilogram</param>
    /// <returns></returns>
    public Task<decimal> GetCost(int amount, decimal basePrice) => Task.FromResult(amount * basePrice / 1000m );
}
