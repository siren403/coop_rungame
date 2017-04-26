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

    public UserData()
    {
        mCoin = new PlayerPrefsInt("UserCoin");
    }
}
