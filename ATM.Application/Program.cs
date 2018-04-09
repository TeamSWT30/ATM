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
            var transponderdataReader = new TransponderdataReader();
            var trackObjectification = new TrackObjectification(transponderReceiver,transponderdataReader);

            Console.ReadLine();
        }
    }
}
