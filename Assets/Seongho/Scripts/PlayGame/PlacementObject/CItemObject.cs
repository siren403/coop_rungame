using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CItemObject : CPlacementObject
{
    public enum ItemType
    {
        None = 0,
        Boost,
    }
    public ItemType _ItemType;
    protected override void OnPlayerEnter(CPlayer tPlayer)
    {
        switch (_ItemType)
        {
            case ItemType.Boost:
                CTrackBoostItem item = new CTrackBoostItem(tPlayer, 1.5f);
                tPlayer.ScenePlayGame.InstItemTimer.AddTrackItem(item);
                break;
        }
        Destroy(this.gameObject);
    }
}
