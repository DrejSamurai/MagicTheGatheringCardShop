using IntegratedSystems.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Domain.ETL_Models
{
    public class UserPlaylist : BaseEntity
    {
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public MusicStoreUser? Owner { get; set; }
        public virtual ICollection<TrackInPlaylist>? TracksInPlaylist { get; set; }
        public bool isPurchased { get; set; } = false;
    }
}
