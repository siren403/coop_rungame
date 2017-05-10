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
        Dash = 4,
    }

    private CItemData mItemdata = null;

    public ItemType _ItemType;

    public float Duration = 0.0f;

    private void Awake()
    {
        mItemdata = new CItemData();
    }


    protected override void OnPlayerEnter(CPlayer tPlayer)
    {
        int RandomItem = 0;
        RandomItem = UnityEngine.Random.Range(1, 100);
        Debug.Log("값 : "+ RandomItem.ToString());
        Debug.Log("들어가기전 " + _ItemType.ToString());
        switch (_ItemType)
        {
            case ItemType.Dash: case ItemType.Heal:
                break;
            default:
                if (RandomItem <= 40)
                {
                    _ItemType = ItemType.Boost;
                }
                else if (RandomItem <= 70)
                {
                    _ItemType = ItemType.Shield;
                }
                else if( RandomItem <= 100)
                {
                    _ItemType = ItemType.Magnet;
                }
                break;
        }
        Debug.Log(_ItemType.ToString());
        tPlayer.ViewGetItemUI(_ItemType);


        switch (_ItemType)
        {
            case ItemType.Boost:
            case ItemType.Dash:
                if (mItemdata.Item3 == 1)
                {
                    Duration = 5.0f + 2.0f;
                    Debug.Log("증가함?");
                }
                else
                {
                    Duration = 5.0f;
                }
                CTrackBoostItem item = new CTrackBoostItem(tPlayer, Duration);
                tPlayer.ScenePlayGame.InstItemTimer.AddTrackItem(_ItemType,item);
                if(_ItemType == ItemType.Boost)
                {
                    
                    Destroy(this.gameObject);
                }
                break;
            case ItemType.Shield:
                if (mItemdata.Item3 == 1)
                {
                    Duration = 10.0f + 2.0f;
                    Debug.Log("증가함?");
                }
                else
                {
                    Duration = 10.0f;
                }
                CShieldItem Shielditem = new CShieldItem(tPlayer, Duration);
                tPlayer.ScenePlayGame.InstItemTimer.AddTrackItem(_ItemType, Shielditem);
                Destroy(this.gameObject);
                break;
            case ItemType.Magnet:
                if (mItemdata.Item3 == 1)
                {
                    Duration = 5.0f + 2.0f;
                    Debug.Log("증가함?");
                }
                else
                {
                    Duration = 5.0f;
                }
                CMagnetItem MagnetItem = new CMagnetItem(tPlayer, Duration);
                tPlayer.ScenePlayGame.InstItemTimer.AddTrackItem(_ItemType, MagnetItem);
                Destroy(this.gameObject);
                break;
            case ItemType.Heal:
                tPlayer.SetAddHeal(1000);
                Destroy(this.gameObject);
                break;

        }
       
        
    }


    protected override void OnPlayerTriggerEnter(CPlayer tPlayer)
    {
        if (tPlayer.IsMagnet == true)
        {
            if(_ItemType != ItemType.Dash)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, tPlayer.transform.position, mMagnetDistanceDelta);
            }
        }
    }
}
