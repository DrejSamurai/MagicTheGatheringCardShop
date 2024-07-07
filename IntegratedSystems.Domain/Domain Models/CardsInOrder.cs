using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Domain.Domain_Models
{
    public class CardsInOrder : BaseEntity
    {
        public Guid CardId { get; set; }
        public Card? OrderedCard { get; set; }
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
        public int Quantity { get; set; }
    }
}
