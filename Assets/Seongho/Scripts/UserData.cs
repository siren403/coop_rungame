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

    private PlayerPrefsInt mItem1 = null;
    public int Item1
    {
        get
        {
            return mItem1.Value;
        }
        set
        {
            mItem1.Value = value;
        }
    }

    private PlayerPrefsInt mItem2 = null;
    public int Item2
    {
        get
        {
            return mItem2.Value;
        }
        set
        {
            mItem2.Value = value;
        }
    }

    private PlayerPrefsInt mItem3 = null;
    public int Item3
    {
        get
        {
            return mItem3.Value;
        }
        set
        {
            mItem3.Value = value;
        }
    }

    private PlayerPrefsInt mItem4 = null;
    public int Item4
    {
        get
        {
            return mItem4.Value;
        }
        set
        {
            mItem4.Value = value;
        }
    }

    public UserData()
    {
        mCoin = new PlayerPrefsInt("UserCoin");
        mHeart = new PlayerPrefsInt("UserHeart");
        mExitTime = new PlayerPrefsInt("ExitTime");
        mSpareTime = new PlayerPrefsInt("SpareTime");
        mItem1 = new PlayerPrefsInt("Item1");
        mItem2 = new PlayerPrefsInt("Item2");
        mItem3 = new PlayerPrefsInt("Item3");
        mItem4 = new PlayerPrefsInt("Item4");

    }
}
