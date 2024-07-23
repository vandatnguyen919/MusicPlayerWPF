using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerUI.Services
{
    public class MediaMapper
    {
        public static MediaDto MapToDto(MediaEntity mediaEntity)
        {
            return new MediaDto
            {
                MediaId = mediaEntity.MediaId,
                TrackName = mediaEntity.TrackName,
                Artist = mediaEntity.Artist,
                Album = mediaEntity.Album,
                Year = mediaEntity.Year,
                Genre = mediaEntity.Genre,
                Duration = mediaEntity.Duration.ToTimeSpan(),
                FilePath = mediaEntity.FilePath
            };
        }

        public static MediaEntity MapToEntity(MediaDto mediaDto)
        {
            return new MediaEntity
            {
                MediaId = mediaDto.MediaId,
                TrackName = mediaDto.TrackName,
                Artist = mediaDto.Artist,
                Album = mediaDto.Album,
                Year = mediaDto.Year,
                Genre = mediaDto.Genre,
                Duration = TimeOnly.FromTimeSpan(mediaDto.Duration),
                FilePath = mediaDto.FilePath
            };
        }
    }
}
