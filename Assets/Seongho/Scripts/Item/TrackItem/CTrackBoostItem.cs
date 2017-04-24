using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTrackBoostItem : CTrackItem
{
    public CTrackBoostItem(CPlayer tPlayer, float tDuration) : base(tPlayer, tDuration)
    {

    }

    public override void Activate()
    {
        Debug.Log("Activate");
        mPlayer.SetBoostSpeedRatio(2.0f);
        mPlayer.ScenePlayGame.ScoreTickRatio = 2.0f;
    }

    public override void Deactivate()
    {
        Debug.Log("Deactivate");
        mPlayer.SetBoostSpeedRatio(1.0f);
        mPlayer.ScenePlayGame.ScoreTickRatio = 1.0f;
    }

}