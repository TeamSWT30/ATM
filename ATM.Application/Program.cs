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
            var transponderdataReader = new Parsing(transponderReceiver);
            var airspace = new Airspace();
            var filtering = new Filtering(airspace, transponderdataReader);
            var calc = new Calculating();
            var trackUpdate = new Updating(filtering, calc);
            var output = new Output();
            //var proximityDetection = new ProximityDetection(trackUpdate);
            var trackRender = new Rendering(trackUpdate, output);
            //var eventRender = new EventRender(proximityDetection);

            Console.ReadLine();
        }
    }
}
