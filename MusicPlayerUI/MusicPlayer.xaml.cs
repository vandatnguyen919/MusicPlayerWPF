using MusicPlayerUI.UserControls;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using TagLib;

namespace MusicPlayerUI
{
    public partial class MusicPlayer : Window
    {
        public static MediaElement MediaElement { set;  get; }
        public static DispatcherTimer Timer { set; get; }
        public static TextBlock TotalTimeTextBlock { set; get; }

        private UserControl homeView;
        private UserControl albumsView;
        private UserControl artistsView;
        public MusicPlayer()
        {
            InitializeComponent();
            
            MediaElement = mediaElement;

            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += Timer_Tick;

            TotalTimeTextBlock = totalTimeTextBlock;

            // Initialize views
            homeView = new HomeView();
            albumsView = new AlbumsView();
            artistsView = new ArtistsView();

            // Set the initial view
            MainContentControl.Content = homeView;
        }

        private void ShowHomeView(object sender, RoutedEventArgs e)
        {
            MainContentControl.Content = homeView;
        }

        private void ShowAlbumsView(object sender, RoutedEventArgs e)
        {
            MainContentControl.Content = albumsView;
        }

        private void ShowArtistsView(object sender, RoutedEventArgs e)
        {
            MainContentControl.Content = artistsView;
        }

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

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
            Timer.Stop();
            playbackSlider.Value = 0;
            currentTimeTextBlock.Text = "0:00:00";
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
        public static void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            if (MusicPlayer.MediaElement.NaturalDuration.HasTimeSpan)
            {
                MusicPlayer.TotalTimeTextBlock.Text = MusicPlayer.MediaElement.NaturalDuration.TimeSpan.ToString(@"h\:mm\:ss");
            }
        }
    }
}
