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
        public TrackRender(IConflict conflict)
        {
            conflict.SeperationEvent += RenderSeperation;
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
        }

        public void RenderSeperation(object sender, SeperationEventArgs e)
        {
            Console.WriteLine("Track with tag: " + e.Tag1 + " and track with tag: " + e.Tag2 + " are in conflict. Seperation needed! Time: " + e.Time);
        }
    }
}
