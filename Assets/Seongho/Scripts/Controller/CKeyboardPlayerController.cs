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
    }

    public override int GetHorizontal()
    {
        return mHorizontal;
    }
}
