using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class CalcVelocityCourse
    {
        public int CalculateVelocity(int oldX, int newX, int oldY, int newY, DateTime oldTime, DateTime newTime)
        {
            int time = (int) newTime.Subtract(oldTime).TotalSeconds;
            int distance = (int) Math.Sqrt(Math.Pow(newX - oldX, 2) + (int) Math.Pow(newY - oldY, 2));
            int velocity = distance / time;
            return velocity;
        }

        public int CalculateCourse(int oldX, int newX, int oldY, int newY)
        {
            // β = atan2(X,Y),
            // X = cos θb * sin ∆L
            // Y = cos θa * sin θb – sin θa * cos θb * cos ∆L

            int X = (int) Math.Cos(newX) * (int) Math.Sin(newY - oldY);
            int Y = (int) Math.Cos(oldX) * (int) Math.Sin(newX) -
                    (int) Math.Sin(newX) * (int) Math.Cos(newX) * (int) Math.Cos(newY - oldY);
            int course = (int)Math.Atan2(X, Y);
            return course;
        }
    }
}
