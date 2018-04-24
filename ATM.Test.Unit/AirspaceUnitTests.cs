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
            _track = Substitute.For<Track>();

        }

        [TestCase(10000, 10000)]
        [TestCase(11000, 11000)]
        [TestCase(89000, 89000)]
        [TestCase(90000, 90000)]
        public void TrackInsideAirspace_ReturnTrue(int x, int y)
        {
            var _coordinates= _uut.

            Assert.That(_coordinates.Contains(x, y), Is.EqualTo(true));
        }

        [TestCase(9999, 10000)]
        [TestCase(10000, 9999)]
        [TestCase(9999, 9999)]
        [TestCase(90000, 90001)]
        [TestCase(90001, 90000)]
        [TestCase(90001, 90001)]
        public void TrackInsideAirspace_ReturnFalse(int x, int y)
        {
            var _coordinates = _uut.();

            Assert.That(_coordinates.Contains(x, y), Is.EqualTo(false));
        }


    }
}
