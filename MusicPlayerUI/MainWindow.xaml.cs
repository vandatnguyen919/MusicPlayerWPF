using MusicPlayerUI.UserControls;
using MusicPlayerUI.UserControls.Albums;
using MusicPlayerUI.UserControls.Playlists;
using System.Windows;
using System.Windows.Controls;
namespace MusicPlayerUI
{
    public partial class MainWindow : Window
    {
        public static ContentControl MainContentControl { get; set; }
        private UserControl homeView;
        private UserControl albumsView;
        private UserControl artistsView;
        public static UserControl PlaylistsView {  get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            MainContentControl = mainContentControl;
            // Initialize views
            homeView = new HomeView();
            albumsView = new AlbumsView();
            artistsView = new ArtistsView();
            PlaylistsView = new PlaylistsView();

            // Set the initial view
            mainContentControl.Content = homeView;
        }

        private void ShowHomeView(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = homeView;
        }

        private void ShowAlbumsView(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = albumsView;
        }

        private void ShowArtistsView(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = artistsView;
        }

        private void ShowPlaylistsView(object sender, RoutedEventArgs e)
        {
            PlaylistsView = new PlaylistsView();
            mainContentControl.Content = PlaylistsView;
        }
    }
}
