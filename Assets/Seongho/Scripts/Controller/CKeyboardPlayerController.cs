using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CKeyboardPlayerController : CPlayerController
{
    private int mHorizontal = 0;

    public KeyCode mKeyJump = KeyCode.X;
    public KeyCode mKeySlide = KeyCode.X;
    public KeyCode mKeyItem_1 = KeyCode.Z;
    public KeyCode mKeyItem_2 = KeyCode.C;

    private void Awake()
    {
        base.ScreenSlideDistance = 30.0f;
    }

    void Update()
    {
        mHorizontal = (int)Input.GetAxisRaw("Horizontal");
        if(Input.GetKeyDown(mKeyJump))
        {
            CallOnJump.SafeInvoke();
        }
        else if (Input.GetKeyDown(mKeySlide))
        {
            CallOnSlide.SafeInvoke();
        }
        else if (Input.GetKeyDown(mKeyItem_1))
        {
            CallOnItem_1.SafeInvoke();
        }
        else if (Input.GetKeyDown(mKeyItem_2))
        {
            CallOnItem_2.SafeInvoke();
        }
        UpdateScreenSlide();
    }

    public override int GetHorizontal()
    {
        return mHorizontal;
    }

    protected override void UpdateScreenSlide()
    {
        if(Input.GetMouseButtonDown(0))
        {
            base.ScreenSlideBeganPosition = Input.mousePosition;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            float distance = Input.mousePosition.x - base.ScreenSlideBeganPosition.x;

            if (distance > base.ScreenSlideDistance)
                CallOnScreenSlide.SafeInvoke(1);
            else if (distance < -base.ScreenSlideDistance)
                CallOnScreenSlide.SafeInvoke(-1);
        }

    }
}
