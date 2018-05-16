using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;

namespace ATM
{
    public class TrackRender : ITrackRender
    {
        private IOutput _output;
        public TrackRender(ITrackUpdate trackUpdate, IOutput output)
        {
            _output = output;
            trackUpdate.TracksUpdated += RenderTracks;
        }

        private void RenderTracks(object o, TracksUpdatedEventArgs args)
        {
            _output.Clear();
            foreach (var track in args.UpdatedTracks)
            {
                RenderTrack(track);
            }
        }

        public void RenderTrack(Track track)
        {
            _output.OutputLine("Tag: " + track.Tag + ", X: " + track.X + ", Y: " + track.Y + ", Altitude: " + track.Altitude + ", Velocity: " + track.Velocity + ", Course: " + track.Course + ", Timestamp: " + track.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss:fff"));
        }
    }
}
