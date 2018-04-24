using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;


namespace ATM
{
    class OutputToFile: IOutputToFile
    {
        public void WriteToFile(string track1, string track2)
        {
            Directory.CreateDirectory(@"C:\\ATM\\");
            using (StreamWriter log = File.AppendText(@"C:\\ATM\\CollidingTracks.txt"))
            {
                log.Write(track1 + " and " + track2);
            }
        }

    }
}
