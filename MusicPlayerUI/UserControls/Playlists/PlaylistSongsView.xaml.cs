using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using MusicPlayerUI.Services;
using Repositories.Entities;
using Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MusicPlayerUI.UserControls.Playlists
{
    /// <summary>
    /// Interaction logic for PlaylistSongsView.xaml
    /// </summary>
    public partial class PlaylistSongsView : UserControl
    {
        private PlaylistService _playlistService = new();

        public Playlist Playlist { get; set; } = null;

        public static ObservableCollection<MediaDto> PlaylistMediaFiles { get; set; } = [];

        public PlaylistSongsView()
        {
            InitializeComponent();
        }

        private void MediaDataGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                MediaDto selectedFile = playlistMediaDataGrid.SelectedItem as MediaDto;
                if (selectedFile != null && selectedFile.FilePath != null)
                {
                    MediaPlayer.MediaFiles = PlaylistMediaFiles;
                    MediaPlayer.PlayMediaFile(selectedFile);
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            playlistMediaDataGrid.ItemsSource = null;
            playlistMediaDataGrid.ItemsSource = PlaylistMediaFiles;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Are you sure want to delete this playlist?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (Playlist != null && res == MessageBoxResult.Yes)
                _playlistService.DeletePlaylist(Playlist);
            App.RemoveGlobalPlaylist(Playlist);
            MainWindow.PlaylistsView = new PlaylistsView();
            MainWindow.MainContentControl.Content = MainWindow.PlaylistsView;
        }

        private void RenameButton_Click(object sender, RoutedEventArgs e)
        {
            EditPlaylistWindow editPlaylistWindow = new();
            editPlaylistWindow.Playlist = Playlist;
            if (editPlaylistWindow.ShowDialog() == true)
            {
                Playlist playlist = PlaylistsView.Playlists.Where(x => x.PlaylistName == Playlist.PlaylistName).First();
                var playlistSongsView = new PlaylistSongsView
                {
                    DataContext = new PlaylistDetails()
                    {
                        PlaylistName = editPlaylistWindow.Playlist.PlaylistName,
                        SongCount = playlist.MediaEntities.Count()
                    }
                };
                playlistSongsView.Playlist = playlist;
                PlaylistMediaFiles = new(playlist.MediaEntities.Select(MediaMapper.MapToDto));
                App.UpdateGlobalPlaylist(editPlaylistWindow.Playlist);
                MainWindow.MainContentControl.Content = playlistSongsView;
            }

        }

        private void PlayAllButton_Click(object sender, RoutedEventArgs e)
        {
            if (!PlaylistMediaFiles.IsNullOrEmpty() && PlaylistMediaFiles[0] != null)
            {
                MediaPlayer.MediaFiles = PlaylistMediaFiles;
                MediaPlayer.PlayMediaFile(PlaylistMediaFiles[0]);
            }
        }
    }
}
