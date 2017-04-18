using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTestPlayerController : CKeyboardPlayerController
{
    public CPlayer TargetPlayer = null;

    protected new void Awake()
    {
        base.Awake();
        SetCallOnJump(TargetPlayer.DoJump);
        SetCallOnSlide(TargetPlayer.DoSlide);
        SetCallOnItem_1(() => TargetPlayer.SetRotateInput(-1));
        SetCallOnItem_2(() => TargetPlayer.SetRotateInput(1));
        SetCallOnScreenSlide(TargetPlayer.SetRotateInput);
        TargetPlayer.SetFuncHorizontal(GetHorizontal);
    }

    protected new void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Q))
        {
            TargetPlayer.SetMoveStart(true);
        }
    }
}
