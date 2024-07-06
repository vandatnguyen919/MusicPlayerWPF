using MusicPlayerUI.UserControls;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MusicPlayerUI
{
    public partial class MusicPlayer : Window
    {
        public static MediaElement MediaElement { get; set; }
        public static Slider PlaybackSlider { get; set; }
        public static TextBlock CurrentTimeTextBlock { get; set; }
        public static ObservableCollection<MediaFile> MediaFiles { get; set; }
        public static MediaFile CurrentMediaFile { get; set; }
        public static DispatcherTimer Timer { get; set; }
        public static TextBlock TotalTimeTextBlock { get; set; }

        public static ContentControl MainContentControl { get; set; }
        private UserControl homeView;
        private UserControl albumsView;
        private UserControl artistsView;

        public MusicPlayer()
        {
            InitializeComponent();

            MediaElement = mediaElement;
            MediaElement.MediaOpened += MediaElement_MediaOpened;
            MediaElement.MediaEnded += MediaElement_MediaEnded;
            MediaElement.LoadedBehavior = MediaState.Manual;
            MediaElement.UnloadedBehavior = MediaState.Stop;

            PlaybackSlider = playbackSlider;
            CurrentTimeTextBlock = currentTimeTextBlock;

            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(1);
            Timer.Tick += Timer_Tick;

            TotalTimeTextBlock = totalTimeTextBlock;

            MainContentControl = mainContentControl;

            // Initialize views
            homeView = new HomeView();
            albumsView = new AlbumsView();
            artistsView = new ArtistsView();

            // Set the initial view
            mainContentControl.Content = homeView;
        }

        private void ShowHomeView(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = homeView;
        }

        private void ShowAlbumsView(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = albumsView;
        }

        private void ShowArtistsView(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = artistsView;
        }

        // Media Controller
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Play();
            Timer.Start();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
            Timer.Stop();
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mediaElement != null && volumeValueTextBlock != null)
            {
                mediaElement.Volume = volumeSlider.Value;
                volumeValueTextBlock.Text = (volumeSlider.Value * 100).ToString("0") + "%";
            }
        }

        private void PlaybackSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mediaElement.NaturalDuration.HasTimeSpan)
            {
                mediaElement.Position = TimeSpan.FromSeconds(playbackSlider.Value);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (mediaElement.NaturalDuration.HasTimeSpan)
            {
                playbackSlider.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
                playbackSlider.Value = mediaElement.Position.TotalSeconds;
                currentTimeTextBlock.Text = mediaElement.Position.ToString(@"h\:mm\:ss");
            }
        }

        private void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            if (MediaElement.NaturalDuration.HasTimeSpan)
            {
                TotalTimeTextBlock.Text = MediaElement.NaturalDuration.TimeSpan.ToString(@"h\:mm\:ss");
            }
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            PlayNextMediaFile();
        }

        private void PlayNextMediaFile()
        {
            int currentIndex = MediaFiles.IndexOf(CurrentMediaFile);
            if (currentIndex < MediaFiles.Count - 1)
            {
                var nextMediaFile = MediaFiles[currentIndex + 1];
                PlayMediaFile(nextMediaFile);
            }
        }

        public static void PlayMediaFile(MediaFile mediaFile)
        {
            if (CurrentMediaFile != null)
            {
                CurrentMediaFile.IsPlaying = false; // Unset the previous file
            }

            CurrentMediaFile = mediaFile;
            CurrentMediaFile.IsPlaying = true; // Set the current file as playing

            MediaElement.Source = new Uri(mediaFile.FilePath);

            // Reset the media file from the beginning
            MediaElement.Stop();
            PlaybackSlider.Value = 0;
            CurrentTimeTextBlock.Text = "0:00:00";

            // Play the media file
            MediaElement.Play();
            Timer.Start();
        }

    }
}
