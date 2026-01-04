# FruitShop

solution contains 3 projects:
* FruitShop.Core
* FruitShop.Core.UnitTests
* FruitShop.Core.FunctionalTests


## FruitShop.Core

This is the core functionality for the FruitShop application. It includes the models, interfaces, and services related to the core business logic.

### Features

- Models for representing fruits, orders, and order items.
- Interfaces for defining the contracts for pricing, store, and order services.
- Services for implementing the pricing, store, and order functionality.

### Architecture

Logic is implemented using dependency injection and interfaces.

The key points:
- Fruits are decoupled from pricing and order services.
- To represent fruits with varing pricing strategies, the Strategy Pattern is used. To change a pricing model of a fruit, a storage needs to be updated. A model 'Fruit' represented in the systtem is designed to be aggregated model. Database models can be different and 1 to many relations binding can be used to link a fruit to multiple pricing strategies.
- Pricing and store services are decoupled from order service.
- Validation is skipped for simplicity.
    
### Key Services
- OrderService: Manages the placement of orders and the calculation of order totals.
- StoreService: Manages the retrieval of fruits from the store inventory.
- PricingService: Manages the calculation of order totals based on pricing strategies.
- PricingStrategy: Represents a pricing strategy for calculating order totals.

### Dependencies

- Microsoft.Extensions.DependencyInjection.Abstractions

### Usage

To place an order, create an instance of the OrderService object.

```csharp
var orderService = new OrderService(storeService, pricingService);
var orderRequest = new OrderRequest
{
    Items = new List<OrderRequestItem>
    {
        new OrderRequestItem
        {
            Name = "Apple",
            Quantity = 5000 //5kg
        },
        new OrderRequestItem
        {
            Name = "Avocado",
            Quantity = 3 //3 items
        }
    }
};

var order = await orderService.PlaceOrder(orderRequest);
```

## FruitShop.Core.UnitTests

The FruitShop.Core.UnitTests project is a unit test project that focuses on testing the logic of the FruitShop.Core project. It is used to verify that the individual components of the core functionality are working as expected.

This project contains tests that cover the following aspects:

- Testing the OrderService class: This includes testing the PlaceOrder method, which is responsible for placing an order. The tests may verify that the order is correctly created, that the pricing strategies are applied correctly, and that the order total is calculated correctly.
- Testing the StoreService class: This includes testing the GetItem method, which retrieves a fruit from the store inventory. The tests may verify that the correct fruit is returned for a given name, and that the fruit is not found if the name does not exist in the inventory.
- Testing the pricing strategies: The project may contain tests that verify the behavior of the different pricing strategies, such as Weight, Amount, and Discount. These tests may include scenarios where the pricing strategy is applied to different amounts and quantities of fruits.
- Testing the models: The project may also include tests that verify the behavior of the models used in the application, such as Fruit, Order, and OrderRequest. These tests may include scenarios where the models are created, modified, and validated.

Overall, the FruitShop.Core.UnitTests project is essential for ensuring the correctness and reliability of the core functionality of the FruitShop application. It helps to catch bugs and regressions, and provides confidence that the application behaves as expected in different scenarios.

## FruitShop.Core.FunctionalTests

The FruitShop.Core.FunctionalTests project is a test project that focuses on testing the functional behavior of the FruitShop.Core project. It is used to verify that the core functionality of the application is working as expected.

This project contains a test that cover a complex order creating scenario using multiple pricing strategies.

Overall, the FruitShop.Core.FunctionalTests project is essential for ensuring the correctness and reliability of the core functionality of the FruitShop application. It helps to catch bugs and regressions, and provides confidence that the application behaves as expected in different scenarios.
