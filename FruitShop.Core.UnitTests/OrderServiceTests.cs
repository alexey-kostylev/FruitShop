using FruitShop.Core.Implementation;
using FruitShop.Core.Models;

using Xunit.Abstractions;

namespace FruitShop.Core.UnitTests;

public class OrderServiceTests
{
    [Fact]
    public async Task PlaceOrder_ProductDoesNotExist_ThrowsInvalidOperationException()
    {
        // Arrange
        var storeServiceMock = new Mock<IStoreService>();
        storeServiceMock
            .Setup(ss => ss.GetItem(It.IsAny<string>()))
            .ReturnsAsync(null as Fruit)
            ;

        var pricingServiceMock = new Mock<IPricingService>();

        var orderService = new OrderService(storeServiceMock.Object, pricingServiceMock.Object);
        var request = new OrderRequest
        {
            Items =
            [
                new OrderRequestItem("Apple", 2)
            ]
        };
        // Act
        Func<Task> act = async () => await orderService.PlaceOrder(request);

        // Act & Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Unknown fruit: Apple");

        pricingServiceMock.Verify(ps => ps.GetPricingStrategy(It.IsAny<PricingMode>()), Times.Never);
    }

    [Fact]
    [Trait("Category", "Acceptance")]
    public async Task PlaceOrder_ReturnsCorrectOrder()
    {
        // Arrange
        var storeServiceMock = new Mock<IStoreService>();
        storeServiceMock
            .SetupSequence(ss => ss.GetItem(It.IsAny<string>()))
            .ReturnsAsync(new Fruit("Apple", 10, PricingMode.Amount))
            .ReturnsAsync(new Fruit("Banana", 10, PricingMode.Weight))
            ;

        var pricingServiceMock = new Mock<IPricingService>();

        pricingServiceMock
            .Setup(ps => ps.GetPricingStrategy(It.IsAny<PricingMode>()))
            .Returns(() =>
            {
                var pricingStrategyMock = new Mock<IPricingStrategy>();
                pricingStrategyMock
                    .Setup(ps => ps.GetCost(It.IsAny<int>(), It.IsAny<decimal>()))
                    .ReturnsAsync((int quantity, decimal basePrice) => quantity * 1.00m); // $1.00 per item
                return pricingStrategyMock.Object;
            });

        var orderService = new OrderService(storeServiceMock.Object, pricingServiceMock.Object);
        var request = new OrderRequest
        {
            Items = [
                new OrderRequestItem("Apple", 2),
                new OrderRequestItem("Banana", 5)
            ]
        };
        // Act
        var result = await orderService.PlaceOrder(request);
        // Assert
        result.Items.Count.Should().Be(2);
        result.Items.Single(i => i.Name == "Apple").Quantity.Should().Be(2);
        result.Items.Single(i => i.Name == "Apple").Price.Should().Be(2);
        result.Items.Single(i => i.Name == "Banana").Quantity.Should().Be(5);
        result.Items.Single(i => i.Name == "Banana").Price.Should().Be(5);

        result.Summary.Should().NotBeNull();

        storeServiceMock.Verify(ss => ss.GetItem("Apple"), Times.Once);
        storeServiceMock.Verify(ss => ss.GetItem("Banana"), Times.Once);

        pricingServiceMock.Verify(ps => ps.GetPricingStrategy(PricingMode.Amount), Times.Once);
        pricingServiceMock.Verify(ps => ps.GetPricingStrategy(PricingMode.Weight), Times.Once);
    }
}
