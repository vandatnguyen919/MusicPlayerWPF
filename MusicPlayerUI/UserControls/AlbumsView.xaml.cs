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

namespace MusicPlayerUI.UserControls
{
    /// <summary>
    /// Interaction logic for AlbumView.xaml
    /// </summary>
    public partial class AlbumsView : UserControl
    {
        public static ObservableCollection<string> Albums { get; set; }

        public AlbumsView()
        {
            InitializeComponent();
            Albums = new ObservableCollection<string>();
            albumsComboBox.ItemsSource = Albums;
        }

        private void AlbumsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (albumsComboBox.SelectedItem is string selectedAlbum)
            {
                FilterMediaFilesByAlbum(selectedAlbum);
            }
        }

        public void FilterMediaFilesByAlbum(string album)
        {
            mediaDataGrid.ItemsSource = new ObservableCollection<MediaFile>(HomeView.MediaFiles.Where(m => m.Album == album));
        }
        private void MediaDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MediaFile selectedFile = mediaDataGrid.SelectedItem as MediaFile;
            if (selectedFile != null && selectedFile.FilePath != null)
            {

                MusicPlayer.MediaElement.Source = new Uri(selectedFile.FilePath);
                MusicPlayer.MediaElement.LoadedBehavior = MediaState.Manual;
                MusicPlayer.MediaElement.UnloadedBehavior = MediaState.Stop;
                MusicPlayer.MediaElement.MediaOpened += MusicPlayer.MediaElement_MediaOpened;
                MusicPlayer.MediaElement.Play();
                MusicPlayer.Timer.Start();
            }
        }
    }
}
