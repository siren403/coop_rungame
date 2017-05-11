using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using DG.Tweening;
public class CSceneMainLobby : MonoBehaviour {


    public CUIMainLobby mUIMainLobby;
    int mHeartCount = 0;

    public GameObject HeartIsNull;
    public GameObject CoinIsNull;

    public Text m_Timecontrol;
    public Text ItemUseTxt;
    public Text ItemBuyCoin;
    
    public GameObject[] HeartArray;
    public Text[] ItemArrayTxt;
    public GameObject CoinShopEnter;
    public GameObject CoinShopBackPanel;
    public GameObject HeartShopEnter;
    public GameObject HeartShopBackPanel;

    public Image mFade;

    //public Stack heartStack = new Stack();


    void Start () {

        mUIMainLobby.SetStart(OnStart);
        mUIMainLobby.SetJump(OnJump);
        mUIMainLobby.SetSlid(OnSlid);
        mUIMainLobby.SetItem_1(OnItem_1);
        mUIMainLobby.SetItem_2(OnItem_2);
        mUIMainLobby.SetItem_3(OnItem_3);
        mUIMainLobby.SetItem_4(OnItem_4);
        mUIMainLobby.SetCoinShop(OnCoinShopEnter);
        mUIMainLobby.SetHeartShop(OnHeartShopEnter);
        mUIMainLobby.SetBackTitle(OnBackToTitle);


    }

    void Update () {
        //Timer();

       // OnTimeCheck();
    }
    
    public void OnStart()
    {
        mFade.gameObject.SetActive(true);
        DOTween.To(() => mFade.color, (color) => mFade.color = color, new Color(1, 1, 1, 1), 2.0f)
        .OnComplete(() => { SceneManager.LoadScene("TestScene");});  
    }

    void OnJump()
    {
        Debug.Log("Jump!");
    }

    void OnSlid()
    {
        Debug.Log("Slid!");
    }

    void OnItem_1()
    {
        Debug.Log("Item1!!");
        ItemUseTxt.text = "추가 체력\n게임 시작 시 일정량의 추가 체력을 얻습니다.";
        ItemBuyCoin.text = "   : 10";
    }
    void OnItem_2()
    {
        Debug.Log("Item2!!");
        ItemUseTxt.text = "획득 코인 2배\n게임 내에서 획득하는 코인의 가치가 2배로 증가합니다.";
        ItemBuyCoin.text = "   : 10";
    }
    void OnItem_3()
    {
        Debug.Log("Item3!!");
        ItemUseTxt.text = "스타트 부스터\n게임시작 부분부터 5초동안 아주 빠른 속도로 이동하게 됩니다.";
        ItemBuyCoin.text = "   : 10";
    }
    void OnItem_4()
    {
        Debug.Log("Item4!!");
        ItemUseTxt.text = "아이템 시간 연장\n게임 내에서 획득한 아이템의 지속시간을 2초 증가 시켜줍니다.";
        ItemBuyCoin.text = "   : 10";
    }

    public void OnCoinShopEnter()
    {
        CoinShopEnter.SetActive(true);
        CoinShopBackPanel.SetActive(true);
        CoinIsNull.SetActive(false);
    }

    public void OnHeartShopEnter()
    {
        HeartShopEnter.SetActive(true);
        HeartShopBackPanel.SetActive(true);

        HeartIsNull.SetActive(false);
    }

    void OnBackToTitle()
    {
        CoinShopEnter.SetActive(false);
        HeartShopEnter.SetActive(false);
        HeartShopBackPanel.SetActive(false);
        CoinShopBackPanel.SetActive(false);
    }
    //===================================
    public void CloseHeartIsNull()
    {
        HeartIsNull.SetActive(false);
        CoinIsNull.SetActive(false);
    }
    
    /*
    public double Timer()
    {
        //Debug.Log((TimeSpan.FromTicks(DateTime.Now.Ticks) - start).TotalSeconds);
        
        return NowTime;
    }
    */
    /*
    void OnTimeCheck()
    {

            mHeartCount++;
        
       

        if (mHeartCount < 5)
            {
                switch (mHeartCount)
                {
                    case 0:
                        HeartArray[0].gameObject.SetActive(false);
                        HeartArray[1].gameObject.SetActive(false);
                        HeartArray[2].gameObject.SetActive(false);
                        HeartArray[3].gameObject.SetActive(false);
                        HeartArray[4].gameObject.SetActive(false);
                        break;
                    case 1:
                        HeartArray[0].gameObject.SetActive(true);
                        HeartArray[1].gameObject.SetActive(false);
                        HeartArray[2].gameObject.SetActive(false);
                        HeartArray[3].gameObject.SetActive(false);
                        HeartArray[4].gameObject.SetActive(false);
                        break;
                    case 2:
                        HeartArray[1].gameObject.SetActive(true);
                        HeartArray[2].gameObject.SetActive(false);
                        HeartArray[3].gameObject.SetActive(false);
                        HeartArray[4].gameObject.SetActive(false);
                        break;
                    case 3:
                        HeartArray[2].gameObject.SetActive(true);
                        HeartArray[3].gameObject.SetActive(false);
                        HeartArray[4].gameObject.SetActive(false);
                        break;
                    case 4:
                        HeartArray[3].gameObject.SetActive(true);
                        HeartArray[4].gameObject.SetActive(false);
                        break;
                }
            }
            else
            {
                HeartArray[4].gameObject.SetActive(true);
            }
        }
        */
    }
    


