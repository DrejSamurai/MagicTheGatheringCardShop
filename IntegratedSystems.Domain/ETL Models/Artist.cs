using IntegratedSystems.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Domain.ETL_Models
{
    public class Artist : BaseEntity
    {
        public string ArtistName { get; set; }
        public string ArtistDescription { get; set; }
        public string ArtistImage { get; set; }
    }
}
