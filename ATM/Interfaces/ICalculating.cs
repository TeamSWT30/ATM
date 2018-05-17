using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Interfaces
{
    public interface ICalculating
    {
        double CalculateVelocity(Track oldTrack, Track newTrack);
        double CalculateCourse(Track oldTrack, Track newTrack);
    }
}
