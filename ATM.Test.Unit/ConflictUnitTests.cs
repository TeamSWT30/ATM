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
        private Conflict _uut;
        private Track _track1;
        private Track _track2;
        private Track _track3;
        private Track _track4;
        private Track _track5;
        private List<Track> myTrackList;
       

        private TrackObjectification _trackObjectification;
        private ITransponderdataReader _transponderReader;

        [SetUp]
        public void SetUp()
        {
            _uut = new Conflict();
            _transponderReader = Substitute.For<ITransponderdataReader>();

            //track 1 og 3 krydser hinandens veje med stor højdeforskel
            //track 1 og 2 flyver parallelt i samme højde
            //track 3 og 4 kolliderer pga. højden
            //track 4 og 5 kolliderer pga. distancen

            _track1 = new Track()
            {
                X = 20000,
                Y = 20000,
                Altitude = 12000,
                Velocity = 235,
                Course = 90,

            };
            _track2 = new Track()
            {
                X = 40000,
                Y = 20000,
                Altitude = 13000,
                Velocity = 235,
                Course = 0,
            };
             _track3 = new Track()
            {
                X = 20000,
                Y = 30000,
                Altitude = 12000,
                Velocity = 235,
                Course = 90,
            };
            _track4 = new Track
            {
                X = 40000,
                Y = 50000,
                Altitude = 1000,
                Velocity = 235.70,
                Course = 45,
            };
            _track5 = new Track
            {
                X = 70000,
                Y = 50000,
                Altitude = 1200,
                Velocity = 235.70,
                Course = 135,
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
            _uut.CheckForConflicts(myTrackList);
            List<Track> crashingPlanes = myTrackList;
            Assert.That(crashingPlanes.Contains(_track1) && crashingPlanes.Contains(_track2) && crashingPlanes.Contains(_track3) && crashingPlanes.Contains(_track4) && crashingPlanes.Contains(_track5));
        }


        [Test]
        public void CrashDetection_NoPlanesInCollidingPlanes()
        {
            var track1 = _transponderReader.ReadTrackData("ATR423;20000;20000;12000;20151006213456789");
            var track2 = _transponderReader.ReadTrackData("ATR423; 40000; 20000; 12000; 20151006213456789");

            myTrackList.Add(track1);
            myTrackList.Add(track2);

            List<Track> conflicts = _uut.ListOfConflicts();
            Assert.That(conflicts.Contains(track1) && conflicts.Contains(track2));
        }

        [Test]
        public void NoCrashCourse()
        {
            

        }

        [Test]
        public void NoCrashHeightAndCourse()
        {

        }

        [Test]
        public void Crash()
        {

        }

    }
}

