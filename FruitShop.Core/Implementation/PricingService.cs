using FruitShop.Core.Models;

namespace FruitShop.Core.Implementation;
internal class PricingService(IServiceProvider serviceProvider) : IPricingService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public IPricingStrategy GetPricingStrategy(PricingMode mode)
        => mode switch
        {
            PricingMode.Amount => _serviceProvider.GetRequiredService<PricingStrategyAmount>(),
            PricingMode.Weight => _serviceProvider.GetRequiredService<PricingStrategyWeight>(),
            PricingMode.Discount => _serviceProvider.GetRequiredService<PricingStrategyDiscount>(),
            _ => throw new NotSupportedException($"Pricing mode {mode} is not supported."),
        };
}
