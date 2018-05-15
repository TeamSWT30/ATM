using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;

namespace ATM
{
    public class Conflict : IConflict
    {
        public event EventHandler<SeperationEventArgs> SeperationEvent;
        public List<Track> ConflictingTracks;

        public void CheckForConflicts(List<Track> Tracks)
        {
            
            foreach (var track1 in Tracks)
            {
                foreach (var track2 in Tracks)
                {
                    int horisontalDist = (int)Math.Sqrt(Math.Pow(track1.X - track2.X, 2) + (int)Math.Pow(track1.Y - track2.Y, 2));
                    int verticalDist = Math.Abs(track1.Altitude - track2.Altitude);
                    if (verticalDist < 300 && horisontalDist < 5000 && track1.Tag != track2.Tag)
                    {
                        SeperationEventArgs SepEvent = new SeperationEventArgs(track1.Tag, track2.Tag, DateTime.Now);
                        OnSperationEvent(SepEvent);

                        ConflictingTracks.Add(track1);
                        ConflictingTracks.Add(track2);
                        
                    }
                }
            }

        }

        public List<Track> ListOfConflicts()
        {
            return ConflictingTracks;
        }


        private void OnSperationEvent(SeperationEventArgs args)
        {
            var handler = SeperationEvent;
            handler?.Invoke(this, args);
        }
    }
}
