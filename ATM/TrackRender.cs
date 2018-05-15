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
        public TrackRender(ITrackUpdate trackUpdate)
        {
            trackUpdate.TracksUpdated += RenderTracks;
        }

        private void RenderTracks(object o, TracksUpdatedEventArgs args)
        {
            foreach (var track in args.UpdatedTracks)
            {
                RenderTrack(track);
            }
        }

        public void RenderTrack(Track track)
        {
            Console.WriteLine("Tag: " + track.Tag + ", X: " + track.X + ", Y: " + track.Y + ", Altitude: " + track.Altitude + ", Velocity: " + track.Velocity + ", Course: " + track.Course + ", Timestamp: " + track.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss:fff"));
        }
    }
}
