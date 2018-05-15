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
    class TrackUpdateUnitTests
    {
        private List<Track> UpdatedTracks;
        private ICalcVelocityCourse _calc;
        private TrackUpdate _uut;
        private IFiltering filtering;
        private ICalcVelocityCourse calcVelocityCourse;

        [SetUp]
        public void SetUp()
        {
            _uut = new TrackUpdate(filtering, calcVelocityCourse);
        }

        [Test]
        public void TrackUpdated()
        {

        }

    }
}
