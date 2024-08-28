using IntegratedSystems.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Domain.ETL_Models
{
    public class Album : BaseEntity
    {
        public string AlbumName { get; set; }
        public string AlbumImage { get; set; }
        public Guid ArtistId { get; set; }
        public Artist? Artist { get; set; }
        public string Genre { get; set; }
        public int Rating { get; set; }
        public ICollection<Track>? Tracks { get; set; }
    }
}
