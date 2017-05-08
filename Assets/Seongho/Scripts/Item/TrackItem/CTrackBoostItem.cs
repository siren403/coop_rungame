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
        mPlayer.SetBoostSpeedRatio(1.5f);
        mPlayer.instBoost.SetActive(true);
        //mPlayer.ScenePlayGame.ScoreTickRatio = 2.0f;
    }

    public override void Deactivate()
    {
        mPlayer.SetBoostSpeedRatio(1.0f);
        mPlayer.instBoost.SetActive(false);

        //mPlayer.ScenePlayGame.ScoreTickRatio = 1.0f;
    }

}