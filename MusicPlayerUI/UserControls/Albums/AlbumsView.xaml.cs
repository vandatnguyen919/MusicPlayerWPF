using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace MusicPlayerUI.UserControls.Albums
{
    public partial class AlbumsView : UserControl
    {
        public static ObservableCollection<Album> Albums { get; set; } = [];

        public AlbumsView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public static void addAlbum(MediaDto mediaFile)
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
}
