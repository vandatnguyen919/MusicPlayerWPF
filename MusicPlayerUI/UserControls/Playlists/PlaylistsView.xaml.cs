using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using MusicPlayerUI.Services;
using Repositories.Entities;
using Services;

namespace MusicPlayerUI.UserControls.Playlists
{
    /// <summary>
    /// Interaction logic for PlaylistsView.xaml
    /// </summary>
    public partial class PlaylistsView : UserControl
    {
        public static ObservableCollection<Playlist> Playlists { get; set; } = [];

        private PlaylistService _playlistService = new();

        public PlaylistsView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Playlists = new(_playlistService.GetAllPlaylists());
            DataContext = this;
        }

        private void AddNewPlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            var addPlaylistWindow = new AddPlaylistWindow();
            if (addPlaylistWindow.ShowDialog() == true)
            {
                var newPlaylist = new Playlist { PlaylistName = addPlaylistWindow.PlaylistName };
                Playlist addedPlaylist = _playlistService.CreatePlaylist(newPlaylist);
                App.AddGlobalPlaylist(addedPlaylist);
                Playlists.Add(addedPlaylist);
                MessageBox.Show($"New playlist '{addedPlaylist.PlaylistName}' added");
            }
        }
    }
}
