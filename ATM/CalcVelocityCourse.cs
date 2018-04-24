using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;

namespace ATM
{
    public class CalcVelocityCourse : ICalcVelocityCourse
    {
        public double CalculateVelocity(Track oldTrack, Track newTrack)
        {
            double time = newTrack.TimeStamp.Subtract(oldTrack.TimeStamp).TotalSeconds;
            double distance = Math.Sqrt(Math.Pow(newTrack.X- oldTrack.X, 2) + Math.Pow(newTrack.Y - oldTrack.Y, 2));
            double velocity = distance / time;
            return velocity;
        }

        public double CalculateCourse(Track oldTrack, Track newTrack)
        {
            // β = atan2(X,Y),
            // X = cos θb * sin ∆L
            // Y = cos θa * sin θb – sin θa * cos θb * cos ∆L

            double X = Math.Cos(newTrack.X) * Math.Sin(newTrack.Y - oldTrack.Y);
            double Y = Math.Cos(oldTrack.X) * Math.Sin(newTrack.X) -
                    Math.Sin(newTrack.X) * Math.Cos(newTrack.X) * Math.Cos(newTrack.Y - oldTrack.Y);
            double course = Math.Atan2(X, Y);
            return course;
        }
    }
}
