using IntegratedSystems.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Domain.ETL_Models
{
    public class Track : BaseEntity
    {
        public string TrackName { get; set; }
        public int Rating { get; set; }
        public Guid AlbumId { get; set; }
        public Album? Album { get; set; }
    }
}
