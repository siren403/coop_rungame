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
        //mPlayer.ScenePlayGame.ScoreTickRatio = 2.0f;
    }

    public override void Deactivate()
    {
        mPlayer.SetShield(false);
        //mPlayer.ScenePlayGame.ScoreTickRatio = 1.0f;
    }
}
