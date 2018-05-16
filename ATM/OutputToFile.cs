//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ATM.Interfaces;


//namespace ATM
//{
//    class OutputToFile : IOutputToFile
//    {
//        public OutputToFile(IProximityDetection proximityDetection)
//        {
//            proximityDetection.Seperation += WriteToFile;
//        }
//        public void WriteToFile(object sender, SeperationEventArgs args)
//        {
//            string path = "SeperationsLog.txt";
//            FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
//            StreamWriter sw = new StreamWriter(fs);

//            using (sw)
//            {
//                sw.WriteLine("Seperationevent on" + args.Tag1 + " and " + args.Tag2 + " at time: " + args.Time);
//            }
//        }

//    }
//}
