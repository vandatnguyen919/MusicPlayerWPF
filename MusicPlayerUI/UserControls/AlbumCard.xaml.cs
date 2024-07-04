using System.Windows;
using System.Windows.Controls;

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

        private void CardButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Album clicked: {AlbumName}");
        }
    }
}
