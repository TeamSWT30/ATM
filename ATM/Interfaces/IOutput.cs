﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Interfaces
{
    public interface IOutput
    {
        void OutputLine(string line);
        void Clear();
    }
}
