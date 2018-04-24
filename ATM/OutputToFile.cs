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
        public OutputToFile(Conflict conflict)
        {
            conflict.SeperationEvent += WriteToFile;
        }
        public void WriteToFile(object sender, SeperationEventArgs e)
        {
            string path = "ConflictingTracks.txt";

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.Write("Log created");
                }
            }

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.Write("Seperationevent on" + e.Tag1 + " and " + e.Tag2 + " at time: " + e.Time);
            }
        }

    }
}
