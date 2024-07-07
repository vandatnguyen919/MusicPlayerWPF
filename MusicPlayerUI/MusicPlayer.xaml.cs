﻿using MusicPlayerUI.UserControls;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace MusicPlayerUI
{
    public partial class MusicPlayer : Window
    {
        public static MediaElement MediaElement { get; set; }
        public static Slider PlaybackSlider { get; set; }
        public static TextBlock CurrentTimeTextBlock { get; set; }
        public static ObservableCollection<MediaFile> MediaFiles { get; set; } = [];
        public static MediaFile CurrentMediaFile { get; set; }
        public static DispatcherTimer Timer { get; set; }
        public static TextBlock TotalTimeTextBlock { get; set; }
        public static Button PlayPauseButton { get; set; }

        public static ContentControl MainContentControl { get; set; }
        private UserControl homeView;
        private UserControl albumsView;
        private UserControl artistsView;

        public static bool IsPaused { get; set; } = false;
        public static bool IsShuffleEnabled { get; set; } = false;

        // Volume properties
        private bool isMuted = false;
        private double previousVolume;

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

            PlayPauseButton = playPauseButton;

            // Initialize the volume to 100%
            MediaElement.Volume = 1.0;
            volumeSlider.Value = 1.0;

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
        private void ShuffleButton_Click(object sender, RoutedEventArgs e)
        {
            IsShuffleEnabled = !IsShuffleEnabled;
            shuffleButton.Content = IsShuffleEnabled ? "Shuffle: On" : "Shuffle: Off";
        }

        private void PlayBackButton_Click(object sender, RoutedEventArgs e)
        {
            PlayPreviousMediaFile();
        }

        private void PlayPause_Click(object sender, RoutedEventArgs e)
        {
            if (IsPaused)
            {
                mediaElement.Play();
                Timer.Start();
            }
            else
            {
                mediaElement.Pause();
                Timer.Stop();
            }
            IsPaused = !IsPaused;
            playPauseButton.Content = IsPaused ? "▶" : "❚❚";
        }

        private void PlayNextButton_Click(object sender, RoutedEventArgs e)
        {
            PlayNextMediaFile();
        }

        // Volume Slider Handler
        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mediaElement != null && volumeValueTextBlock != null)
            {
                mediaElement.Volume = volumeSlider.Value;
                volumeValueTextBlock.Text = (volumeSlider.Value * 100).ToString("0") + "%";
                if (mediaElement.Volume != 0)
                {
                    isMuted = false;
                    volumeIconTextBlock.Text = "🔊";
                }
                else
                {
                    isMuted = true;
                    volumeIconTextBlock.Text = "🔇";
                }
            }
        }

        private void VolumeIcon_Click(object sender, MouseButtonEventArgs e)
        {
            if (MediaElement != null)
            {
                if (isMuted)
                {
                    MediaElement.Volume = previousVolume;
                    isMuted = false;
                    ((TextBlock)sender).Text = "🔊";
                }
                else
                {
                    previousVolume = MediaElement.Volume;
                    MediaElement.Volume = 0;
                    isMuted = true;
                    ((TextBlock)sender).Text = "🔇";
                }
                volumeSlider.Value = MediaElement.Volume;
                volumeValueTextBlock.Text = (volumeSlider.Value * 100).ToString("0") + "%";
            }
        }

        // Playback Slider
        private void PlaybackSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mediaElement.NaturalDuration.HasTimeSpan)
            {
                mediaElement.Position = TimeSpan.FromSeconds(playbackSlider.Value);
            }
        }

        // Media Timer
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

        private void PlayPreviousMediaFile()
        {
            int currentIndex = MediaFiles.IndexOf(CurrentMediaFile);
            if (currentIndex > 0)
            {
                var previousMediaFile = MediaFiles[currentIndex - 1];
                PlayMediaFile(previousMediaFile);
            }
        }

        private void PlayNextMediaFile()
        {
            if (IsShuffleEnabled)
            {
                Random random = new Random();
                int nextIndex = random.Next(MediaFiles.Count);
                var nextMediaFile = MediaFiles[nextIndex];
                PlayMediaFile(nextMediaFile);
            }
            else
            {
                int currentIndex = MediaFiles.IndexOf(CurrentMediaFile);
                if (currentIndex < MediaFiles.Count - 1)
                {
                    var nextMediaFile = MediaFiles[currentIndex + 1];
                    PlayMediaFile(nextMediaFile);
                }
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

            IsPaused = false;
            PlayPauseButton.Content = IsPaused ? "▶" : "❚❚";
            // Play the media file
            MediaElement.Play();
            Timer.Start();
        }
    }
}
