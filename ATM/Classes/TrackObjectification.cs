using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace ATM.Classes
{
    public class TrackObjectification : ITracksSource
    {
        public event EventHandler<TracksChangedEventArgs> TracksChanged;
        private TransponderdataReader _transponderdataReader;
        private List<Track> _tracks = new List<Track>();

        public TrackObjectification(ITransponderReceiver transponderReceiver, TransponderdataReader transponderdataReader)
        {
            _transponderdataReader = transponderdataReader;
            transponderReceiver.TransponderDataReady += UpdateTrack;
        }

        private void UpdateTrack(object o, RawTransponderDataEventArgs args)
        {
            foreach (var data in args.TransponderData)
            {
                var track = _transponderdataReader.ReadTrackData(data);
                _tracks.Add(track);
            }
            OnTrackChanged(new TracksChangedEventArgs {Tracks = _tracks});
        }

        private void OnTrackChanged(TracksChangedEventArgs track)
        {
            var handler = TracksChanged;
            handler?.Invoke(this, track);
        }
    }
}
