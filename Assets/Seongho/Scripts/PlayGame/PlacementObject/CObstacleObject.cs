using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CObstacleObject : CPlacementObject
{
    protected override void OnPlayerEnter(CPlayer tPlayer)
    {
        Debug.Log("Obstacle");
        tPlayer.DecrementHp(tPlayer.CurrentHp.Value);
    }
}
