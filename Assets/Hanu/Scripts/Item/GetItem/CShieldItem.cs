using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CShieldItem : CTrackItem {


    public CShieldItem(CPlayer tPlayer, float tDuration) : base(tPlayer,tDuration)
    {

    }
    public override void Activate()
    {
        mPlayer.SetShield(true);
    }

    public override void Deactivate()
    {
        mPlayer.SetShield(false);
    }
}
