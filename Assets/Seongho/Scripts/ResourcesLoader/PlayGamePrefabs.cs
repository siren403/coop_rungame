using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceLoader
{
    public class PlayGamePrefabs : ResourceLoader
    {
        public CPlayer PFPlayer = null;
        public CTargetCamera PFTargetCamera = null;

        public override void Load()
        {
            PrefabLoad(ref PFPlayer, "PlayGame/PFPlayer");
            PrefabLoad(ref PFTargetCamera, "PlayGame/PFTargetCamera");
        }
    }
}
