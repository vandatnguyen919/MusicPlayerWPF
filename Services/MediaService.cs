using Repositories;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerUI.Services
{
    public class MediaService
    {
        private MediaRepository _mediaRepo = new();

        public List<MediaEntity> GetAllMedium()
        {
            return _mediaRepo.GetAll();
        }

        public MediaEntity CreateMedia(MediaEntity media)
        {
            return _mediaRepo.Create(media);
        }

        public MediaEntity UpdateMedia(MediaEntity media)
        {
            return _mediaRepo.Update(media);
        }

        public MediaEntity DeleteMedia(MediaEntity media)
        {
            
            return _mediaRepo.Delete(media);
        }
    }
}
