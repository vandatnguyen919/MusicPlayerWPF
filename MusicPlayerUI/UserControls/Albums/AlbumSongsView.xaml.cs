using Microsoft.IdentityModel.Tokens;
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

namespace MusicPlayerUI.UserControls.Albums
{
    /// <summary>
    /// Interaction logic for AlbumSongsView.xaml
    /// </summary>
    public partial class AlbumSongsView : UserControl
    {
        public ObservableCollection<MediaDto> AlbumMediaFiles { get; set; }

        public AlbumSongsView()
        {
            InitializeComponent();
        }

        private void MediaDataGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                MediaDto selectedFile = albumMediaDataGrid.SelectedItem as MediaDto;
                if (selectedFile != null && selectedFile.FilePath != null)
                {
                    MediaPlayer.MediaFiles = AlbumMediaFiles;
                    MediaPlayer.PlayMediaFile(selectedFile);
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            albumMediaDataGrid.ItemsSource = null;
            albumMediaDataGrid.ItemsSource = AlbumMediaFiles;
        }

        private void PlayAllButton_Click(object sender, RoutedEventArgs e)
        {
            if (!AlbumMediaFiles.IsNullOrEmpty() && AlbumMediaFiles[0] != null)
            {
                MediaPlayer.MediaFiles = AlbumMediaFiles;
                MediaPlayer.PlayMediaFile(AlbumMediaFiles[0]);
            }
        }
    }
}
