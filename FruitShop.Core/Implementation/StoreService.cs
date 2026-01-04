using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FruitShop.Core.Models;

namespace FruitShop.Core.Implementation;

/// <summary>
/// Store service representing a fruit store inventory
/// </summary>
/// <param name="fruits"></param>
internal class StoreService(IReadOnlyCollection<Fruit> fruits) : IStoreService
{
    private readonly Dictionary<string, Fruit> _fruits = fruits.ToDictionary(f => f.Name, StringComparer.OrdinalIgnoreCase);

    public Task<Fruit?> GetItem(string name)
    {
        return !_fruits.TryGetValue(name, out var fruit)
            ? Task.FromResult<Fruit?>(null)
            : Task.FromResult<Fruit?>(fruit);
    }
}
