using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicPlayerUI.UserControls.Playlists
{
    /// <summary>
    /// Interaction logic for PlaylistsView.xaml
    /// </summary>
    public partial class PlaylistsView : UserControl
    {
        public ObservableCollection<Playlist> Playlists { get; set; }

        private PlaylistService _playlistService;

        public PlaylistsView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _playlistService = new();
            Playlists = PlaylistService.Playlists;
            DataContext = this;
            //Playlists = PlaylistService.Playlists;
            //playlistGrid.ItemsSource = Playlists;
        }

        private void AddNewPlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            var addPlaylistWindow = new AddPlaylistWindow();
            if (addPlaylistWindow.ShowDialog() == true)
            {
                var newPlaylist = new Playlist { PlaylistName = addPlaylistWindow.PlaylistName };
                _playlistService.AddNewPlaylist(newPlaylist);
                MessageBox.Show($"New playlist '{newPlaylist.PlaylistName}' added");
            }
        }
    }
}
