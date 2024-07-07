using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCart AddCardToShoppingCart(string userId, AddToCartDTO model);
        AddToCartDTO getProductInfo(Guid Id);
        ShoppingCartDTO getShoppingCartDetails(string userId);
        Boolean deleteFromShoppingCart(string userId, Guid? Id);
        Boolean orderCardss(string userId);
    }
}
