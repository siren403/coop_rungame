using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CItemObject : CPlacementObject
{
    protected override void OnPlayerEnter(CPlayer tPlayer)
    {
        Debug.Log("Item");
        CTrackBoostItem item = new CTrackBoostItem(tPlayer, 1.5f);
        tPlayer.ScenePlayGame.InstItemTimer.AddTrackItem(item);
        Destroy(this.gameObject);
    }
}
