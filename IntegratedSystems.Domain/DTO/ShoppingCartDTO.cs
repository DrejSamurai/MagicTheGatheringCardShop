using IntegratedSystems.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Domain.DTO
{
    public class ShoppingCartDTO
    {
        public List<CardsInShoppingCart>? AllCards { get; set; }

        public double TotalPrice { get; set; }
    }
}
