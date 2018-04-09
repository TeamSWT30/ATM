using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace ATM
{
    public class TrackObjectification : ITracksSource
    {
        public event EventHandler<TracksChangedEventArgs> TracksChanged;
        private ITransponderdataReader _transponderdataReader;
        private List<Track> _tracks = new List<Track>();
        private TrackRender trackRender = new TrackRender();

        public TrackObjectification(ITransponderReceiver transponderReceiver, ITransponderdataReader transponderdataReader)
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
                trackRender.RenderTrack(track);
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
