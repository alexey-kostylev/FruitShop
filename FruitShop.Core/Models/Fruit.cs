using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitShop.Core.Models;
public record Fruit(string Name, decimal BasePrice, PricingMode PricingMode);
