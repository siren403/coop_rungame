using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceLoader
{
    public class CTheme1TileLoader : CTrackTileLoader
    {
        protected override TilePaths InitTilePaths()
        {
            TilePaths paths = new TilePaths();

            paths.PathStartTrack = "Prefabs/Thema1/PFStartTrack";
            paths.PathTrackA = "Prefabs/Thema1/PFTrackA";
            paths.PathTrackB = "Prefabs/Thema1/PFTrackB";
            paths.PathTrackC = "Prefabs/Thema1/PFTrackC";
            paths.PathTrackD = "Prefabs/Thema1/PFTrackD";
            paths.PathTrackE = "Prefabs/Thema1/PFTrackE";
            paths.PathTrackF = "Prefabs/Thema1/PFTrackF";
            paths.PathTrackG = "Prefabs/Thema1/PFTrackG";
            paths.PathVerticalTrack = "Prefabs/Thema1/PFVerticalTrack";   
            paths.PathEndTrack = "Prefabs/Thema1/PFEndTrack";

            return paths;
        }
    }
}
