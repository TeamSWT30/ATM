//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ATM.Interfaces;

//namespace ATM
//{
//    public class EventRender : IEventRender
//    {
//        public EventRender(IProximityDetection proximityDetection)
//        {
//            proximityDetection.Seperation += RenderEvents;
//        }

//        private void RenderEvents(object o, SeperationEventArgs args)
//        {
//            Console.WriteLine("Track with tag: " + args.Tag1 + " and track with tag: " + args.Tag2 + " are in conflict. Seperation needed! Time: " + args.Time);
//        }
//    }
//}
