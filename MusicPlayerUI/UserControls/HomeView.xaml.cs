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
using System.Windows.Threading;

namespace MusicPlayerUI.UserControls
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {

        public static ObservableCollection<MediaFile> MediaFiles { get; set; }

        public HomeView()
        {
            InitializeComponent();

            MediaFiles = new ObservableCollection<MediaFile>();
            mediaDataGrid.ItemsSource = MediaFiles;
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
                    MediaFiles.Add(new MediaFile
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
                mediaDataGrid.ItemsSource = MediaFiles;
            }
            UpdateAlbumsAndArtists();
        }

        private void UpdateAlbumsAndArtists()
        {
            foreach (var mediaFile in MediaFiles)
            {
                AlbumsView.addAlbum(mediaFile);
                if (!ArtistsView.Artists.Contains(mediaFile.Artist)) ArtistsView.Artists.Add(mediaFile.Artist);
            }
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
    public class MediaFile
    {
        public string TrackName { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public int? Year { get; set; }
        public string Genre { get; set; }
        public TimeSpan Duration { get; set; }
        public string FilePath { get; set; }  // Add a property to store the file path

        public string FormattedDuration => Duration.ToString(@"h\:mm\:ss");
    }
}
