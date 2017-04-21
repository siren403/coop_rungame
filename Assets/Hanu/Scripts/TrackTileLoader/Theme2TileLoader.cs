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

            paths.PathVerticalTrack = "Prefabs/Thema2/PFVerticalTrack";
            paths.PathHorizontalTrack = "Prefabs/Thema2/PFHorizontalTrack";

            paths.PathUpLeftTrack = "Prefabs/Thema2/PFUpLeftTrack";
            paths.PathLeftUpTrack = "Prefabs/Thema2/PFLeftUpTrack";
            paths.PathUpRightTrack = "Prefabs/Thema2/PFUpRightTrack";
            paths.PathRightUpTrack = "Prefabs/Thema2/PFRightUpTrack";

            paths.PathEndTrack = "Prefabs/Thema2/PFEndTrack";

            return paths;
        }
    }
}
