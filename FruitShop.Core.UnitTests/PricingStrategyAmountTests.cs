using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitShop.Core.UnitTests;
public class PricingStrategyAmountTests
{
    [Fact]
    public async Task GetTotalPrice_ShouldReturnCorrectTotalPrice()
    {
        // Arrange
        var pricingStrategy = new Implementation.PricingStrategyAmount();
        int amount = 5; // number of fruits
        decimal basePrice = 2.0m; // price per fruit
        // Act
        var totalPrice = await pricingStrategy.GetCost(amount, basePrice);
        // Assert
        Assert.Equal(10.0m, totalPrice);
    }
}
