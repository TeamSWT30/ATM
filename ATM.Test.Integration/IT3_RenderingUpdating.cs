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
    [TestFixture()]
    class IT3_RenderingUpdating
    {
        private Filtering _filtering;
        private Parsing _parsing;
        private Airspace _airspace;
        private Calculating _calculating;
        private Updating _updating;
        private Rendering _uut;
        private IOutput _output;
        private ITransponderReceiver _transponderReceiver;
        private string transponderDataInside;

        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _parsing = new Parsing(_transponderReceiver);
            _airspace = new Airspace();
            _filtering = new Filtering(_airspace, _parsing);
            _calculating = new Calculating();
            _updating = new Updating(_filtering, _calculating);
            _uut = new Rendering(_updating, _output);
            transponderDataInside = "ATR423;39045;12932;14000;20151006213456789";
        }

        [Test]
        public void UpdatingToRendering_RenderTrack_CorrectOutput()
        {
            List<string> transponderStrings = new List<string>();
            transponderStrings.Add(transponderDataInside);
            var args = new RawTransponderDataEventArgs(transponderStrings);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);

            string[] seperatedStrings = transponderDataInside.Split(';');

            _output.Received().OutputLine("Tag: " + seperatedStrings[0] + ", X: " + seperatedStrings[1] + ", Y: " +
                                          seperatedStrings[2] + ", Altitude: " + seperatedStrings[3] + ", Velocity: " +
                                          "0" + ", Course: " + "0");
        }
    }
}
