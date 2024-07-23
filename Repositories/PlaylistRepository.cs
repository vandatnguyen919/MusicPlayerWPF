using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PlaylistRepository
    {
        private MusicPlayerPrn212assignmentContext _context;

        public List<Playlist> GetAll()
        {
            _context = new();
            return _context.Playlists.Include("MediaEntities").ToList();
        }

        public Playlist GetOne(int playlistId)
        {
            _context = new();
            return _context.Playlists.FirstOrDefault(x => x.PlaylistId == playlistId);
        }

        public Playlist Create(Playlist playlist)
        {
            _context = new();
            Playlist addedPlaylist = _context.Playlists.Add(playlist).Entity;
            _context.SaveChanges();
            return addedPlaylist;
        }

        public Playlist Update(Playlist playlist)
        {
            _context = new();
            Playlist updatedPlaylist = _context.Playlists.Update(playlist).Entity;
            _context.SaveChanges();
            return updatedPlaylist;
        }

        public Playlist Delete(Playlist playlist)
        {
            _context = new();
            Playlist deletedPlaylist = _context.Playlists.Remove(playlist).Entity;
            _context.SaveChanges();
            return deletedPlaylist;
        }
    }
}
