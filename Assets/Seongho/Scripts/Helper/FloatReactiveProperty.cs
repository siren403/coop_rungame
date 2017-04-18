using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatReactiveProperty
{
    private float mValue = 0;
    public float Value
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

    private System.Action<float> mCallBack = null;

    public void Subscribe(System.Action<float> callBack)
    {
        mCallBack += callBack;
    }
}
