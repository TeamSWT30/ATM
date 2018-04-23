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
        public List<Track> _tracks = new List<Track>();
        private ITrackRender _trackRender;

        private Airspace _airspace = new Airspace()
        {
            SWCornerY = 10000, SWCornerX = 10000, NECornerX = 90000, NECornerY = 90000, lowerAlt = 500, upperAlt = 20000 
        };

        public TrackObjectification(ITransponderReceiver transponderReceiver, ITransponderdataReader transponderdataReader,ITrackRender trackRender)
        {
            _trackRender = trackRender;
            _transponderdataReader = transponderdataReader;
            
            transponderReceiver.TransponderDataReady += UpdateTrack;
        }

        private void UpdateTrack(object o, RawTransponderDataEventArgs args)
        {
            foreach (var data in args.TransponderData)
            {
                var track = _transponderdataReader.ReadTrackData(data);
                if (track.X >= _airspace.SWCornerX && track.X <= _airspace.NECornerX &&
                    track.Y >= _airspace.SWCornerY && track.Y <= _airspace.NECornerY &&
                    track.Altitude >= _airspace.lowerAlt && track.Altitude <= _airspace.upperAlt)
                {
                    _tracks.Add(track);
                    _trackRender.RenderTrack(track);
                }
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
