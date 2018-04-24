using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class SeperationEventArgs : EventArgs
    {
        public SeperationEventArgs(string tag1, string tag2, DateTime time)
        {
            Tag1 = tag1;
            Tag2 = tag2;
            Time = time;
        }
        public string Tag1 { get; set; }
        public string Tag2 { get; set; }
        public DateTime Time { get; set; }

    }
    public interface IConflict
    {
        event EventHandler<SeperationEventArgs> SeperationEvent;
        void CheckForConflicts(List<Track> Tracks);
    }
}
