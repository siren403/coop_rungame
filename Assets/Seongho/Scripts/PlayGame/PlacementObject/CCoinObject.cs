using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCoinObject : CPlacementObject
{
    protected override void OnPlayerEnter(CPlayer tPlayer)
    {
        tPlayer.ScenePlayGame.OnIncrementCoin();
        Destroy(this.gameObject);
    }
}
