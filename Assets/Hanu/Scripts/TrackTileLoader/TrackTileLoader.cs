﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceLoader
{
    public abstract class TrackTileLoader : ResourceLoader
    {
        public struct TilePaths
        {
            public string PathStartTrack;

            public string PathVerticalTrack;
            public string PathHorizontalTrack;

            public string PathUpLeftTrack;
            public string PathLeftUpTrack;
            public string PathUpRightTrack;
            public string PathRightUpTrack;

            public string PathEndTrack;
        }

        private CTrackTile PFStartTrack = null;

        private CTrackTile PFVerticalTrack = null;
        private CTrackTile PFHorizontalTrack = null;

        private CTrackTile PFUpLeftTrack = null;
        private CTrackTile PFLeftUpTrack = null;
        private CTrackTile PFUpRightTrack = null;
        private CTrackTile PFRightUpTrack = null;

        private CTrackTile PFEndTrack = null;

        private Dictionary<CTrackCreater.TRACKKIND, CTrackTile> TrackKind = null;


        protected abstract TilePaths InitTilePaths();

        public override void Load()
        {
            TilePaths paths = InitTilePaths();

            PFStartTrack = Resources.Load<CTrackTile>(paths.PathStartTrack);

            PFVerticalTrack = Resources.Load<CTrackTile>(paths.PathVerticalTrack);
            PFHorizontalTrack = Resources.Load<CTrackTile>(paths.PathHorizontalTrack);

            PFUpLeftTrack = Resources.Load<CTrackTile>(paths.PathUpLeftTrack);
            PFLeftUpTrack = Resources.Load<CTrackTile>(paths.PathLeftUpTrack);
            PFUpRightTrack = Resources.Load<CTrackTile>(paths.PathUpRightTrack);
            PFRightUpTrack = Resources.Load<CTrackTile>(paths.PathRightUpTrack);
    
            PFEndTrack = Resources.Load<CTrackTile>(paths.PathEndTrack);


            TrackKind = new Dictionary<CTrackCreater.TRACKKIND, CTrackTile>();

            TrackKind.Add(CTrackCreater.TRACKKIND.START, PFStartTrack);

            TrackKind.Add(CTrackCreater.TRACKKIND.VERTICAL, PFVerticalTrack);
            TrackKind.Add(CTrackCreater.TRACKKIND.HORIZONTAL, PFHorizontalTrack);

            TrackKind.Add(CTrackCreater.TRACKKIND.UPLEFT, PFUpLeftTrack);
            TrackKind.Add(CTrackCreater.TRACKKIND.LEFTUP, PFLeftUpTrack);
            TrackKind.Add(CTrackCreater.TRACKKIND.UPRIGHT, PFUpRightTrack);
            TrackKind.Add(CTrackCreater.TRACKKIND.RIGHTUP, PFRightUpTrack);
            TrackKind.Add(CTrackCreater.TRACKKIND.END, PFEndTrack);
        }

        public CTrackTile GetPrefab(CTrackCreater.TRACKKIND tTrackKind)
        {
            if (TrackKind.ContainsKey(tTrackKind))
            {
                return TrackKind[tTrackKind];
            }

            return null;
        }

      
    }
}