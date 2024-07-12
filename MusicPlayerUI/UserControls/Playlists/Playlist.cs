using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerUI
{
    public class Playlist
    {
        public string PlaylistName { get; set; }
        public HashSet<MediaFile> MediaFiles { get; set; } = [];
    }
}
