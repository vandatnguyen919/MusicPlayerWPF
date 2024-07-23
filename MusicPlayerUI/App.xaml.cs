using MusicPlayerUI.Services;
using MusicPlayerUI.UserControls;
using MusicPlayerUI.UserControls.Playlists;
using Repositories.Entities;
using Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace MusicPlayerUI
{
    public partial class App : Application
    {
        private PlaylistService _playlistService = new();
        private MediaService _mediaService = new();
        public static ObservableCollection<Playlist> Playlists { get; set; } = [];
        public ICommand AddToPlaylistCommand { get; }
        public ICommand RemoveCommand { get; }

        public App()
        {
            Playlists = new(_playlistService.GetAllPlaylists());
            AddToPlaylistCommand = new RelayCommand(ExecuteAddToPlaylist);
            RemoveCommand = new RelayCommand(ExecuteRemove);
        }
        private void ExecuteAddToPlaylist(object parameter)
        {
            if (parameter is object[] parameters && parameters.Length == 2)
            {
                var mediaFile = parameters[0] as MediaDto;
                var playlist = parameters[1] as Playlist;

                if (mediaFile != null && playlist != null)
                {
                    var addedMediaFile = _playlistService.AddToPlaylist(playlist.PlaylistId, MediaMapper.MapToEntity(mediaFile));
                    if (addedMediaFile != null)
                        MessageBox.Show($"{addedMediaFile.TrackName} added to {playlist.PlaylistName}");
                    else
                        MessageBox.Show($"This file is already added in '{playlist.PlaylistName}'");
                }
            }
            else if (parameter is object[] newPlaylistParams && newPlaylistParams.Length == 1)
            {
                var mediaFile = newPlaylistParams[0] as MediaDto;

                var addPlaylistWindow = new AddPlaylistWindow();
                if (addPlaylistWindow.ShowDialog() == true)
                {
                    var newPlaylist = new Playlist { PlaylistName = addPlaylistWindow.PlaylistName };
                    var addedPlaylist = _playlistService.CreatePlaylist(newPlaylist);
                    PlaylistsView.Playlists.Add(addedPlaylist);
                    var addedMediaFile = _playlistService.AddToPlaylist(addedPlaylist.PlaylistId, MediaMapper.MapToEntity(mediaFile));
                    if (addedMediaFile != null)
                        MessageBox.Show($"New playlist '{newPlaylist.PlaylistName}' added with media file '{addedMediaFile?.TrackName}'");
                    else
                        MessageBox.Show($"This file is already added in '{newPlaylist.PlaylistName}'");
                }
            }
        }

        private void ExecuteRemove(object parameter)
        {
            if (parameter is MediaDto mediaFile)
            {
                var deletedMediaFile = _mediaService.DeleteMedia(MediaMapper.MapToEntity(mediaFile));
                var homeDeletedMedia = HomeView.HomeMediaFiles.FirstOrDefault(x => x.MediaId == deletedMediaFile.MediaId);
                var playlistSongDeleted = PlaylistSongsView.PlaylistMediaFiles.FirstOrDefault(x => x.MediaId == deletedMediaFile.MediaId);
                HomeView.HomeMediaFiles.Remove(homeDeletedMedia);
                PlaylistSongsView.PlaylistMediaFiles.Remove(playlistSongDeleted);
                // Remove logic here
                MessageBox.Show($"Remove clicked for {mediaFile.TrackName}");
            }
        }

        public static void RemoveGlobalPlaylist(Playlist playlist)
        {
            var deletedPlaylist = Playlists.FirstOrDefault(x => x.PlaylistId == playlist.PlaylistId);
            Playlists.Remove(deletedPlaylist);
        }

        public static void AddGlobalPlaylist(Playlist playlist)
        {
            Playlists.Add(playlist);
        }

        public static void UpdateGlobalPlaylist(Playlist playlist)
        {
            var deletedPlaylist = Playlists.FirstOrDefault(x => x.PlaylistId == playlist.PlaylistId);
            Playlists.Remove(deletedPlaylist);
            Playlists.Add(playlist);
        }
    }
}
