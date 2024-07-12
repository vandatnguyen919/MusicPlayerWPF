using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerUI
{
    public class MediaFile : INotifyPropertyChanged
    {
        private bool isPlaying;

        public string TrackName { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public int? Year { get; set; }
        public string Genre { get; set; }
        public TimeSpan Duration { get; set; }
        public string FilePath { get; set; }

        public string FormattedDuration => Duration.ToString(@"h\:mm\:ss");

        public bool IsPlaying
        {
            get { return isPlaying; }
            set
            {
                if (isPlaying != value)
                {
                    isPlaying = value;
                    OnPropertyChanged(nameof(IsPlaying));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
