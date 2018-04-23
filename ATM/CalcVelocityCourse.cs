using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class CalcVelocityCourse
    {
        public int CalculateVelocity(Track oldTrack, Track newTrack)
        {
            int time = (int) newTrack.TimeStamp.Subtract(oldTrack.TimeStamp).TotalSeconds;
            int distance = (int) Math.Sqrt(Math.Pow(newTrack.X- oldTrack.X, 2) + (int) Math.Pow(newTrack.Y - oldTrack.Y, 2));
            int velocity = distance / time;
            return velocity;
        }

        public int CalculateCourse(Track oldTrack, Track newTrack)
        {
            // β = atan2(X,Y),
            // X = cos θb * sin ∆L
            // Y = cos θa * sin θb – sin θa * cos θb * cos ∆L

            int X = (int) Math.Cos(newTrack.X) * (int) Math.Sin(newTrack.Y - oldTrack.Y);
            int Y = (int) Math.Cos(oldTrack.X) * (int) Math.Sin(newTrack.X) -
                    (int) Math.Sin(newTrack.X) * (int) Math.Cos(newTrack.X) * (int) Math.Cos(newTrack.Y - oldTrack.Y);
            int course = (int)Math.Atan2(X, Y);
            return course;
        }
    }
}
