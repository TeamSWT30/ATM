using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;

namespace ATM
{
    public class TrackUpdate : ITrackUpdate
    {
        public event EventHandler<TracksUpdatedEventArgs> TracksUpdated;
        private List<Track> UpdatedTracks;
        private ICalcVelocityCourse _calcVelocityCourse;


        public TrackUpdate(IFiltering filtering, ICalcVelocityCourse calcVelocityCourse)
        {
            UpdatedTracks = new List<Track>();
            _calcVelocityCourse = calcVelocityCourse;

            filtering.TracksFiltered += UpdateTrack;
        }

        private void UpdateTrack(object o, TracksFilteredEventArgs args)
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
