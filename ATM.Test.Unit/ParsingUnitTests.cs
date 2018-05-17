using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;
using ATM;
using ATM.Interfaces;

namespace ATM.Test.Unit
{
    [TestFixture]
    public class ParsingUnitTests
    {
        private Parsing uut;
        private string transponderData;
        private string newTransponderData;
        private ITransponderReceiver _transponderReceiver;
        private List<Track> _tracks;
        private int _nEventsReceived;

        [SetUp]
        public void Setup()
        {
            _nEventsReceived = 0;
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            uut = new Parsing(_transponderReceiver);
            transponderData = "ATR423;39045;12932;14000;20151006213456789";
            newTransponderData = "ATR423;39245;13132;14000;20151006213457789";

            uut.TracksChanged += (o, args) =>
            {
                _tracks = args.Tracks;
                ++_nEventsReceived;
            };
        }

        [Test]
        public void ReadTrackData_CorrectTag()
        {
            Assert.That(uut.ReadTrackData(transponderData).Tag, Is.EqualTo("ATR423"));
        }

        [Test]
        public void ReadTrackData_CorrectX()
        {
            Assert.That(uut.ReadTrackData(transponderData).X, Is.EqualTo(39045));
        }

        [Test]
        public void ReadTrackData_CorrectY()
        {
            Assert.That(uut.ReadTrackData(transponderData).Y, Is.EqualTo(12932));
        }

        [Test]
        public void ReadTrackData_CorrectAltitude()
        {
            Assert.That(uut.ReadTrackData(transponderData).Altitude, Is.EqualTo(14000));
        }

        [Test]
        public void ReadTrackData_CorrectTimeStampYear()
        {
            Assert.That(uut.ReadTrackData(transponderData).TimeStamp.Year, Is.EqualTo(2015));
        }

        [Test]
        public void ReadTrackData_CorrectTimeStampMonth()
        {
            Assert.That(uut.ReadTrackData(transponderData).TimeStamp.Month, Is.EqualTo(10));
        }

        [Test]
        public void ReadTrackData_CorrectTimeStampDay()
        {
            Assert.That(uut.ReadTrackData(transponderData).TimeStamp.Day, Is.EqualTo(06));
        }

        [Test]
        public void ReadTrackData_CorrectTimeStampHour()
        {
            Assert.That(uut.ReadTrackData(transponderData).TimeStamp.Hour, Is.EqualTo(21));
        }

        [Test]
        public void ReadTrackData_CorrectTimeStampMinute()
        {
            Assert.That(uut.ReadTrackData(transponderData).TimeStamp.Minute, Is.EqualTo(34));
        }

        [Test]
        public void ReadTrackData_CorrectTimeStampSecond()
        {
            Assert.That(uut.ReadTrackData(transponderData).TimeStamp.Second, Is.EqualTo(56));
        }

        [Test]
        public void ReadTrackData_CorrectTimeStampMS()
        {
            Assert.That(uut.ReadTrackData(transponderData).TimeStamp.Millisecond, Is.EqualTo(789));
        }
        
        [Test]
        public void ReadTransponderdata_twoTransponderdataStringsAdded_TracksContainNewTracksWithTransponderdata()
        {
            List<string> transponderStrings = new List<string>();
            transponderStrings.Add(transponderData);
            transponderStrings.Add(newTransponderData);
            var args = new RawTransponderDataEventArgs(transponderStrings);
            
            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);

            string[] seperatedStrings = transponderData.Split(';');
            string[] seperatedStrings2 = newTransponderData.Split(';');

            Assert.That(_tracks[0].Tag, Is.EqualTo(seperatedStrings[0]));
            Assert.That(_tracks[0].X, Is.EqualTo(Int32.Parse(seperatedStrings[1])));
            Assert.That(_tracks[0].Y, Is.EqualTo(Int32.Parse(seperatedStrings[2])));
            Assert.That(_tracks[0].Altitude, Is.EqualTo(Int32.Parse(seperatedStrings[3])));
            Assert.That(_tracks[0].TimeStamp, Is.EqualTo(DateTime.ParseExact(seperatedStrings[4], "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture)));
            Assert.That(_tracks[0].Course, Is.EqualTo(0));
            Assert.That(_tracks[0].Velocity, Is.EqualTo(0));
            Assert.That(_tracks[1].Tag, Is.EqualTo(seperatedStrings2[0]));
            Assert.That(_tracks[1].X, Is.EqualTo(Int32.Parse(seperatedStrings2[1])));
            Assert.That(_tracks[1].Y, Is.EqualTo(Int32.Parse(seperatedStrings2[2])));
            Assert.That(_tracks[1].Altitude, Is.EqualTo(Int32.Parse(seperatedStrings2[3])));
            Assert.That(_tracks[1].TimeStamp, Is.EqualTo(DateTime.ParseExact(seperatedStrings2[4], "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture)));
            Assert.That(_tracks[1].Course, Is.EqualTo(0));
            Assert.That(_tracks[1].Velocity, Is.EqualTo(0));
        }

        [Test]
        public void ReadTransponderdata_TransponderdataAddedTwice_NumberOfEventsReceivedIsCorrect()
        {
            List<string> transponderStrings = new List<string>();
            transponderStrings.Add(transponderData);
            var args = new RawTransponderDataEventArgs(transponderStrings);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);
            args.TransponderData.Add(newTransponderData);
            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);

            Assert.That(_nEventsReceived, Is.EqualTo(2));
        }

    }
}