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
    public Slider InstSliderTrackProgress = null;
    [SerializeField]
    private Text InstTxtStageNumber = null;

    //PauseUI
    [Space]
    [SerializeField]
    private GameObject InstPanelPause = null;
    [SerializeField]
    private Text InstTxtPauseScore = null;

    //GameOverUI
    [Space]
    public GameObject InstUIGameOver = null;
    public Button InstBtnMoveLobby = null;
    [SerializeField]
    private Text InstTxtGameOverStage = null;
    [SerializeField]
    private Text InstTxtGameOverScore = null;
    [SerializeField]
    private Text InstTxtGameOverCoin = null;

    //Retire - CheckCoin
    [Space]
    public GameObject InstPanelCheckCoin = null;
    public Button InstBtnSubmitRetire = null;


    #region Play UI
    public void SetTxtScore(int value)
    {
        InstTxtScore.text = string.Format("SCORE : {0}", value.ToString());
    }
    public void SetTxtCoin(int value)
    {
        InstTxtCoin.text = string.Format("COIN : {0}", value.ToString());
    }
    public void SetTxtStageNumber(int tNumber)
    {
        InstTxtStageNumber.text = string.Format("STAGE : {0}", tNumber);
    }
    #endregion

    #region Pause
    public void ShowUIPause()
    {
        InstPanelPause.SetActive(true);
    }
    public void SetTxtPauseScore(int score)
    {
        InstTxtPauseScore.text = score.ToString();
    }
    public void HideUIPause()
    {
        InstPanelPause.SetActive(false);
    }
    #endregion
    
    #region Retire - CheckCoin
    public void OnClickBtnCheckCoinOpen()
    {
        InstPanelCheckCoin.SetActive(true);
    }
    public void OnClickBtnCheckCoinClose()
    {
        InstPanelCheckCoin.SetActive(false);
    }
    #endregion

    #region GameOver
    public void ShowUIGameOver(int tStage,int tScore,int tCoin)
    {
        SetTxtGameOverStage(tStage);
        SetTxtGameOverScore(tScore);
        SetTxtGameOverCoin(tCoin);
        InstUIGameOver.SetActive(true);
    }
    public void HideUIGameOver()
    {
        InstUIGameOver.SetActive(false);
    }
    private void SetTxtGameOverStage(int tStage)
    {
        InstTxtGameOverStage.text = string.Format("STAGE : {0}", tStage);
    }
    private void SetTxtGameOverScore(int tScore)
    {
        InstTxtGameOverScore.text = string.Format("SCORE : {0}", tScore);
    }
    private void SetTxtGameOverCoin(int tCoin)
    {
        InstTxtGameOverCoin.text = string.Format("COIN : {0}", tCoin);
    }
    #endregion
}

