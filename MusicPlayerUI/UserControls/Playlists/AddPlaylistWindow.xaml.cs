using System.Windows;

namespace MusicPlayerUI.UserControls.Playlists
{
    /// <summary>
    /// Interaction logic for AddPlaylistWindow.xaml
    /// </summary>
    public partial class AddPlaylistWindow : Window
    {
        public string PlaylistName { get; private set; }

        public AddPlaylistWindow()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            PlaylistName = playlistNameTextBox.Text;
            if (!string.IsNullOrWhiteSpace(PlaylistName))
            {
                DialogResult = true;
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
    }
}
