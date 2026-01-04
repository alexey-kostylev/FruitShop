using FruitShop.Core.Models;

namespace FruitShop.Core;

public interface IPricingService
{
    IPricingStrategy GetPricingStrategy(PricingMode mode);
}