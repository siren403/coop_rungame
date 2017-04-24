using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CObstacleObject : CPlacementObject
{
    [SerializeField]
    private bool mIsGameOver = false;
    [SerializeField]
    private int mDamageValue = 100;

    protected override void OnPlayerEnter(CPlayer tPlayer)
    {
        Debug.Log("Obstacle");
        if (mIsGameOver)
        {
            tPlayer.DecrementHp(tPlayer.CurrentHp.Value);
        }
        else
        {
            tPlayer.DecrementHp(mDamageValue);
        }
    }
}
