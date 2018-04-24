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
    public class TrackObjectificationUnitTests
    {
        private TrackObjectification _uut;
        private ITransponderdataReader _TpDataReader;
        private ITransponderReceiver _TpDataReceiver;
        private ITrackRender _trackRender;
        private int _nEventsReceived;
        private IAirspace _airSpace;
        private List<string> _rawTransponderDataList; 
        [SetUp]
        public void SetUp()
        {

            _trackRender = Substitute.For<ITrackRender>();
            _TpDataReader = Substitute.For<ITransponderdataReader>();
            _TpDataReceiver = Substitute.For<ITransponderReceiver>();
            _airSpace = Substitute.For<IAirspace>();
            _uut = new TrackObjectification(_TpDataReceiver,_TpDataReader,_trackRender,_airSpace);

            _rawTransponderDataList = new List<string>();
            _rawTransponderDataList.Add("ATR423;39045;12932;14000;20151006213456789");

            _uut.TracksChanged += (o, args) => { ++_nEventsReceived; };

        }

        [Test]
        public void AddOneTrackToList_ReadTrackDataCorrectInput()
        {
            var args = new RawTransponderDataEventArgs(_rawTransponderDataList);

            _TpDataReceiver.TransponderDataReady += Raise.EventWith(args);

            _TpDataReader.Received(1).ReadTrackData("ATR423;39045;12932;14000;20151006213456789");

        }

        [TestCase(0)]
        [TestCase(5)]
        public void AddMultipleTracksToList_ReadTrackDataCorrectInput(int length)
        {
            var args = new RawTransponderDataEventArgs(_rawTransponderDataList);

            for (int i = 0; i < length; i++)
            {
                _TpDataReceiver.TransponderDataReady += Raise.EventWith(args);
            }

            _TpDataReader.Received(length).ReadTrackData("ATR423;39045;12932;14000;20151006213456789");

        }


        [TestCase(0)]
        [TestCase(5)]
        public void AddMultipleTracksToList_CorrectNumberOfEvents(int numberOfEvents)
        {
            var args = new RawTransponderDataEventArgs(_rawTransponderDataList);

            for (int i = 0; i < numberOfEvents; i++)
            {
                _TpDataReceiver.TransponderDataReady += Raise.EventWith(args);
            }

            Assert.That(_nEventsReceived, Is.EqualTo(numberOfEvents));

        }





    }
}
