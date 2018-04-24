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
                foreach (var oldTrack in e.Tracks)
                {
                    var newTrack = Tracks.Find(i => i.Tag == oldTrack.Tag);
                    if (_airspace.IsTrackInAirspace(oldTrack) && newTrack == null)
                    {
                        Tracks.Add(oldTrack);
                    }

                    else if (!_airspace.IsTrackInAirspace(oldTrack) && newTrack != null)
                    {
                        Tracks.Remove(Tracks[Tracks.IndexOf(newTrack)]);
                    }

                    else
                    {
                        oldTrack.Course = _calc.CalculateCourse(oldTrack, Tracks[Tracks.IndexOf(newTrack)]);
                        oldTrack.Velocity = _calc.CalculateVelocity(oldTrack, Tracks[Tracks.IndexOf(newTrack)]);
                        Tracks[Tracks.IndexOf(newTrack)] = oldTrack;
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
