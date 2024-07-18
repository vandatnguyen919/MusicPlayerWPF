using MusicPlayerUI.UserControls.Albums;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace MusicPlayerUI.UserControls.Playlists
{
    public partial class PlaylistCard : UserControl
    {
        public static readonly DependencyProperty PlaylistNameProperty =
           DependencyProperty.Register("PlaylistName", typeof(string), typeof(PlaylistCard), new PropertyMetadata(string.Empty));

        public string PlaylistName
        {
            get { return (string)GetValue(PlaylistNameProperty); }
            set { SetValue(PlaylistNameProperty, value); }
        }

        public PlaylistCard()
        {
            InitializeComponent();
        }

        private void PlaylistCard_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Playlist playlist = PlaylistService.Playlists.Where(x => x.PlaylistName == this.PlaylistName).First();
            var playlistSongsView = new PlaylistSongsView
            {
                DataContext = new PlaylistDetails()
                {
                    PlaylistName = this.PlaylistName,
                    SongCount = playlist.MediaFiles.Count()
                }
            };
            playlistSongsView.songsDataGrid.ItemsSource = null;
            playlistSongsView.songsDataGrid.ItemsSource = playlist.MediaFiles;
            MainWindow.MainContentControl.Content = playlistSongsView;
        }
    }

    public class PlaylistDetails
    {
        public string PlaylistName { get; set; }
        public int SongCount { get; set; }
    }
}
