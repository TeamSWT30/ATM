using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;
using ATM;

namespace ATM.Test.Unit
{
    [TestFixture]
    public class TransponderdataReaderUnitTests
    {
        private TransponderdataReader uut;
        private string transponderData;

        [SetUp]
        public void Setup()
        {
            uut = new TransponderdataReader();
            transponderData = "ATR423;39045;12932;14000;20151006213456789";
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
        public void ReadTrackData_CorrectTimeStamp()
        {
            Assert.That(uut.ReadTrackData(transponderData).TimeStamp, Is.EqualTo(new DateTime(2015, 10, 06, 21, 34, 56,789)));
        }
    }
}