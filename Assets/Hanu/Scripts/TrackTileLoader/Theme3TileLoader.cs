using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceLoader
{
    public class CTheme3TileLoader : CTrackTileLoader
    {
        protected override TilePaths InitTilePaths()
        {
            TilePaths paths = new TilePaths();

            paths.PathStartTrack = "Prefabs/Thema3/PFStartTrack";
            paths.PathTrackA = "Prefabs/Thema3/PFTrackA";
            paths.PathTrackB = "Prefabs/Thema3/PFTrackB";
            paths.PathTrackC = "Prefabs/Thema3/PFTrackC";
            paths.PathTrackD = "Prefabs/Thema3/PFTrackD";
            paths.PathTrackE = "Prefabs/Thema3/PFTrackE";
            paths.PathTrackF = "Prefabs/Thema3/PFTrackF";
            paths.PathTrackG = "Prefabs/Thema3/PFTrackG";
            paths.PathVerticalTrack = "Prefabs/Thema3/PFVerticalTrack";
            paths.PathEndTrack = "Prefabs/Thema3/PFEndTrack";

            return paths;
        }
    }
}
