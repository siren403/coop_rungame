using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Inspector;

public class CUIPlayerController : CPlayerController
{
    [ReadOnly]
    public Slider InstSliderHPBar = null;//체력바
    [ReadOnly]
    public Slider InstSliderBoostBar = null;//부스터게이지
    [ReadOnly]
    public Slider InstSliderJoyStick = null;//조이스틱

    private void Awake()
    {
        base.ScreenSlideDistance = 3.0f;
    }

    private void Update()
    {
        if (Time.timeScale == 0)
            return;

        UpdateScreenSlide();
        ResetJoyStick();
    }

    public override int GetHorizontal()
    {
        float value = InstSliderJoyStick.value;

        if (value < 2)
        {
            return -1;
        }
        else if (value > 2)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public void ResetJoyStick()
    {
        if (Input.GetMouseButtonUp(0)
           || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            InstSliderJoyStick.value = 2.0f;
        }
    }

    #region UI Call Back
    public void OnClickBtnJump()
    {
        CallOnJump.SafeInvoke();
    }
    public void OnClickBtnSlide()
    {
        CallOnSlide.SafeInvoke();
    }
    public void OnClickBtnItem_1()
    {
        CallOnItem_1.SafeInvoke();
    }
    public void OnClickBtnItem_2()
    {
        CallOnItem_2.SafeInvoke();
    }

    protected override void UpdateScreenSlide()
    {
        if (Input.touchCount > 0)
        {
            Touch currentTouch = Input.GetTouch(0);

            switch(currentTouch.phase)
            {
                case TouchPhase.Began:
                    base.ScreenSlideBeganPosition = currentTouch.position;
                    break;
                case TouchPhase.Ended:
                    float distance = currentTouch.position.x - base.ScreenSlideBeganPosition.x;

                    if (distance > base.ScreenSlideDistance)
                        CallOnScreenSlide.SafeInvoke(1);
                    else if (distance < -base.ScreenSlideDistance)
                        CallOnScreenSlide.SafeInvoke(-1);

                    break;
            }

        }
    }
    #endregion



}
