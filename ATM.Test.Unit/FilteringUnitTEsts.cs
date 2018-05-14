/*using System;
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
        
        [TestCase(10000, 10000, 20000)]
        [TestCase(11000, 11000, 20000)]
        [TestCase(89000, 89000, 500)]
        [TestCase(90000, 90000, 500)]
        public void TrackInAirspaceAdded(int x, int y, int alt)
        {
            _track = new Track() { X = x, Y = y, Altitude = alt };


            Assert.That(_uut.FilterTrack(), Is.EqualTo(4));
        }
        
        [Test]
        public void TrackNotInAirspace()
        {

        }
    }
} */
