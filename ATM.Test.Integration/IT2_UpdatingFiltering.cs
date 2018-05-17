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
    class IT2_UpdatingFiltering
    {
        private Filtering _filtering;
        private Parsing _parsing;
        private Airspace _airspace;
        private Calculating _calculating;
        private Updating _uut;
        private ITransponderReceiver _transponderReceiver;
        private List<Track> _updatedTracks;
        private string transponderDataInside;
        private string transponderDataOutside;

        [SetUp]
        public void SetUp()
        {
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _parsing = new Parsing(_transponderReceiver);
            _airspace = new Airspace();
            _filtering = new Filtering(_airspace, _parsing);
            _calculating = new Calculating();
            _uut = new Updating(_filtering, _calculating);
            transponderDataInside = "ATR423;39045;12932;14000;20151006213456789";
            transponderDataOutside = "ATR424;95000;95000;25000;20151006213457789";

            _uut.TracksUpdated += (o, args) =>
            {
                _updatedTracks = args.UpdatedTracks;
            };
        }

        [Test]
        public void filteringToUpdating_NoTracksInUpdatedTracks_AddedToUpdatedTracks()
        {
            List<string> transponderStrings = new List<string>();
            transponderStrings.Add(transponderDataInside);
            var args = new RawTransponderDataEventArgs(transponderStrings);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);

            string[] seperatedStrings = transponderDataInside.Split(';');

            Assert.That(_updatedTracks[0].Tag, Is.EqualTo(seperatedStrings[0]));
        }

        [Test]
        public void UpdateTrack_OneTrackInUpdatedTracksTrackChanged_TrackUpdatedInUpdatedTracks()
        {
            List<string> transponderStrings = new List<string>();
            transponderStrings.Add(transponderDataInside);
            var args = new RawTransponderDataEventArgs(transponderStrings);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);
            transponderDataInside = "ATR423;52000;12932;14000;20151006213456789";
            args.TransponderData.Add(transponderDataInside);
            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);

            Assert.That(_updatedTracks[0].X, Is.EqualTo(52000));
        }
    }
}
