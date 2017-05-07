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

    protected override void OnPlayerTriggerEnter(CPlayer tPlayer)
    {
        if(tPlayer.IsMagnet == true)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, tPlayer.transform.position, mMagnetDistanceDelta);
        } 
    }
}
