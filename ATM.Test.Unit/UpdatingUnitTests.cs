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
    class UpdatingUnitTests
    {
        private Updating _uut;
        private IFiltering _filtering;
        private ICalculating _calc;
        private List<Track> _updatedTracks;
        private int _nEventsRecieved;
        private Track _testTrack1;
        private Track _testTrack2;


        [SetUp]
        public void Setup()
        {
            _nEventsRecieved = 0;
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
                Tag = "test2",
                X = 51000,
                Y = 51000,
                Course = 210,
                Velocity = 310,
                TimeStamp = DateTime.Now
            };
            _filtering = Substitute.For<IFiltering>();
            _calc = Substitute.For<ICalculating>();
            _uut = new Updating(_filtering, _calc);

            _uut.TracksUpdated += (o, args) =>
            {
                _updatedTracks = args.UpdatedTracks;
                ++_nEventsRecieved;
            };
        }

        [Test]
        public void UpdateTrack_NoTracksInUpdatedTracks_AddedToUpdatedTracks()
        {
            List<Track> testTracks = new List<Track>();
            testTracks.Add(_testTrack1);
            var args = new TracksFilteredEventArgs(testTracks);
            _filtering.TracksFiltered += Raise.EventWith(args);

            Assert.That(_updatedTracks.Contains(_testTrack1));
        }

        [Test]
        public void UpdateTrack_OneTrackInUpdatedTracksNewTrackAdded_NewTrackAddedToUpdatedTracks()
        {
            List<Track> testTracks = new List<Track>();
            testTracks.Add(_testTrack1);
            var args = new TracksFilteredEventArgs(testTracks);

            _filtering.TracksFiltered += Raise.EventWith(args);
            args.FilteredTracks.Add(_testTrack2);
            _filtering.TracksFiltered += Raise.EventWith(args);

            Assert.That(_updatedTracks.Contains(_testTrack1));
            Assert.That(_updatedTracks.Contains(_testTrack2));
        }

        [Test]
        public void UpdateTrack_OneTrackInUpdatedTracksTrackChanged_TrackUpdatedInUpdatedTracks()
        {
            List<Track> testTracks = new List<Track>();
            testTracks.Add(_testTrack1);
            var args = new TracksFilteredEventArgs(testTracks);

            _filtering.TracksFiltered += Raise.EventWith(args);
            _testTrack1.X = 52000;
            _testTrack1.Y = 52000;
            _testTrack1.Altitude = 11000;


            args.FilteredTracks.Add(_testTrack1);
            _filtering.TracksFiltered += Raise.EventWith(args);

            Assert.That(_updatedTracks.Contains(_testTrack1));
            Assert.That(_updatedTracks.Count, Is.EqualTo(1));
            Assert.That(_updatedTracks[_updatedTracks.IndexOf(_testTrack1)].Tag, Is.EqualTo("test1"));
            Assert.That(_updatedTracks[_updatedTracks.IndexOf(_testTrack1)].X, Is.EqualTo(52000));
            Assert.That(_updatedTracks[_updatedTracks.IndexOf(_testTrack1)].Y, Is.EqualTo(52000));
            Assert.That(_updatedTracks[_updatedTracks.IndexOf(_testTrack1)].Altitude, Is.EqualTo(11000));
        }

        [Test]
        public void UpdateTrack_TracksAddedTwice_NumberOfEventsReceivedIsCorrect()
        {
            List<Track> testTracks = new List<Track>();
            testTracks.Add(_testTrack1);
            var args = new TracksFilteredEventArgs(testTracks);

            _filtering.TracksFiltered += Raise.EventWith(args);
            args.FilteredTracks.Add(_testTrack2);
            _filtering.TracksFiltered += Raise.EventWith(args);

            Assert.That(_nEventsRecieved, Is.EqualTo(2));
        }

    }
}
