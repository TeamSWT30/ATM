using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;

namespace ATM
{
    public class Calculating : ICalculating
    {
        public double CalculateVelocity(Track oldTrack, Track newTrack)
        {
            double time = newTrack.TimeStamp.Subtract(oldTrack.TimeStamp).TotalSeconds;
            double distance = Math.Sqrt(Math.Pow(newTrack.X-oldTrack.X,2) + Math.Pow(newTrack.Y - oldTrack.Y, 2));
            double velocity = distance / time;
            return velocity;
        }

        public double CalculateCourse(Track oldTrack, Track newTrack)
        {
            // β = atan2(X,Y),
            // X = cos θb * sin ∆L
            // Y = cos θa * sin θb – sin θa * cos θb * cos ∆L

            double X = Math.Abs(newTrack.X - oldTrack.X);
            double Y = Math.Abs(newTrack.Y - oldTrack.Y);
            double course = Math.Atan2(X, Y) * (180 / Math.PI);


            if (newTrack.X > oldTrack.X && newTrack.Y <= oldTrack.Y)
            {
                course += 90;
            }
            else if (newTrack.X <= oldTrack.X && newTrack.Y < oldTrack.Y)
            {
                course += 180;
            }
            else if (newTrack.X < oldTrack.X && newTrack.Y > oldTrack.Y)
            {
                course += 270;
            }
            return course;
        }
    }
}
