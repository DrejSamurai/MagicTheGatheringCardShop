using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Domain.DTO
{
    public class AddToCartDTO
    {
        public Guid SelectedCardId { get; set; }
        public string? SelectedCardName { get; set; }
        public int Quantity { get; set; }
    }
}
