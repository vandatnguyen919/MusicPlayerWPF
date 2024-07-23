namespace MusicPlayerUI.UserControls.Albums
{
    public class Album
    {
        public string AlbumName { get; set; }
        public string ArtistName { get; set; }
        public int? ReleaseYear { get; set; }
    }
    public class AlbumComparer : IEqualityComparer<Album>
    {
        public bool Equals(Album x, Album y) => x.AlbumName == y.AlbumName && x.ArtistName == y.ArtistName && x.ReleaseYear == y.ReleaseYear;

        public int GetHashCode(Album obj) => (obj.AlbumName, obj.ArtistName, obj.ReleaseYear).GetHashCode();
    }
}
