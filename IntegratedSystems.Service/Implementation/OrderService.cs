using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository.Interface;
using IntegratedSystems.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public List<Order> GetAllOrders()
        {
            return orderRepository.GetAllOrders();
        }

        public Order GetDetailsForOrder(BaseEntity id)
        {
            return orderRepository.GetDetailsForOrder(id);
        }
    }
}
