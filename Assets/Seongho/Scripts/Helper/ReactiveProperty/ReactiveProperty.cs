using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveProperty<T>
{
    private T mValue;
    public virtual T Value
    {
        set
        {
            if (!mValue.Equals(value))
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
    private System.Action<T> mCallBack = null;

    public void Subscribe(System.Action<T> callBack)
    {
        mCallBack += callBack;
    }
}
