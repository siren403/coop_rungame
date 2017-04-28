using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UserData
{
    private PlayerPrefsInt mCoin = null;
    public int Coin
    {
        get
        {
            return mCoin.Value;
        }
        set
        {
            mCoin.Value = value;
        }
    }

    private PlayerPrefsInt mHeart = null;
    public int Heart
    {
        get
        {
            return mHeart.Value;
        }
        set
        {
            mHeart.Value = value;
        }
    }

    private PlayerPrefsInt mExitTime = null;
    public int ExitTime
    {
        get
        {
            return mExitTime.Value;
        }
        set
        {
            mExitTime.Value = value;
        }
    }

    private PlayerPrefsInt mSpareTime = null;
    public int SpareTime
    {
        get
        {
            return mSpareTime.Value;
        }
        set
        {
            mSpareTime.Value = value;
        }
    }

    public UserData()
    {
        mCoin = new PlayerPrefsInt("UserCoin");
        mHeart = new PlayerPrefsInt("UserHeart");
        mExitTime = new PlayerPrefsInt("ExitTime");
        mSpareTime = new PlayerPrefsInt("SpareTime");
    }
}
