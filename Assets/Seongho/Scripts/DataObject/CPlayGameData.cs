using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayGameData : ScriptableObject
{
    [SerializeField]
    private float mHpTickTime = 0.5f;
    [SerializeField]
    private int mHpTickPerHp = 10;
    [SerializeField]
    private float mScoreTickTime = 0.01f;
    [SerializeField]
    private float mCoinPerScore = 20.0f;
    [SerializeField]
    private float mCoinPerBoost = 1.0f;

    public float HpTickTime { get { return mHpTickTime; } }
    public int HpTickPerHp { get { return mHpTickPerHp; } }
    public float ScoreTickTime { get { return mScoreTickTime; } }
    public float CoinPerScore { get { return mCoinPerScore; } }
    public float CoinPerBoost { get { return mCoinPerBoost; } }


    [SerializeField]
    private float mOutTrackSpeedRatio = 0.5f;
    public float OutTrackSpeedRatio { get { return mOutTrackSpeedRatio; } }
    [SerializeField]
    private float mOutTrackHpDecrementRatio = 35.0f;
    public float OutTrackHpDecrementRatio { get { return mOutTrackHpDecrementRatio; } }

    [SerializeField]
    private int mTheme1EffectCount = 15;
    public int Theme1EffectCount
    {
        get
        {
            return mTheme1EffectCount;
        }
    }
    [SerializeField]
    private float mTheme1EffectDelay = 1.5f;
    public float Theme1EffectDelay
    {
        get
        {
            return mTheme1EffectDelay;
        }
    }

    [SerializeField]
    private float mTheme2NotInputDuration = 3.0f;
    public float Theme2NotInputDuration
    {
        get
        {
            return mTheme2NotInputDuration;
        }
    }
    [SerializeField]
    private float mTheme2EffectSpeedRatio = 0.5f;
    public float Theme2EffectSpeedRatio
    {
        get
        {
            return mTheme2EffectSpeedRatio;
        }
    }

}
