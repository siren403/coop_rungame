using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntReactiveProperty
{
    private int mValue = 0;
    public int Value
    {
        set
        {
            if(mValue != value)
            {
                mCallBack.SafeInvoke(value);
            }
            mValue = value;
        }
        get
        {
            return mValue;
        }
    }


    private System.Action<int> mCallBack = null;


    public void Subscribe(System.Action<int> callBack)
    {
        mCallBack += callBack;
    }
}
