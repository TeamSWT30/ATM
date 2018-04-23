using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Interfaces
{
    public class SeperationEventArgs : EventArgs
    {
        public string tag1 { get; set; };
        public string tag2 { get; set; };
        public DateTime time { get; set; };

    }
    interface IConflict
    {
        event EventHandler<SeperationEventArgs> SeperationEvent;
    }
}
