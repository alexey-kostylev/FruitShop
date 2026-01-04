using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitShop.Core.UnitTests;
public class PricingStrategyDiscountTests
{
    [Fact]
    public async Task GetTotalPrice_ShouldReturnCorrectTotalPrice_WhenAmountIsLessThanOrEqualTo2000()
    {
        // Arrange
        var pricingStrategy = new Implementation.PricingStrategyDiscount();
        int amount = 2000; // amount in grams
        decimal basePrice = 5.0m; // price per kilogram
        // Act
        var totalPrice = await pricingStrategy.GetCost(amount, basePrice);
        // Assert
        totalPrice. Should().Be(10.0m);
    }
    [Fact]
    public async Task GetTotalPrice_ShouldReturnCorrectTotalPrice_WhenAmountIsGreaterThan2000()
    {
        // Arrange
        var pricingStrategy = new Implementation.PricingStrategyDiscount();
        int amount = 3000; // amount in grams
        decimal basePrice = 5.0m; // price per kilogram
        // Act
        var totalPrice = await pricingStrategy.GetCost(amount, basePrice);
        // Assert
        // For the first 2000 grams: 5.0 * 2 = 10.0
        // For the next 1000 grams: 5.0 * 1 = 5.0 with a 10% discount => 4.5
        // Total = 10.0 + 4.5 = 14.5
        totalPrice.Should().Be(14.5m);
    }
}
