using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository.Interface;
using IntegratedSystems.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IUserRepository userRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public List<Order> GetAllOrders()
        {
            return orderRepository.GetAllOrders();
        }

        public List<Order> GetAllUserOrders(string id)
        {
            //var user = userRepository.Get(id);
            return GetAllOrders().Where(z => z.OwnerId == id).ToList();
        }

        public Order GetDetailsForOrder(BaseEntity id)
        {
            return orderRepository.GetDetailsForOrder(id);
        }
    }
}
