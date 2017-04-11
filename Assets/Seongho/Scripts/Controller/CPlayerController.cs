using UnityEngine;
using System;



public abstract class CPlayerController : MonoBehaviour
{
    protected Action CallOnJump = null;
    protected Action CallOnSlide = null;
    protected Action CallOnItem_1 = null;
    protected Action CallOnItem_2 = null;

    public void SetCallOnJump(Action callBack)
    {
        CallOnJump = callBack;
    }
    public void SetCallOnSlide(Action callBack)
    {
        CallOnSlide = callBack;
    }
    public void SetCallOnItem_1(Action callBack)
    {
        CallOnItem_1 = callBack;
    }
    public void SetCallOnItem_2(Action callBack)
    {
        CallOnItem_2 = callBack;
    }

    public abstract int GetHorizontal();
}
