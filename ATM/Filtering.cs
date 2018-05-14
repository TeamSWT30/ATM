using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;

namespace ATM
{
    public class Filtering : IFiltering
    {
        public event EventHandler<TracksFilteredEventArgs> TracksFiltered;
        public List<Track> FilteredTracks;
        private IAirspace _airspace;

        public Filtering(IAirspace airspace, ITransponderdataReader tdr)
        {
            FilteredTracks = new List<Track>();

            tdr.TracksChanged += FilterTrack;
        }


        public void FilterTrack(object o, TracksChangedEventArgs args)
        {
            foreach (var track in args.Tracks)
            {
                if (_airspace.IsTrackInAirspace(track))
                {
                    FilteredTracks.Add(track);
                }
            }

            if (FilteredTracks.Count != 0)
            {
                var handler = TracksFiltered;
                handler?.Invoke(this, new TracksFilteredEventArgs(FilteredTracks));
            }
        }
    }
}
