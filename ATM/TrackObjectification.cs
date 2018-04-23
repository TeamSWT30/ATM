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
        private List<Track> tracks;
        private ITrackRender _trackRender;

        private Airspace _airspace = new Airspace()
        {
            SWCornerY = 10000, SWCornerX = 10000, NECornerX = 90000, NECornerY = 90000, lowerAlt = 500, upperAlt = 20000 
        };

        public TrackObjectification(ITransponderReceiver transponderReceiver, ITransponderdataReader transponderdataReader,ITrackRender trackRender)
        {
            _trackRender = trackRender;
            _transponderdataReader = transponderdataReader;
            tracks = new List<Track>();

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
                    tracks.Add(track);
                    _trackRender.RenderTrack(track);
                }
            }
            if (tracks.Count != 0)
            {
                OnTrackChanged(new TracksChangedEventArgs {Tracks =tracks});
            }
        }

        private void OnTrackChanged(TracksChangedEventArgs track)
        {
            var handler = TracksChanged;
            handler?.Invoke(this, track);
        }
    }
}
