
using FruitShop.Core.Models;

namespace FruitShop.Core;
public interface IStoreService
{
    Task<Fruit?> GetItem(string name);
}