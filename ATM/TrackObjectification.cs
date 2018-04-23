﻿using System;
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

        private IAirspace _airspace;

        public TrackObjectification(ITransponderReceiver transponderReceiver, ITransponderdataReader transponderdataReader,ITrackRender trackRender, IAirspace airspace)
        {
            _trackRender = trackRender;
            _transponderdataReader = transponderdataReader;
            _airspace = airspace;
            tracks = new List<Track>();

            transponderReceiver.TransponderDataReady += UpdateTrack;
        }

        private void UpdateTrack(object o, RawTransponderDataEventArgs args)
        {
            foreach (var data in args.TransponderData)
            {
                var track = _transponderdataReader.ReadTrackData(data);
                if (_airspace.IsTrackInAirspace(track))
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
