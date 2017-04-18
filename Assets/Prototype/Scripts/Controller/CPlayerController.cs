using UnityEngine;
using System;

public abstract class CPlayerController : MonoBehaviour
{
    protected Action CallOnJump = null;
    protected Action CallOnSlide = null;
    protected Action CallOnItem_1 = null;
    protected Action CallOnItem_2 = null;
    protected Action<int> CallOnScreenSlide = null;

    protected Vector3 ScreenSlideBeganPosition;

    protected float ScreenSlideDistance = 5;

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
    public void SetCallOnScreenSlide(Action<int> callBack)
    {
        CallOnScreenSlide = callBack;
    }

    public abstract int GetHorizontal();

    protected abstract  void UpdateScreenSlide();
}
