using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using ATM;
using ATM.Interfaces;
using TransponderReceiver;

namespace ATM.Test.Unit
{
    [TestFixture]
    class FilteringUnitTests
    {
        private Filtering _uut;
        private IAirspace _airspace;
        private IParsing _dataReader;
        private List<Track> _filteredTracks;
        private Track insideLowerBoundry;
        private Track insideMiddle;
        private Track insideUpperBoundry;
        private Track outsideLowerBoundry;
        private Track outsideUpperBoundry;
        private int _nEventsRecieved;
        

        [SetUp]
        public void Setup()
        {
            _nEventsRecieved = 0;
            insideLowerBoundry = new Track() { Altitude = 500, X = 10000, Y = 10000 };
            insideMiddle = new Track() { Altitude = 10000, X = 50000, Y = 50000 };
            insideUpperBoundry = new Track() { Altitude = 20000, X = 90000, Y = 90000 };
            outsideLowerBoundry = new Track() { Altitude = 499, X = 9999, Y = 9999 };
            outsideUpperBoundry = new Track() { Altitude = 20001, X = 90001, Y = 90001 };
            _dataReader = Substitute.For<IParsing>();
            _airspace = Substitute.For<IAirspace>();
            _airspace.IsTrackInAirspace(insideLowerBoundry).Returns(true);
            _airspace.IsTrackInAirspace(insideMiddle).Returns(true);
            _airspace.IsTrackInAirspace(insideUpperBoundry).Returns(true);
            _airspace.IsTrackInAirspace(outsideLowerBoundry).Returns(false);
            _airspace.IsTrackInAirspace(outsideUpperBoundry).Returns(false);
            _uut = new Filtering(_airspace, _dataReader);

            _uut.TracksFiltered += (o, args) =>
            {
                _filteredTracks = args.FilteredTracks;
                ++_nEventsRecieved;
            };
        }

        [Test]
        public void FilterTrack_OneTracksInAirspace_AddedToFilteredTracks()
        {
            List<Track> testTracks = new List<Track>();
            testTracks.Add(insideMiddle);
            var args = new TracksChangedEventArgs(testTracks);

            _dataReader.TracksChanged += Raise.EventWith(args);

            Assert.That(_filteredTracks.Contains(insideMiddle));
        }

        [Test]
        public void FilterTrack_ThreeTracksInAirspace_AddedToFilteredTracks()
        {
            List<Track>testTracks = new List<Track>();
            testTracks.Add(insideLowerBoundry);
            testTracks.Add(insideMiddle);
            testTracks.Add(insideUpperBoundry);
            var args = new TracksChangedEventArgs(testTracks);

            _dataReader.TracksChanged += Raise.EventWith(args);

            Assert.That(_filteredTracks.Count, Is.EqualTo(3));
        }

        [Test]
        public void FilterTrack_TrackOutsideAirspace_NotAddedToFilteredTracks()
        {
            List<Track> testTracks = new List<Track>();
            testTracks.Add(outsideLowerBoundry);
            var args = new TracksChangedEventArgs(testTracks);

            _dataReader.TracksChanged += Raise.EventWith(args);

            Assert.That(!_filteredTracks.Contains(outsideLowerBoundry));
        }
        [Test]
        public void FilterTrack_TwoTracksOutsideAirspace_NotAddedToFilteredTracks()
        {
            List<Track> testTracks = new List<Track>();
            testTracks.Add(outsideLowerBoundry);
            testTracks.Add(outsideUpperBoundry);
            var args = new TracksChangedEventArgs(testTracks);

            _dataReader.TracksChanged += Raise.EventWith(args);

            Assert.That(_filteredTracks.Count, Is.EqualTo(0));
        }

        [Test]
        public void FilterTrack_TracksAddedTwice_NumberOfEventsReceivedIsCorrect()
        {
            List<Track> testTracks = new List<Track>();
            testTracks.Add(insideLowerBoundry);
            var args = new TracksChangedEventArgs(testTracks);

            _dataReader.TracksChanged += Raise.EventWith(args);
            args.Tracks.Add(insideUpperBoundry);
            _dataReader.TracksChanged += Raise.EventWith(args);

            Assert.That(_nEventsRecieved, Is.EqualTo(2));
        }
    }
}
