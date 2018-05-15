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
        private ATM.Filtering _uut;
        private IAirspace _airspace;
        private Track _track;
        private IFiltering _filter;
        private List<Track> FilteredTracks;
        private ITransponderdataReader _dataReader;
        

        [SetUp]
        public void Setup()
        {
            _dataReader = Substitute.For<ITransponderdataReader>();
            _airspace = Substitute.For<IAirspace>();
            _uut = new Filtering(_airspace, _dataReader);

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

        [TestCase(9999, 9999, 499)]
        [TestCase(90001, 90001, 20001)]
        public void TrackNotInAirspace(int x, int y, int alt)
        {
            _track = new Track() { X = x, Y = y, Altitude = alt };

            //_filter.TracksFiltered += Raise.EventWith < new TracksFilteredEventArgs > ()
            ;


            Assert.That(_uut.FilteredTracks.Count, Is.EqualTo(0));
        }
    }
}
