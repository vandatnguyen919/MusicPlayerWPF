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
    /// Interaction logic for ArtistView.xaml
    /// </summary>
    public partial class ArtistsView : UserControl
    {
        public static ObservableCollection<string> Artists { get; set; }

        public ArtistsView()
        {
            InitializeComponent();
            Artists = new ObservableCollection<string>();
            artistsComboBox.ItemsSource = Artists;
        }

        private void ArtistsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (artistsComboBox.SelectedItem is string selectedArtist)
            {
                FilterMediaFilesByArtist(selectedArtist);
            }
        }

        public void FilterMediaFilesByArtist(string artist)
        {
            mediaDataGrid.ItemsSource = new ObservableCollection<MediaFile>(HomeView.HomeMediaFiles.Where(m => m.Artist == artist));
        }
        private void MediaDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MediaFile selectedFile = mediaDataGrid.SelectedItem as MediaFile;
            //if (selectedFile != null && selectedFile.FilePath != null)
            //{

            //    MusicPlayer.MediaElement.Source = new Uri(selectedFile.FilePath);
            //    MusicPlayer.MediaElement.LoadedBehavior = MediaState.Manual;
            //    MusicPlayer.MediaElement.UnloadedBehavior = MediaState.Stop;
            //    MusicPlayer.MediaElement.MediaOpened += MusicPlayer.MediaElement_MediaOpened;
            //    MusicPlayer.MediaElement.Play();
            //    MusicPlayer.Timer.Start();
            //}
        }
    }
}
