using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;
using NUnit.Framework;
using NSubstitute;
using TransponderReceiver;

namespace ATM.Test.Unit
{
    [TestFixture]
    public class ConflictUnitTest
    {
        private ProximityDetection _uut;
        private Track _track1;
        private Track _track2;
        private Track _track3;
        private Track _track4;
        private Track _track5;
        private List<Track> myTrackList;
       

       // private TrackObjectification _trackObjectification;
        private ITransponderdataReader _transponderReader;
        private ITrackUpdate _trackUpdate;

        [SetUp]
        public void SetUp()
        {
            _trackUpdate = Substitute.For<ITrackUpdate>();
            _uut = new ProximityDetection(_trackUpdate);
            _transponderReader = Substitute.For<ITransponderdataReader>();

            //track 1 og 3 krydser hinandens veje med stor højdeforskel
            //track 1 og 2 flyver parallelt i samme højde
            //track 3 og 4 kolliderer pga. højden
            //track 4 og 5 kolliderer pga. distancen

            _track1 = new Track()
            {
                Tag = "ATR123",
                X = 20000,
                Y = 20000,
                Altitude = 12000,
                Velocity = 235,
                Course = 90,
                TimeStamp = DateTime.Now
            };
            _track2 = new Track()
            {
                Tag = "ATR312",
                X = 40000,
                Y = 20000,
                Altitude = 13000,
                Velocity = 235,
                Course = 0,
                TimeStamp = DateTime.Now
            };
             _track3 = new Track()
            {
                Tag = "ATR132",
                 X = 20000,
                Y = 30000,
                Altitude = 12000,
                Velocity = 235,
                Course = 90,
                TimeStamp = DateTime.Now
             };
            _track4 = new Track
            {
                Tag = "ATR321",
                X = 40000,
                Y = 50000,
                Altitude = 1000,
                Velocity = 235.70,
                Course = 45,
                TimeStamp = DateTime.Now
            };
            _track5 = new Track
            {
                Tag = "ATR212",
                X = 70000,
                Y = 50000,
                Altitude = 1200,
                Velocity = 235.70,
                Course = 135,
                TimeStamp = DateTime.Now
            };

            myTrackList = new List<Track>();

            myTrackList.Add(_track1);
            myTrackList.Add(_track2);
            myTrackList.Add(_track3);
            myTrackList.Add(_track4);
            myTrackList.Add(_track5);

        }

       [Test]
        public void AllTracksInList()
        {
            List<Track> crashingPlanes = myTrackList;
            Assert.That(crashingPlanes.Contains(_track1) && crashingPlanes.Contains(_track2) && crashingPlanes.Contains(_track3) && crashingPlanes.Contains(_track4) && crashingPlanes.Contains(_track5));
        }

        //track 1 og 3 krydser hinandens veje med stor højdeforskel
        //track 1 og 2 flyver parallelt i samme højde
        public void Crash()
        {
            Track track1 = _transponderReader.ReadTrackData("ATR423;70000;50000;1200;20151006213456789");
            Track track2 = _transponderReader.ReadTrackData("ATR123;60800;50000;1000;20151006213456789");
        }

        public void CloseToCrash()
        {
            Track track1 = _transponderReader.ReadTrackData("ATR423;70000;50000;1300;20151006213456789");
            Track track2 = _transponderReader.ReadTrackData("ATR123;60500;50000;1000;20151006213456789");
        }

        [Test]
        public void NoCrashHeight()
        {
            Track track1 = _transponderReader.ReadTrackData("ATR423;20000;20000;1500;20151006213456789");
            Track track2 = _transponderReader.ReadTrackData("ATR123;20000;20000;1000;20151006213456789");
           
        }
       
        [Test]
        public void NoCrashCourse()
        {
            Track track1 = _transponderReader.ReadTrackData("ATR423;40000;20000;1500;20151006213456789");
            Track track2 = _transponderReader.ReadTrackData("ATR123;20000;20000;1500;20151006213456789");

        }
    }
}
