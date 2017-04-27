using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceLoader
{
    public class CTheme0TileLoader : CTrackTileLoader
    {
        protected override TilePaths InitTilePaths()
        {
            TilePaths paths = new TilePaths();

            paths.PathStartTrack = "Prefabs/Thema0/PFStartTrack";
            paths.PathTrackA = "Prefabs/Thema0/PFTrackA";
            paths.PathTrackB = "Prefabs/Thema0/PFTrackB";
            paths.PathTrackC = "Prefabs/Thema0/PFTrackC";
            paths.PathTrackD = "Prefabs/Thema0/PFTrackD";
            paths.PathTrackE = "Prefabs/Thema0/PFTrackE";
            paths.PathTrackF = "Prefabs/Thema0/PFTrackF";
            paths.PathTrackG = "Prefabs/Thema0/PFTrackG";
            paths.PathVerticalTrack = "Prefabs/Thema0/PFVerticalTrack";
            paths.PathEndTrack = "Prefabs/Thema0/PFEndTrack";

            return paths;
        }
    }
}
