using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Playlist
{
    public int PlaylistId { get; set; }

    public string PlaylistName { get; set; } = null!;

    public virtual ICollection<MediaEntity> MediaEntities { get; set; } = new List<MediaEntity>();
}
