using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;

namespace ATM
{
    public class ProximityDetection : IProximityDetection
    {
        public event EventHandler<SeperationEventArgs> Seperation;

        public ProximityDetection(ITrackUpdate trackUpdate)
        {
            trackUpdate.TracksUpdated += DetectProximity;
        }

        private void DetectProximity(object o, TracksUpdatedEventArgs args)
        {
            foreach (var track1 in args.UpdatedTracks)
            {
                foreach (var track2 in args.UpdatedTracks)
                {
                    int horisontalDist = (int)Math.Sqrt(Math.Pow(track1.X - track2.X, 2) + (int)Math.Pow(track1.Y - track2.Y, 2));
                    int verticalDist = Math.Abs(track1.Altitude - track2.Altitude);
                    if (verticalDist < 300 && horisontalDist < 5000 && track1.Tag != track2.Tag)
                    {

                        var handler = Seperation;
                        handler?.Invoke(this, new SeperationEventArgs(track1.Tag, track2.Tag, DateTime.Now));
                    }
                }
            }
        }
    }
}
