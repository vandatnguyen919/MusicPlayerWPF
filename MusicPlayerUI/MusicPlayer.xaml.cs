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
        //public ObservableCollection<MediaFile> MediaFiles { get; set; }
        private DispatcherTimer timer;

        public MusicPlayer()
        {
            InitializeComponent();

            //MediaFiles = new ObservableCollection<MediaFile>();
            //mediaDataGrid.ItemsSource = MediaFiles;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
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
                List<MediaFile> mediaFiles = new List<MediaFile>();
                foreach (var fileName in openFileDialog.FileNames)
                {
                    var tfile = TagLib.File.Create(fileName);
                    mediaFiles.Add(new MediaFile
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
                mediaDataGrid.ItemsSource = mediaFiles;
            }
        }

        private void MediaDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MediaFile selectedFile = mediaDataGrid.SelectedItem as MediaFile;
            if (selectedFile != null && selectedFile.FilePath != null)
            {
                mediaElement.Source = new Uri(selectedFile.FilePath);
                mediaElement.LoadedBehavior = MediaState.Manual;
                mediaElement.UnloadedBehavior = MediaState.Stop;
                mediaElement.MediaOpened += MediaElement_MediaOpened;
                mediaElement.Play();
                timer.Start();
            }
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Play();
            timer.Start();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
            timer.Stop();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
            timer.Stop();
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

        private void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            if (mediaElement.NaturalDuration.HasTimeSpan)
            {
                totalTimeTextBlock.Text = mediaElement.NaturalDuration.TimeSpan.ToString(@"h\:mm\:ss");
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
