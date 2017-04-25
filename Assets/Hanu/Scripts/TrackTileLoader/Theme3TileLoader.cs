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

            paths.PathVerticalTrack = "Prefabs/Thema3/PFVerticalTrack";
            paths.PathHorizontalTrack = "Prefabs/Thema3/PFHorizontalTrack";

            paths.PathUpLeftTrack = "Prefabs/Thema3/PFUpLeftTrack";
            paths.PathLeftUpTrack = "Prefabs/Thema3/PFLeftUpTrack";
            paths.PathUpRightTrack = "Prefabs/Thema3/PFUpRightTrack";
            paths.PathRightUpTrack = "Prefabs/Thema3/PFRightUpTrack";

            paths.PathEndTrack = "Prefabs/Thema3/PFEndTrack";

            return paths;
        }
    }
}
