using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace MusicPlayerUI.UserControls
{
    public partial class AlbumsView : UserControl
    {
        public static ObservableCollection<Album> Albums { get; set; } = [];

        public AlbumsView()
        {
            InitializeComponent();
            Albums = new ObservableCollection<Album>();
            DataContext = this;
        }

        public static void addAlbum(MediaFile mediaFile)
        {
            Album album = new Album() 
            {
                AlbumName = mediaFile.Album,
                ArtistName = mediaFile.Artist,
                ReleaseYear = mediaFile.Year
            };
            if (!Albums.Contains(album, new AlbumComparer()))
            {
                Albums.Add(album);
            }
        }
    }
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
