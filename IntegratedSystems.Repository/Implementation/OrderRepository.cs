using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }


        public List<Order> GetAllOrders()
        {
            return entities
                 .Include(z => z.CardsInOrders)
                 .Include(z => z.Owner)
                 .Include("CardsInOrders.OrderedCard")
                 .Include("CardsInOrders.OrderedCard.Expansion")
                 .ToList();
        }

        public Order GetDetailsForOrder(BaseEntity id)
        {
            return entities
                .Include(z => z.CardsInOrders)
                .Include(z => z.Owner)
                .Include("CardsInOrders.OrderedCard")
                .Include("CardsInOrders.OrderedCard.Expansion")
                .SingleOrDefaultAsync(z => z.Id == id.Id).Result;
        }

    }
}
