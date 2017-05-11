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
        StartBoost = 5,
        None = 6,
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
        /*if(mItemdata.Item4 == 1)
        {
            _ItemType = ItemType.StartBoost;
        }*/
        Debug.Log("값 : "+ RandomItem.ToString());
        Debug.Log("들어가기전 " + _ItemType.ToString());
        switch (_ItemType)
        {
            case ItemType.Dash: case ItemType.Heal:
            case ItemType.StartBoost:
            case ItemType.None:
                break;
            default:
                if (RandomItem <= 20)
                {
                    _ItemType = ItemType.Boost;
                }
                else if (RandomItem <= 50)
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

                tPlayer.ScenePlayGame.AudioData.GetDashSound();
                if (mItemdata.Item3 == 1)
                {
                    Duration = 3.0f + 2.0f;
                    Debug.Log("증가함?");
                }
                else
                {
                    Duration = 3.0f;
                }
                if(mItemdata.Item4 == 1)
                {
                    Duration = 99999.0f;
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
                    Duration = 4.0f + 2.0f;
                    Debug.Log("증가함?");
                }
                else
                {
                    Duration = 4.0f;
                }
                if (mItemdata.Item4 == 1)
                {
                    Duration = 99999.0f;
                }
                CShieldItem Shielditem = new CShieldItem(tPlayer, Duration);
                tPlayer.ScenePlayGame.InstItemTimer.AddTrackItem(_ItemType, Shielditem);
                Destroy(this.gameObject);

                tPlayer.ScenePlayGame.AudioData.GetItemSound();
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

                tPlayer.ScenePlayGame.AudioData.GetItemSound();
                break;
            case ItemType.Heal:
                tPlayer.SetAddHeal(1000);
                Destroy(this.gameObject);
                break;
            case ItemType.StartBoost:
 
                Duration = 99999.0f;
                CTrackBoostItem StartBoostitem = new CTrackBoostItem(tPlayer, Duration);
                tPlayer.ScenePlayGame.InstItemTimer.AddTrackItem(_ItemType, StartBoostitem);
                CShieldItem StartShielditem = new CShieldItem(tPlayer, Duration);
                tPlayer.ScenePlayGame.InstItemTimer.AddTrackItem(_ItemType, StartShielditem);
                _ItemType = ItemType.None;
                break;
            case ItemType.None:
                Duration = 0.0f;
                mItemdata.Item4 = 0;
                _ItemType = ItemType.Boost;
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
