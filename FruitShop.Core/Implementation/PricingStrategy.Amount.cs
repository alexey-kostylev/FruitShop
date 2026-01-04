namespace FruitShop.Core.Implementation;
internal class PricingStrategyAmount : IPricingStrategy
{
    public Task<decimal> GetCost(int amount, decimal basePrice) => Task.FromResult(amount * basePrice);
}
