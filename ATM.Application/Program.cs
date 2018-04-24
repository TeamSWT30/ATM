using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace ATM.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var transponderReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            var transponderdataReader = new TransponderdataReader(transponderReceiver);
            var conflict = new Conflict();
            var trackRender = new TrackRender(conflict);
            var airspace = new Airspace();
            var calc = new CalcVelocityCourse();
            var trackObjectification = new TrackObjectification(airspace, calc, conflict, trackRender, transponderdataReader);

            Console.ReadLine();
        }
    }
}
