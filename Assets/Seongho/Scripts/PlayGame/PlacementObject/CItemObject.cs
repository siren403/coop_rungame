using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CItemObject : CPlacementObject
{
    public enum ItemType
    {
        Boost = 0,
        Shield = 1,
        Magnet = 2,
        Heal = 3,
        FootHoldBoost = 4,
    }
    

    public ItemType _ItemType;

    public float Duration = 0.0f;

    protected override void OnPlayerEnter(CPlayer tPlayer)
    {
        int RandomItem = 0;
        RandomItem = UnityEngine.Random.Range(1, 100);
        Debug.Log("값 : "+ RandomItem.ToString());
        Debug.Log("들어가기전 " + _ItemType.ToString());
        switch (_ItemType)
        {
            case ItemType.FootHoldBoost: case ItemType.Heal:
                break;
            default:
                if (RandomItem <= 40)
                {
                    _ItemType = ItemType.Boost;
                }
                else if (RandomItem <= 70)
                {
                    _ItemType = ItemType.Boost;
                }
                else if( RandomItem <= 100)
                {
                    _ItemType = ItemType.Boost;
                }
                break;
        }
        Debug.Log(_ItemType.ToString());

        Duration = 5.0f;

        switch (_ItemType)
        {
            case ItemType.Boost:
                CTrackBoostItem item = new CTrackBoostItem(tPlayer, Duration);
                tPlayer.ScenePlayGame.InstItemTimer.AddTrackItem(_ItemType,item);
                break;
            case ItemType.Heal:
                tPlayer.SetAddHeal(300);
                break;
        }
        Destroy(this.gameObject);
    }
}
