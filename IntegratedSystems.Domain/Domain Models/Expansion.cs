using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Domain.Domain_Models
{
    public class Expansion : BaseEntity
    {
        [Required]
        public string? ExpansionName { get; set; }

        [Required]
        public string? ExpansionDescription { get; set; }

        [Required]
        public string? ExpansionImage { get; set; }

        [Required]
        public double Rating { get; set; }

        [Required]
        public double CardNumber { get; set; }

        public virtual ICollection<Card>? Cards { get; set; }
    }
}
