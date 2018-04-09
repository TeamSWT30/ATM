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
        private int _nEventsReceived;

        [SetUp]
        public void SetUp()
        {

            _TpDataReader = Substitute.For<ITransponderdataReader>();
            _TpDataReceiver = Substitute.For<ITransponderReceiver>();
            _uut = new TrackObjectification(_TpDataReceiver,_TpDataReader);

            _uut.TracksChanged += (o, args) => { ++_nEventsReceived; };
        }

        //[Test]

        //public void Initial_NumberChangedOnce_SumIsCorrect()
        //{
        //    var args = new NumberChangedEventArgs() { Number = 4 };

        //    _numberSource.NumberChanged += Raise.EventWith(args);

        //    Assert.That(_sum, Is.EqualTo(4));
        //}

    }
}
