using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceLoader
{
    public class CTheme2TileLoader : CTrackTileLoader
    {
        protected override TilePaths InitTilePaths()
        {
            TilePaths paths = new TilePaths();

            paths.PathStartTrack = "Prefabs/Thema2/PFStartTrack";
            paths.PathTrackA = "Prefabs/Thema2/PFTrackA";
            paths.PathTrackB = "Prefabs/Thema2/PFTrackB";
            paths.PathTrackC = "Prefabs/Thema2/PFTrackC";
            paths.PathTrackD = "Prefabs/Thema2/PFTrackD";
            paths.PathTrackE = "Prefabs/Thema2/PFTrackE";
            paths.PathTrackF = "Prefabs/Thema2/PFTrackF";
            paths.PathTrackG = "Prefabs/Thema2/PFTrackG";
            paths.PathVerticalTrack = "Prefabs/Thema2/PFVerticalTrack";
            paths.PathEndTrack = "Prefabs/Thema2/PFEndTrack";

            return paths;
        }
    }
}
