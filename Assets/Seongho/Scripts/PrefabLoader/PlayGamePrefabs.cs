using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrefabLoader
{
    public class PlayGamePrefabs : PrefabLoader
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
