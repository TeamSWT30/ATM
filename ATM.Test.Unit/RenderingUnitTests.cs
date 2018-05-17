using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace ATM.Test.Unit
{
    [TestFixture]
    class RenderingUnitTests
    {
        private Rendering _uut;
        private IUpdating _trackUpdate;
        private IOutput _output;
        private Track _testTrack1;
        private Track _testTrack2;

        [SetUp]
        public void Setup()
        {
            _testTrack1 = new Track()
            {
                Altitude = 10000,
                X = 50000,
                Y = 50000,
                Course = 200,
                Tag = "test1",
                TimeStamp = DateTime.Now,
                Velocity = 300
            };
            _testTrack2 = new Track()
            {
                Altitude = 11000,
                X = 51000,
                Y = 51000,
                Course = 210,
                Tag = "test2",
                TimeStamp = DateTime.Now,
                Velocity = 310
            };
            _trackUpdate = Substitute.For<IUpdating>();
            _output = Substitute.For<IOutput>();
            _uut = new Rendering(_trackUpdate, _output);
        }

        [Test]
        public void RenderTrack_RenderTrack_CorrectOutput()
        {
            List<Track> testTracks = new List<Track>();
            testTracks.Add(_testTrack1);
            var args = new TracksUpdatedEventArgs(testTracks);
            _trackUpdate.TracksUpdated += Raise.EventWith(args);

            _output.Received().OutputLine("Tag: " + _testTrack1.Tag + ", X: " + _testTrack1.X + ", Y: " +
                                          _testTrack1.Y + ", Altitude: " + _testTrack1.Altitude + ", Velocity: " +
                                          _testTrack1.Velocity + ", Course: " + _testTrack1.Course);
        }
    }
}
