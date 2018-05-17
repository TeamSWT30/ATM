using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;

namespace ATM
{
    public class Updating : IUpdating
    {
        public event EventHandler<TracksUpdatedEventArgs> TracksUpdated;
        private List<Track> UpdatedTracks;
        private ICalculating _calc;


        public Updating(IFiltering filtering, ICalculating calcVelocityCourse)
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
                foreach (var filteredTrack in args.FilteredTracks)
            {
                var updatedTrack = UpdatedTracks.Find(i => i.Tag == filteredTrack.Tag);
                if (updatedTrack == null)
                {
                    UpdatedTracks.Add(filteredTrack);
                }

                else
                {
                    filteredTrack.Course = _calc.CalculateCourse(UpdatedTracks[UpdatedTracks.IndexOf(updatedTrack)], filteredTrack);
                    filteredTrack.Velocity =
                        _calc.CalculateVelocity(UpdatedTracks[UpdatedTracks.IndexOf(updatedTrack)], filteredTrack);
                    UpdatedTracks[UpdatedTracks.IndexOf(updatedTrack)] = filteredTrack;
                }
                }
            }
            var handler = TracksUpdated;
            handler?.Invoke(this, new TracksUpdatedEventArgs(UpdatedTracks));
        }
    }
}