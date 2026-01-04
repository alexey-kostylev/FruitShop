using AwesomeAssertions;

using FruitShop.Core;
using FruitShop.Core.Models;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Xunit.Abstractions;

namespace FruitShop.FunctionalTests;

public class OrderServiceTests
{
    [Fact]
    public async Task ShouldCreateOrder()
    {
        var services = new ServiceCollection();

        services.RegisterDependencies();

        var serviceDescriptor = services.FirstOrDefault(d => d.ServiceType == typeof(IStoreService));

        if (serviceDescriptor != null)
        {
            services.Remove(serviceDescriptor);
        }

        services.AddSingleton<IStoreService, DummyStoreService>();

        var serviceProvider = services.BuildServiceProvider();

        var serviceOrder = serviceProvider.GetRequiredService<IOrderService>();

        var order = await serviceOrder.PlaceOrder(new Core.Models.OrderRequest
        {
            Items = [
                // weight strategy should be used
                new OrderRequestItem("Apple", 2000), // 2kg
                // amount strategy should be used
                new OrderRequestItem("Banana", 5), // 5 items
                // discount strategy should be used
                new OrderRequestItem("Cherry", 6500), // 6.5kg
            ]
        });

        order.Should().NotBeNull();
        order.Items.Should().HaveCount(3);
        order.Items.Single(i => i.Name == "Apple").Price.Should().Be(4.0m);
        order.Items.Single(i => i.Name == "Banana").Price.Should().Be(1.5m);
        order.Items.Single(i => i.Name == "Cherry").Price.Should().Be(30.25m);
        order.Summary.SubTotal.Should().Be(4.0m + 1.5m + 30.25m);
        order.Summary.Tax.Should().Be(3.58m);
        order.Summary.Total.Should().Be(4.0m + 1.5m + 30.25m + 3.58m);
    }

    internal class DummyStoreService : IStoreService
    {
        public Task<Fruit?> GetItem(string name)
        {
            return name switch
            {
                "Apple" => Task.FromResult<Fruit?>(new Fruit("Apple", 2.0m, PricingMode.Weight)),
                "Cherry" => Task.FromResult<Fruit?>(new Fruit("Cherry", 5.0m, PricingMode.Discount)),
                "Banana" => Task.FromResult<Fruit?>(new Fruit("Banana", 0.3m, PricingMode.Amount)),
                _ => Task.FromResult<Fruit?>(null)
            };
        }
    }
}
