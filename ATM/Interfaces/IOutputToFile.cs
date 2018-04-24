using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public interface IOutputToFile
    {
        void WriteToFile(object sender, SeperationEventArgs e);

    }
}