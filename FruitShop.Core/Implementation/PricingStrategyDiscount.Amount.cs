namespace FruitShop.Core.Implementation;
internal class PricingStrategyDiscount : IPricingStrategy
{
    /// <summary>
    /// The threshold at which a discount is applied.
    /// </summary>
    private const int DiscountThreshold = 2000;
    private const decimal DiscountRate = 0.1m;

    /// <summary>
    /// Calculates the total cost with a discount applied to the amount exceeding the threshold.
    /// </summary>
    /// <param name="amount">amount in grams</param>
    /// <param name="basePrice"></param>
    /// <returns></returns>
    public Task<decimal> GetCost(int amount, decimal basePrice)
    {
        if (amount <= DiscountThreshold)
        {
            return Task.FromResult(basePrice * amount / 1000m);
        }

        var discountedAmount = amount - DiscountThreshold;
        var totalPrice = basePrice * discountedAmount / 1000m;

        return Task.FromResult(DiscountThreshold * basePrice / 1000m + totalPrice - totalPrice * DiscountRate);
    }
}
