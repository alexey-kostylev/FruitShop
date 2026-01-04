using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FruitShop.Core.Implementation;
using FruitShop.Core.Models;

using Microsoft.Extensions.DependencyInjection;

namespace FruitShop.Core.UnitTests;
public class PricingServiceTests
{
    [Fact]
    public void GetPricingStrategy_ReturnsCorrectStrategy()
    {
        // Arrange
        var serviceProviderMock = new Mock<IServiceProvider>();
        serviceProviderMock.Setup(sp => sp.GetService(typeof(PricingStrategyAmount))).Returns(new PricingStrategyAmount());
        serviceProviderMock.Setup(sp => sp.GetService(typeof(PricingStrategyWeight))).Returns(new PricingStrategyWeight());
        serviceProviderMock.Setup(sp => sp.GetService(typeof(PricingStrategyDiscount))).Returns(new PricingStrategyDiscount());

        var pricingService = new PricingService(serviceProviderMock.Object);

        // Act
        var amountStrategy = pricingService.GetPricingStrategy(PricingMode.Amount);
        var weightStrategy = pricingService.GetPricingStrategy(PricingMode.Weight);
        var discountStrategy = pricingService.GetPricingStrategy(PricingMode.Discount);

        // Assert
        amountStrategy.Should().BeOfType<PricingStrategyAmount>();
        weightStrategy.Should().BeOfType<PricingStrategyWeight>();
        discountStrategy.Should().BeOfType<PricingStrategyDiscount>();
    }
}
