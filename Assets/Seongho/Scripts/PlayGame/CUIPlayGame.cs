using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CUIPlayGame : MonoBehaviour
{
    public GameObject InstUIGameOver = null;

    public Slider InstSliderHPBar = null;//체력바
    public Slider InstSliderBoostBar = null;//부스터게이지
    public Slider InstSliderJoyStick = null;//조이스틱

    public Button InstBtnRestart = null;

    public Text InstTxtScore = null;

    public void SetTxtScore(int value)
    {
        InstTxtScore.text = string.Format("SCORE : {0}", value.ToString());
    }
}

