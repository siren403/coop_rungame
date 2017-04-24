using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CUIPlayGame : MonoBehaviour
{

    //PlayUI
    public Slider InstSliderHPBar = null;//체력바
    public Slider InstSliderBoostBar = null;//부스터게이지
    public Slider InstSliderJoyStick = null;//조이스틱
    [SerializeField]
    private Text InstTxtScore = null;
    [SerializeField]
    private Text InstTxtCoin = null;
    public Button InstBtnPause = null;
    public Button InstBtnPauseClose = null;

    //PauseUI
    [SerializeField]
    private GameObject InstPanelPause = null;
    [SerializeField]
    private Text InstTxtPauseScore = null;

    //GameOverUI
    public GameObject InstUIGameOver = null;
    public Button InstBtnRestart = null;


    //Retire - CheckCoin
    public GameObject InstPanelCheckCoin = null;
    public Button InstBtnSubmitRetire = null;


    public void SetTxtScore(int value)
    {
        InstTxtScore.text = string.Format("SCORE : {0}", value.ToString());
    }
    public void SetTxtCoin(int value)
    {
        InstTxtCoin.text = string.Format("COIN : {0}", value.ToString());
    }
    public void ShowUIPause(int score)
    {
        InstPanelPause.SetActive(true);
        InstTxtPauseScore.text = score.ToString();
    }
    public void HideUIPause()
    {
        InstPanelPause.SetActive(false);
    }

    public void OnClickBtnCheckCoinOpen()
    {
        InstPanelCheckCoin.SetActive(true);
    }
    public void OnClickBtnCheckCoinClose()
    {
        InstPanelCheckCoin.SetActive(false);
    }
}

