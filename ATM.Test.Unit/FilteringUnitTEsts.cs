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
        private ITransponderdataReader _dataReader;
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
            _dataReader = Substitute.For<ITransponderdataReader>();
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
        public void FilterTrack_ThreeTracksInAirspace_AddedToFilteredTracks()
        {
            List<Track>testTracks = new List<Track>();
            testTracks.Add(insideLowerBoundry);
            testTracks.Add(insideMiddle);
            testTracks.Add(insideUpperBoundry);
            var args = new TracksChangedEventArgs(testTracks);

            _dataReader.TracksChanged += Raise.EventWith(args);

            Assert.That(_filteredTracks.Contains(insideLowerBoundry));
            Assert.That(_filteredTracks.Contains(insideMiddle));
            Assert.That(_filteredTracks.Contains(insideUpperBoundry));
        }

        [Test]
        public void FilterTrack_TwoTracksOutsideAirspace_NotAddedToFilteredTracks()
        {
            List<Track> testTracks = new List<Track>();
            testTracks.Add(outsideLowerBoundry);
            testTracks.Add(outsideUpperBoundry);
            var args = new TracksChangedEventArgs(testTracks);

            _dataReader.TracksChanged += Raise.EventWith(args);

            Assert.That(!_filteredTracks.Contains(outsideLowerBoundry));
            Assert.That(!_filteredTracks.Contains(outsideUpperBoundry));
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



        //SW corner, max altitude
        /*[TestCase(10000, 10000, 20000)]
        //completely inside airspace
        [TestCase(50000, 50000, 12000)]
        //NE corner, min altitude
        [TestCase(90000, 90000, 500)]

        //Burde være 1, men er 0
        public void TrackInAirspaceAdded(int x, int y, int alt)
        {
            _track = new Track() { X = x, Y = y, Altitude = alt };

            //_filter.TracksFiltered += Raise.EventWith < new TracksFilteredEventArgs > ();

            Assert.That(_uut.FilteredTracks.Count, Is.EqualTo(1));
        }*/

        //[TestCase(9999, 9999, 499)]
        //[TestCase(90001, 90001, 20001)]
        //public void TrackNotInAirspace(int x, int y, int alt)
        //{
        //    var _track = new Track() { X = x, Y = y, Altitude = alt };

        //    //_filter.TracksFiltered += Raise.EventWith < new TracksFilteredEventArgs > ()
        //    ;


        //    Assert.That(_uut.FilteredTracks.Count, Is.EqualTo(0));
        //}
    }
}
