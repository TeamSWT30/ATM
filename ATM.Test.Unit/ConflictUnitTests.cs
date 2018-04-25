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
        private List<Track> myTrackList;
        private Track _track3;
        private Track _track4;

        private TrackObjectification _trackObjectification;

        [SetUp]
        public void SetUp()
        {
            _uut = new Conflict();
            _track1 = new Track();
            _track2 = new Track();
            _track3 = new Track();
            _track4 = new Track();

            
            myTrackList = new List<Track>();

        }

        [Test]
        public void CrashDetection_AllPlanesInCollidingPlanesList()
        {
            // track 1 og 2 kolliderer ikke pga højdeforskellen
            //track 2 og 3 kolliderer ikke pga både højde- og distanceforskellen
            // track 3 og 1 kolliderer ikke, da de flyver parallelt
            //track 4 og 1 kolliderer, da der ikke er nok forskel i distance eller højde

        }
    }
}

