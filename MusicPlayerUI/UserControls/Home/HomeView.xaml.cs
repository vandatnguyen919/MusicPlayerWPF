using MusicPlayerUI.Services;
using MusicPlayerUI.UserControls.Albums;
using Repositories.Entities;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MusicPlayerUI.UserControls
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {

        public static ObservableCollection<MediaDto> HomeMediaFiles { get; set; } = [];

        private MediaService _mediaService = new();

        public HomeView()
        {
            InitializeComponent();
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
                    MediaEntity media = new()
                    {
                        TrackName = tfile.Tag.Title ?? System.IO.Path.GetFileNameWithoutExtension(fileName),
                        Artist = tfile.Tag.FirstPerformer ?? "Unknown Artist",
                        Album = tfile.Tag.Album ?? "Unknown Album",
                        Year = (int)tfile.Tag.Year == 0 ? null : (int)tfile.Tag.Year,
                        Genre = tfile.Tag.FirstGenre ?? "Unknown",
                        Duration = TimeOnly.FromTimeSpan(tfile.Properties.Duration),
                        FilePath = fileName
                    };
                    MediaEntity savedMedia = _mediaService.CreateMedia(media);
                    MediaDto mediaDto = MediaMapper.MapToDto(savedMedia);
                    HomeMediaFiles.Add(mediaDto);
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

        private void MediaDataGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                MediaDto selectedFile = mediaDataGrid.SelectedItem as MediaDto;
                if (selectedFile != null && selectedFile.FilePath != null)
                {
                    MediaPlayer.MediaFiles = HomeMediaFiles;
                    MediaPlayer.PlayMediaFile(selectedFile);
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            List<MediaDto> mediaDtos = _mediaService.GetAllMedium().Select(m => MediaMapper.MapToDto(m)).ToList();
            HomeMediaFiles = new(mediaDtos);
            mediaDataGrid.ItemsSource = HomeMediaFiles;
            UpdateAlbumsAndArtists();
        }
    }
}
