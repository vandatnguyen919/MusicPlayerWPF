using MusicPlayerUI.UserControls.Albums;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TagLib;

namespace MusicPlayerUI.UserControls
{
    public partial class AlbumCard : UserControl
    {
        public static readonly DependencyProperty AlbumNameProperty =
            DependencyProperty.Register("AlbumName", typeof(string), typeof(AlbumCard), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ArtistNameProperty =
            DependencyProperty.Register("ArtistName", typeof(string), typeof(AlbumCard), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ReleaseYearProperty =
            DependencyProperty.Register("ReleaseYear", typeof(string), typeof(AlbumCard), new PropertyMetadata(string.Empty));

        public string AlbumName
        {
            get { return (string)GetValue(AlbumNameProperty); }
            set { SetValue(AlbumNameProperty, value); }
        }

        public string ArtistName
        {
            get { return (string)GetValue(ArtistNameProperty); }
            set { SetValue(ArtistNameProperty, value); }
        }

        public string ReleaseYear
        {
            get { return (string)GetValue(ReleaseYearProperty); }
            set { SetValue(ReleaseYearProperty, value); }
        }

        public AlbumCard()
        {
            InitializeComponent();
        }

        private void AlbumCardButton_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<MediaFile> mediaFiles = new ObservableCollection<MediaFile>(HomeView.HomeMediaFiles.Where(m => m.Album == AlbumName));
            TimeSpan totalDuration = mediaFiles.Aggregate(TimeSpan.Zero, (sum, file) => sum + file.Duration);
            var albumSongsView = new AlbumSongsView
            {
                DataContext = new AlbumDetails
                {
                    AlbumName = this.AlbumName,
                    ArtistName = this.ArtistName,
                    ReleaseYear = this.ReleaseYear,
                    Genre = mediaFiles[0].Genre,
                    SongCount = mediaFiles.Count(), // Replace with actual song count
                    TotalDuration = totalDuration.ToString(@"h\:mm\:ss") // Replace with actual total duration
                }
            };
            albumSongsView.songsDataGrid.ItemsSource = mediaFiles;
            MainWindow.MainContentControl.Content = albumSongsView;
        }
    }
    public class AlbumDetails
    {
        public string AlbumName { get; set; }
        public string ArtistName { get; set; }
        public string ReleaseYear { get; set; }
        public string Genre { get; set; }
        public int SongCount { get; set; }
        public string TotalDuration { get; set; }
    }
}
