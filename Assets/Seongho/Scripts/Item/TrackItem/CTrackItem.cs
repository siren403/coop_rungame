using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CTrackItem : CItem, IEnumerator<float>
{
    private float mDuration = 1.0f;
    public float Duration
    {
        get
        {
            return mDuration;
        }
    }
    public float Current
    {
        get
        {
            return mCurrentTime;
        }
    }
    object IEnumerator.Current
    {
        get
        {
            return mCurrentTime;
        }
    }
    private float mCurrentTime = 0.0f;
    protected CPlayer mPlayer = null;
    private bool mIsDispose = false; 
    public bool IsDispose
    {
        get
        {
            return mIsDispose;
        }
    }

    public CTrackItem(CPlayer tPlayer,float tDuration)
    {
        mPlayer = tPlayer;
        mDuration = tDuration;
    }

    public abstract void Activate();
    public abstract void Deactivate();

    public bool MoveNext()
    {
        mCurrentTime += Time.deltaTime;
        return mCurrentTime < mDuration;
    }

    public void Reset()
    {
        mCurrentTime = 0.0f;
    }

    public void Dispose()
    {
        mCurrentTime = 0.0f;
        mPlayer = null;
        mIsDispose = true;
    }
}
