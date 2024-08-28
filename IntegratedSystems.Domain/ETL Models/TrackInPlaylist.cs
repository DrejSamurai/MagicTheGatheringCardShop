using IntegratedSystems.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Domain.ETL_Models
{
    public class TrackInPlaylist : BaseEntity
    {
        public Guid TrackId { get; set; }
        public Track Track { get; set; }
        public Guid UserPlaylistId { get; set; }
        public UserPlaylist UserPlaylist { get; set; }
    }
}
