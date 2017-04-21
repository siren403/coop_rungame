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

            paths.PathVerticalTrack = "Prefabs/Thema1/PFVerticalTrack";
            paths.PathHorizontalTrack = "Prefabs/Thema1/PFHorizontalTrack";

            paths.PathUpLeftTrack = "Prefabs/Thema1/PFUpLeftTrack";
            paths.PathLeftUpTrack = "Prefabs/Thema1/PFLeftUpTrack";
            paths.PathUpRightTrack = "Prefabs/Thema1/PFUpRightTrack";
            paths.PathRightUpTrack = "Prefabs/Thema1/PFRightUpTrack";

            paths.PathEndTrack = "Prefabs/Thema1/PFEndTrack";

            return paths;
        }
    }
}
