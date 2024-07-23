using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class MediaEntity
{
    public int MediaId { get; set; }

    public string TrackName { get; set; } = null!;

    public string? Artist { get; set; }

    public string? Album { get; set; }

    public int? Year { get; set; }

    public string? Genre { get; set; }

    public TimeOnly Duration { get; set; }

    public string FilePath { get; set; } = null!;

    public virtual ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
}
