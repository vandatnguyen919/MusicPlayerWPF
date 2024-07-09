using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace MusicPlayerUI
{
    public partial class App : Application
    {
        public ICommand PlayCommand { get; }
        public ICommand AddToPlaylistCommand { get; }
        public ICommand RemoveCommand { get; }

        public ObservableCollection<string> Playlists { get; } = new ObservableCollection<string> { "Playlist 1", "Playlist 2", "Playlist 3" };

        public App()
        {
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
            if (parameter is MediaFile mediaFile)
            {
                // Add to Playlist logic here
                MessageBox.Show($"Add to Playlist clicked for {mediaFile.TrackName}");
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
