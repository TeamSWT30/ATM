using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;
using NUnit.Framework;
using NSubstitute;
using TransponderReceiver;


namespace ATM.Test.Unit
{
    [TestFixture]
    class CalcVelocityCourseUnitTests
    {
        private CalcVelocityCourse _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new CalcVelocityCourse();
        }

        [Test]

        public void CalcVelocity_StraightHorisontal_RightVelocityOut()
        {
            Track oldTrack = new Track();
            Track newTrack = new Track();

            oldTrack.X = 20000;
            oldTrack.Y = 20000;
            oldTrack.Altitude = 3500;
            oldTrack.TimeStamp = new DateTime(2015, 04, 25, 12, 00, 00);

            newTrack.X = 30000;
            newTrack.Y = 20000;
            newTrack.Altitude = 3500;
            newTrack.TimeStamp = new DateTime(2015, 04, 25, 12, 00, 50);



            Assert.That(_uut.CalculateVelocity(oldTrack, newTrack), Is.EqualTo(200));
        }

        [Test]

        public void CalcVelocity_StraightVertical_RightVelocityOut()
        {
            Track oldTrack = new Track();
            Track newTrack = new Track();

            oldTrack.X = 20000;
            oldTrack.Y = 30000;
            oldTrack.Altitude = 3500;
            oldTrack.TimeStamp = new DateTime(2015,04,25,12,00,00);

            newTrack.X = 20000;
            newTrack.Y =20000;
            newTrack.Altitude = 3500;
            newTrack.TimeStamp = new DateTime(2015, 04, 25, 12, 00, 50);



            Assert.That(_uut.CalculateVelocity(oldTrack, newTrack), Is.EqualTo(200));
        }

        [Test]
        public void CalcVelocity_Diagonal_RightVelocityOut()
        {
            Track oldTrack = new Track();
            Track newTrack = new Track();

            oldTrack.X = 30000;
            oldTrack.Y = 30000;
            oldTrack.Altitude = 3500;
            oldTrack.TimeStamp = new DateTime(2015, 04, 25, 12, 00, 00);

            newTrack.X = 20000;
            newTrack.Y = 20000;
            newTrack.Altitude = 3500;
            newTrack.TimeStamp = new DateTime(2015, 04, 25, 12, 01, 00);


            //Afrundet 235,7 m/s
            Assert.That(_uut.CalculateVelocity(oldTrack, newTrack), Is.EqualTo(235.70226039551582));
        }
        /*
        [Test]
        public void CalcAngle_Tracks_x1_y2_AngelIs63dot43l()
        {
            Track track1 = new Track();
            Track track2 = new Track();

            Assert.That(_uut.CalculateCourse(track1, track2), Is.EqualTo(63.43));
        }
        [Test]
        public void CalcAngle_Angleless0_degreepositiv()
        {
            Track track1 = new Track();
            Track track2 = new Track();

            
            Assert.That(_uut.CalculateCourse(track1, track2), Is.EqualTo(78.69));
        }
        */
    }
}
