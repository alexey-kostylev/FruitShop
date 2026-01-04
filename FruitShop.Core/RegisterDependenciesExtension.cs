using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FruitShop.Core.Implementation;

namespace FruitShop.Core;
public static class RegisterDependenciesExtension
{
    public static IServiceCollection RegisterDependencies(this IServiceCollection services){
        return services
            .AddSingleton<IPricingService, PricingService>()
            .AddScoped<IStoreService, StoreService>()
            .AddScoped<IOrderService, OrderService>()
            .AddTransient<PricingStrategyAmount>()
            .AddTransient<PricingStrategyWeight>()
            .AddTransient<PricingStrategyDiscount>()
            ;
    }
}
