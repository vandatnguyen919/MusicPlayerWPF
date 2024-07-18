using MusicPlayerUI.UserControls.Playlists;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace MusicPlayerUI
{
    public partial class App : Application
    {
        private PlaylistService _playlistService = new();
        public ObservableCollection<Playlist> Playlists { get; set; }
        public ICommand PlayCommand { get; }
        public ICommand AddToPlaylistCommand { get; }
        public ICommand RemoveCommand { get; }

        public App()
        {
            Playlists = PlaylistService.Playlists;
            PlayCommand = new RelayCommand(ExecutePlay);
            AddToPlaylistCommand = new RelayCommand(ExecuteAddToPlaylist);
            RemoveCommand = new RelayCommand(ExecuteRemove);
        }

        private void ExecutePlay(object parameter)
        {
            if (parameter is MediaFile mediaFile)
            {
                // Play logic here
                MessageBox.Show($"Play clicked for {mediaFile.TrackName}");
            }
        }

        private void ExecuteAddToPlaylist(object parameter)
        {
            if (parameter is object[] parameters && parameters.Length == 2)
            {
                var mediaFile = parameters[0] as MediaFile;
                var playlist = parameters[1] as Playlist;

                if (mediaFile != null && playlist != null)
                {
                    _playlistService.AddToPlaylist(playlist, mediaFile);
                    MessageBox.Show($"{mediaFile.TrackName} added to {playlist.PlaylistName}");
                }
            }
            else if (parameter is object[] newPlaylistParams && newPlaylistParams.Length == 1)
            {
                var mediaFile = newPlaylistParams[0] as MediaFile;

                var addPlaylistWindow = new AddPlaylistWindow();
                if (addPlaylistWindow.ShowDialog() == true)
                {
                    var newPlaylist = new Playlist { PlaylistName = addPlaylistWindow.PlaylistName };
                    _playlistService.AddNewPlaylist(newPlaylist);
                    _playlistService.AddToPlaylist(newPlaylist, mediaFile);
                    MessageBox.Show($"New playlist '{newPlaylist.PlaylistName}' added with media file '{mediaFile?.TrackName}'");
                }
            }
        }

        private void ExecuteRemove(object parameter)
        {
            if (parameter is MediaFile mediaFile)
            {
                // Remove logic here
                MessageBox.Show($"Remove clicked for {mediaFile.TrackName}");
            }
        }
    }
}
