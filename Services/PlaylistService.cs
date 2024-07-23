using Repositories;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PlaylistService
    {
        private PlaylistRepository _playlistRepo = new();

        public List<Playlist> GetAllPlaylists()
        {
            return _playlistRepo.GetAll();
        }

        public Playlist GetPlaylist(int playlistId)
        {
            return _playlistRepo.GetOne(playlistId);
        }

        public Playlist CreatePlaylist(Playlist playlist)
        {
            return _playlistRepo.Create(playlist);
        }

        public Playlist UpdatePlaylist(Playlist playlist)
        {
            return _playlistRepo.Update(playlist);
        }

        public Playlist DeletePlaylist(Playlist playlist)
        {
            return _playlistRepo.Delete(playlist);
        }

        public MediaEntity AddToPlaylist(int playlistId, MediaEntity mediaEntity)
        {
            try
            {
                Playlist playlist = _playlistRepo.GetOne(playlistId);
                playlist.MediaEntities.Add(mediaEntity);
                _playlistRepo.Update(playlist);
                return mediaEntity;
            } catch (Exception x)
            {
                return null;
            }
        }
    }
}
