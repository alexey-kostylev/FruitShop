using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitShop.Core.UnitTests;
public class PricingStrategyWeightTests
{
    [Fact]
    public async Task GetTotalPrice_ShouldReturnCorrectTotalPrice()
    {
        // Arrange
        var pricingStrategy = new Implementation.PricingStrategyWeight();
        int amount = 1500; // amount in grams
        decimal basePrice = 4.0m; // price per kilogram
        // Act
        var totalPrice = await pricingStrategy.GetCost(amount, basePrice);
        // Assert
        Assert.Equal(6.0m, totalPrice);
    }
}
