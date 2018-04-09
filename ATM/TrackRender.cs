using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class TrackRender : ITrackRender
    {
        public void RenderTrack(Track track)
        {
            Console.WriteLine("Tag: " + track.Tag);
            Console.WriteLine("X: " + track.X);
            Console.WriteLine("Y: " + track.Y);
            Console.WriteLine("Altitude: " + track.Altitude);
            Console.WriteLine("Timestamp: " + track.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss:fff"));
        }
    }
}
