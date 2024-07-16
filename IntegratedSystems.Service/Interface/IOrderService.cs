using IntegratedSystems.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Interface
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();
        List<Order> GetAllUserOrders(string id);
        Order GetDetailsForOrder(BaseEntity id);
    }
}
