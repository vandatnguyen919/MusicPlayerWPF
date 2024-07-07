using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace MusicPlayerUI.UserControls
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {

        public static ObservableCollection<MediaFile> HomeMediaFiles { get; set; }

        public HomeView()
        {
            InitializeComponent();
            HomeMediaFiles = new ObservableCollection<MediaFile>();
        }

        private void OpenMediaFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Multiselect = true,
                Filter = "Media Files|*.mp3;*.wav;*.wma;*.m4a;*.mp4;*.avi"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (var fileName in openFileDialog.FileNames)
                {
                    var tfile = TagLib.File.Create(fileName);
                    HomeMediaFiles.Add(new MediaFile
                    {
                        TrackName = tfile.Tag.Title ?? System.IO.Path.GetFileNameWithoutExtension(fileName),
                        Artist = tfile.Tag.FirstPerformer ?? "Unknown Artist",
                        Album = tfile.Tag.Album ?? "Unknown Album",
                        Year = (int)tfile.Tag.Year == 0 ? null : (int)tfile.Tag.Year,
                        Genre = tfile.Tag.FirstGenre ?? "Unknown",
                        Duration = tfile.Properties.Duration,
                        FilePath = fileName
                    });
                }
                mediaDataGrid.ItemsSource = HomeMediaFiles;
            }
            UpdateAlbumsAndArtists();
        }

        private void UpdateAlbumsAndArtists()
        {
            foreach (var mediaFile in HomeMediaFiles)
            {
                AlbumsView.addAlbum(mediaFile);
                if (!ArtistsView.Artists.Contains(mediaFile.Artist)) ArtistsView.Artists.Add(mediaFile.Artist);
            }
        }

        private void MediaDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MusicPlayer.MediaFiles = HomeMediaFiles;
            MediaFile selectedFile = mediaDataGrid.SelectedItem as MediaFile;
            if (selectedFile != null && selectedFile.FilePath != null)
            {
                MusicPlayer.PlayMediaFile(selectedFile);
            }
        }
    }
}
