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
            var airspace = new Airspace();
            var filtering = new Filtering(airspace, transponderdataReader);
            var calc = new CalcVelocityCourse();
            var trackUpdate = new TrackUpdate(filtering, calc);
            //var proximityDetection = new ProximityDetection(trackUpdate);
            var trackRender = new TrackRender(trackUpdate);
            //var eventRender = new EventRender(proximityDetection);

            Console.ReadLine();
        }
    }
}
