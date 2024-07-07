using IntegratedSystems.Domain.Identity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Domain.Domain_Models
{
    public class Order : BaseEntity
    {
        public string? OwnerId { get; set; }
        public IntegratedSystemsUser? Owner { get; set; }
        public ICollection<CardsInOrder>? CardsInOrders { get; set; }
    }
}
