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
    class TrackRenderUnitTests
    {
        private TrackRender _uut;
        private ITrackUpdate _trackUpdate;
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
            _trackUpdate = Substitute.For<ITrackUpdate>();
            _output = Substitute.For<IOutput>();
            _uut = new TrackRender(_trackUpdate, _output);
        }

        [Test]
        public void RenderTrack_RenderOneTrack_CorrectOutput()
        {
            List<Track> testTracks = new List<Track>();
            testTracks.Add(_testTrack1);
            var args = new TracksUpdatedEventArgs(testTracks);
            _trackUpdate.TracksUpdated += Raise.EventWith(args);

            _output.Received().OutputLine("Tag: " + _testTrack1.Tag + ", X: " + _testTrack1.X + ", Y: " +
                                          _testTrack1.Y + ", Altitude: " + _testTrack1.Altitude + ", Velocity: " +
                                          _testTrack1.Velocity + ", Course: " + _testTrack1.Course + ", Timestamp: " +
                                          _testTrack1.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss:fff"));
        }

        [Test]
        public void RenderTrack_RenderTwoTracks_CorrectOutput()
        {
            List<Track> testTracks = new List<Track>();
            testTracks.Add(_testTrack1);
            testTracks.Add(_testTrack2);
            var args = new TracksUpdatedEventArgs(testTracks);
            _trackUpdate.TracksUpdated += Raise.EventWith(args);

            _output.Received().OutputLine("Tag: " + _testTrack1.Tag + ", X: " + _testTrack1.X + ", Y: " +
                                          _testTrack1.Y + ", Altitude: " + _testTrack1.Altitude + ", Velocity: " +
                                          _testTrack1.Velocity + ", Course: " + _testTrack1.Course + ", Timestamp: " +
                                          _testTrack1.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss:fff"));
            _output.Received().OutputLine("Tag: " + _testTrack2.Tag + ", X: " + _testTrack2.X + ", Y: " +
                                          _testTrack2.Y + ", Altitude: " + _testTrack2.Altitude + ", Velocity: " +
                                          _testTrack2.Velocity + ", Course: " + _testTrack2.Course + ", Timestamp: " +
                                          _testTrack2.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss:fff"));
        }
    }
}
