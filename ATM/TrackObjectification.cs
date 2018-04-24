using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;
using ATM.Interfaces;

namespace ATM
{
    public class TrackObjectification
    {
        private List<Track> Tracks;
        private IAirspace _airspace;
        private ICalcVelocityCourse _calc;
        private IConflict _conflict;
        private ITrackRender _render;
        private ITransponderdataReader _transponderdataReader;

        public TrackObjectification(IAirspace airspace, ICalcVelocityCourse calc, IConflict conflict, ITrackRender render, ITransponderdataReader transponderdataReader)
        {
            Tracks = new List<Track>();
            _airspace = airspace;
            _calc = calc;
            _conflict = conflict;
            _render = render;
            _transponderdataReader = transponderdataReader;

            _transponderdataReader.TracksChanged += OnTracksChanged;
        }

        private void OnTracksChanged(Object o, TracksChangedEventArgs e)
        {
            if (e.Tracks.Count != 0 && Tracks.Count == 0)
            {
                foreach (var track in e.Tracks)
                {
                    if (_airspace.IsTrackInAirspace(track))
                    {
                        Tracks.Add(track);
                    }
                }
            }

            else if (e.Tracks.Count != 0 && Tracks.Count != 0)
            {
                foreach (var newTrack in e.Tracks)
                {
                    var oldTrack = Tracks.Find(i => i.Tag == newTrack.Tag);
                    if (_airspace.IsTrackInAirspace(newTrack) && oldTrack == null)
                    {
                        Tracks.Add(newTrack);
                    }

                    else if (!_airspace.IsTrackInAirspace(newTrack) && oldTrack != null)
                    {
                        Tracks.Remove(Tracks[Tracks.IndexOf(oldTrack)]);
                    }

                    else if(_airspace.IsTrackInAirspace(newTrack) && oldTrack != null)
                    {
                        newTrack.Course = _calc.CalculateCourse(Tracks[Tracks.IndexOf(oldTrack)], newTrack);
                        newTrack.Velocity = _calc.CalculateVelocity(Tracks[Tracks.IndexOf(oldTrack)], newTrack);
                        Tracks[Tracks.IndexOf(oldTrack)] = newTrack;
                    }
                }
            }
            Console.Clear();
            foreach (var track in Tracks)
            {
                _render.RenderTrack(track);
            }
            _conflict.CheckForConflicts(Tracks);
        }































        //public event EventHandler<TracksChangedEventArgs> TracksChanged;
        //private ITransponderdataReader _transponderdataReader;
        //private List<Track> tracks;
        //private ITrackRender _trackRender;

        //private IAirspace _airspace;

        //public TrackObjectification(ITransponderReceiver transponderReceiver, ITransponderdataReader transponderdataReader,ITrackRender trackRender, IAirspace airspace)
        //{
        //    _trackRender = trackRender;
        //    _transponderdataReader = transponderdataReader;
        //    _airspace = airspace;
        //    tracks = new List<Track>();

        //    transponderReceiver.TransponderDataReady += UpdateTrack;
        //}

        //private void UpdateTrack(object o, RawTransponderDataEventArgs args)
        //{
        //    foreach (var data in args.TransponderData)
        //    {
        //        var track = _transponderdataReader.ReadTrackData(data);
        //        if (_airspace.IsTrackInAirspace(track))
        //        {
        //            tracks.Add(track);
        //            _trackRender.RenderTrack(track);
        //        }
        //    }
        //    if (tracks.Count != 0)
        //    {
        //        var handler = TracksChanged;
        //        handler?.Invoke(this, new TracksChangedEventArgs(tracks));
        //    }
        //}

        //private void OnTrackChanged(TracksChangedEventArgs track)
        //{
        //    var handler = TracksChanged;
        //    handler?.Invoke(this, track);
        //}
    }
}
