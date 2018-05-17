using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ATM;
using ATM.Interfaces;
using NSubstitute;
using TransponderReceiver;

namespace ATM.Test.Integration
{
    [TestFixture]
    class IT1_FilteringParsing
    {
        private Filtering _uut;
        private Parsing _parsing;
        private Airspace _airspace;
        private ITransponderReceiver _transponderReceiver;
        private List<Track> _filteredTracks;
        private string transponderDataInside;
        private string transponderDataOutside;
        private int _nEventsReceived;

        [SetUp]
        public void SetUp()
        {
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _parsing = new Parsing(_transponderReceiver);
            _airspace = new Airspace();
            _uut = new Filtering(_airspace, _parsing);
            transponderDataInside = "ATR423;39045;12932;14000;20151006213456789";
            transponderDataOutside = "ATR424;95000;95000;25000;20151006213457789";
            _nEventsReceived = 0;

            _uut.TracksFiltered += (o, args) =>
            {
                _filteredTracks = args.FilteredTracks;
                ++_nEventsReceived;
            };
        }

        [Test]
        public void ParsingToFiltering_TransponderdataStringInsideAirspace_FilteredTracksContainsNewTrack()
        {
            List<string> transponderStrings = new List<string>();
            transponderStrings.Add(transponderDataInside);
            var args = new RawTransponderDataEventArgs(transponderStrings);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);

            string[] seperatedStrings = transponderDataInside.Split(';');

            Assert.That(_filteredTracks[0].Tag, Is.EqualTo(seperatedStrings[0]));
        }


        [Test]
        public void ParsingToFiltering_TransponderdataStringOutsideAirspace_FilteredTracksDoesNotContainsNewTrack()
        {
            List<string> transponderStrings = new List<string>();
            transponderStrings.Add(transponderDataOutside);
            var args = new RawTransponderDataEventArgs(transponderStrings);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);

            Assert.That(_filteredTracks.Count, Is.EqualTo(0));
        }

        [Test]
        public void FilterTrack_TracksAddedTwice_NumberOfEventsReceivedIsCorrect()
        {
            List<string> transponderStrings = new List<string>();
            transponderStrings.Add(transponderDataInside);
            var args = new RawTransponderDataEventArgs(transponderStrings);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);
            args.TransponderData.Add(transponderDataOutside);
            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);

            Assert.That(_nEventsReceived, Is.EqualTo(2));
        }
    }
}
