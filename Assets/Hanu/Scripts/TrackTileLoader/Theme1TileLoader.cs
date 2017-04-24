using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceLoader
{
    public class Theme1TileLoader : TrackTileLoader
    {
        protected override TilePaths InitTilePaths()
        {
            TilePaths paths = new TilePaths();

            paths.PathStartTrack = "Prefabs/Thema0/PFStartTrack";

            paths.PathVerticalTrack = "Prefabs/Thema0/PFVerticalTrack";
            paths.PathHorizontalTrack = "Prefabs/Thema0/PFHorizontalTrack";

            paths.PathUpLeftTrack = "Prefabs/Thema0/PFUpLeftTrack";
            paths.PathLeftUpTrack = "Prefabs/Thema0/PFLeftUpTrack";
            paths.PathUpRightTrack = "Prefabs/Thema0/PFUpRightTrack";
            paths.PathRightUpTrack = "Prefabs/Thema0/PFRightUpTrack";

            paths.PathEndTrack = "Prefabs/Thema0/PFEndTrack";

            return paths;
        }
    }
}
