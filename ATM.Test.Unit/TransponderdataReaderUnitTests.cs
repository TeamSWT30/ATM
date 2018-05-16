using System;
using System.Collections.Generic;
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
    public class TransponderdataReaderUnitTests
    {
        private ITransponderdataReader uut;
        private string transponderData;
        private string newTransponderData;
        private ITransponderReceiver _transponderReceiver;
        private Track oldtrack;
        private Track newtrack;
        private List<Track> _updatedTracks;

        [SetUp]
        public void Setup()
        {
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            uut = new TransponderdataReader(_transponderReceiver);
            transponderData = "ATR423;39045;12932;14000;20151006213456789";
            newTransponderData = "ATR423;39245;13132;14000;20151006213457789";
            oldtrack = new Track()
            {
                Tag = "ATR423",
                X = 39045,
                Y = 12932,
                Altitude = 14000,
                TimeStamp = new DateTime(20151006212356789)
            };

            newtrack = new Track()
            {
                Tag = "ATR423",
                X = 39245,
                Y = 13132,
                Altitude = 14000,
                TimeStamp = new DateTime(20151006212357789)
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
        public void AddedToUpdatedTracks()
        {
            List<string> testTracks = new List<string>();
            testTracks.Add(transponderData);
            testTracks.Add(newTransponderData);

            var args = new RawTransponderDataEventArgs(testTracks);
            
            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);

            Assert.That(_updatedTracks.Count, Is.EqualTo(1));
        }

    }
}