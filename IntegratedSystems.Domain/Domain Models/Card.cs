using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Domain.Identity_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Domain.Domain_Models
{
    public class Card : BaseEntity
    {
        public Guid ExpansionId { get; set; }
        public Expansion? Expansion { get; set; }

        [Required]
        public string? CardName { get; set; }

        [Required]
        public string? CardDescription { get; set; }

        [Required]
        public string? CardImage { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public double Rating { get; set; }

        [Required]
        public string? Rarity { get; set; }

        public virtual IntegratedSystemsUser? CreatedBy { get; set; }
        public virtual ICollection<CardsInShoppingCart>? CardsInShoppingCart { get; set; }
        public ICollection<CardsInOrder>? CardsInOrders { get; set; }
    }
}
