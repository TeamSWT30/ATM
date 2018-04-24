using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public interface ICalcVelocityCourse
    {
        int CalculateVelocity(Track oldTrack, Track newTrack);
        int CalculateCourse(Track oldTrack, Track newTrack);
    }
}
