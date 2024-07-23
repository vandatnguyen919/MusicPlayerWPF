using Repositories.Entities;
using Services;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace MusicPlayerUI.UserControls.Playlists
{
    /// <summary>
    /// Interaction logic for EditPlaylistWindow.xaml
    /// </summary>
    public partial class EditPlaylistWindow : Window
    {
        public Playlist Playlist { get; set; }

        private PlaylistService _playlistService = new();

        public EditPlaylistWindow()
        {
            InitializeComponent();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            string playlistName = playlistNameTextBox.Text;
            if (!string.IsNullOrWhiteSpace(playlistName))
            {
                DialogResult = true;
                Playlist editedPlaylist = _playlistService.GetPlaylist(Playlist.PlaylistId);
                editedPlaylist.PlaylistName = playlistName;
                _playlistService.UpdatePlaylist(editedPlaylist);
                Playlist = editedPlaylist;
                Close();
            }
            else
                MessageBox.Show("Playlist name cannot be empty", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            playlistNameTextBox.Text = Playlist.PlaylistName;
        }
    }
}
