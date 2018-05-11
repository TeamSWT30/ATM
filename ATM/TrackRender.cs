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
            Console.Clear();
            foreach (var track in args.UpdatedTracks)
            {
                RenderTrack(track);
            }
        }

        public void RenderTrack(Track track)
        {
            Console.WriteLine("Tag: " + track.Tag);
            Console.WriteLine("X: " + track.X);
            Console.WriteLine("Y: " + track.Y);
            Console.WriteLine("Altitude: " + track.Altitude);
            Console.WriteLine("Velocity: " + track.Velocity); 
            Console.WriteLine("Course: " + track.Course);
            Console.WriteLine("Timestamp: " + track.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss:fff"));
            Console.WriteLine();
        }
    }
}
