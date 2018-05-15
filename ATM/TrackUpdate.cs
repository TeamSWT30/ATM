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
        private ICalcVelocityCourse _calc;


        public TrackUpdate(IFiltering filtering, ICalcVelocityCourse calcVelocityCourse)
        {
            UpdatedTracks = new List<Track>();
            _calc = calcVelocityCourse;

            filtering.TracksFiltered += UpdateTrack;
        }

        private void UpdateTrack(object o, TracksFilteredEventArgs args)
        {
            if (args.FilteredTracks.Count != 0 && UpdatedTracks.Count == 0)
            {
                foreach (var track in args.FilteredTracks)
                {
                    UpdatedTracks.Add(track);
                }
            }

            else if (args.FilteredTracks.Count != 0 && UpdatedTracks.Count != 0)
            {
                foreach (var newTrack in args.FilteredTracks)
                {
                    var oldTrack = UpdatedTracks.Find(i => i.Tag == newTrack.Tag);
                    if (oldTrack == null)
                    {
                        UpdatedTracks.Add(newTrack);
                    }

                    else
                    {
                        newTrack.Course = _calc.CalculateCourse(UpdatedTracks[UpdatedTracks.IndexOf(oldTrack)], newTrack);
                        newTrack.Velocity = _calc.CalculateVelocity(UpdatedTracks[UpdatedTracks.IndexOf(oldTrack)], newTrack);
                        UpdatedTracks[UpdatedTracks.IndexOf(oldTrack)] = newTrack;
                    }
                }
            }
            Console.Clear();
            var handler = TracksUpdated;
            handler?.Invoke(this, new TracksUpdatedEventArgs(UpdatedTracks));
        }
    }
}
