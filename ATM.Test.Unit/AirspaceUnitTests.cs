using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using ATM;
using TransponderReceiver;

namespace ATM.Test.Unit

{
    [TestFixture]
    class AirspaceUnitTests
    {
        private ATM.Airspace _uut;
        private Track _track;

        [SetUp]
        public void setup()
        {
            _uut = new ATM.Airspace();
        }

        [TestCase(10000, 10000,20000)]
        [TestCase(11000, 11000,20000)]
        [TestCase(89000, 89000,200)]
        [TestCase(90000, 90000,200)]
        public void TrackInsideAirspace_ReturnTrue(int x, int y,int alt)
        {
            _track = new Track(){X = x, Y = y, Altitude = alt};

            Assert.That(_uut.IsTrackInAirspace(_track),Is.EqualTo(true));
        }

        [TestCase(9999, 10000,1000)]
        [TestCase(10000, 9999,1000)]
        [TestCase(9999, 9999,1000)]
        [TestCase(90000, 90001,1000)]
        [TestCase(90001, 90000,1000)]
        [TestCase(90001, 90001,1000)]
        [TestCase(10000, 10000, 199)]
        [TestCase(10000, 10000, 200001)]
        public void TrackOutsideAirspace_ReturnFalse(int x, int y, int alt)
        {
            _track = new Track() { X = x, Y = y,Altitude = alt };

            Assert.That(_uut.IsTrackInAirspace(_track), Is.EqualTo(false));
        }


    }
}
