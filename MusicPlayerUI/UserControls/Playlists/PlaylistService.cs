using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerUI.UserControls.Playlists
{
    class PlaylistService
    {
        public static ObservableCollection<Playlist> Playlists { get; set; } = [];

        public ObservableCollection<Playlist> GetAllPlaylists()
        {
            return Playlists;
        }

        public Playlist AddNewPlaylist(Playlist playlist)
        {
            Playlists.Add(playlist);
            return playlist;
        }

        public MediaFile AddToPlaylist(Playlist playlist, MediaFile mediaFile)
        {
            playlist.MediaFiles.Add(mediaFile);
            return mediaFile;
        }
    }
}
