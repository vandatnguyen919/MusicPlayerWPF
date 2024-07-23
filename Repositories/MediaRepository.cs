using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class MediaRepository
    {
        private MusicPlayerPrn212assignmentContext _context;

        public List<MediaEntity> GetAll()
        {
            _context = new();
            return _context.MediaEntities.ToList();
        }

        public MediaEntity Create(MediaEntity media)
        {
            _context = new();
            MediaEntity addedMedia = _context.MediaEntities.Add(media).Entity;
            _context.SaveChanges();
            return addedMedia;
        }

        public MediaEntity Update(MediaEntity media)
        {
            _context = new();
            MediaEntity updatedMedia = _context.MediaEntities.Update(media).Entity;
            _context.SaveChanges();
            return updatedMedia;
        }

        public MediaEntity Delete(MediaEntity media)
        {
            _context = new();

            var deletedMedia = _context.MediaEntities
                .Include("Playlists")
                .FirstOrDefault(m => m.MediaId == media.MediaId);

            if (deletedMedia == null)
                return null;

            foreach (var playlist in media.Playlists.ToList())
                playlist.MediaEntities.Remove(media);

            _context.MediaEntities.Remove(deletedMedia);
            _context.SaveChanges();
            return deletedMedia;
        }
    }
}
