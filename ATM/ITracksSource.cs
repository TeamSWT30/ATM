using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class TracksChangedEventArgs : EventArgs
    {
        public List<Track> Tracks { get; set; }
    }

    public interface ITracksSource
    {
        event EventHandler<TracksChangedEventArgs> TracksChanged;
    }
}
