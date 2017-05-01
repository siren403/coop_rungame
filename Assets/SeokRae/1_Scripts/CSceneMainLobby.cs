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



    public Text m_Timecontrol;
    public Text ItemUseTxt;
    public Text ItemBuyCoin;
    
    public GameObject[] HeartArray;
    public Text[] ItemArrayTxt;
    public GameObject CoinShopEnter;
    public GameObject HeartShopEnter;

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
        DOTween.To(() => { return mFade.color; }, (color) => mFade.color = color, new Color(0, 0, 0, 1), 0.2f)
            .OnComplete(()=> 
            {
                SceneManager.LoadScene("TestScene");
            });
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
        ItemUseTxt.text = "추가 체력\n\n\n15%추가 체력\n\n(설명하시오)";
        ItemBuyCoin.text = "Coin : 1000";
    }
    void OnItem_2()
    {
        Debug.Log("Item2!!");
        ItemUseTxt.text = "아이템시간연장\n\n\n아이템 효과 유지시간이 2초증가\n\n(설명하시오)";
        ItemBuyCoin.text = "Coin : 1000";
    }
    void OnItem_3()
    {
        Debug.Log("Item3!!");
        ItemUseTxt.text = "코인2배\n\n\n코인이두배가되 두두두두배두\n\n(설명하시오)";
        ItemBuyCoin.text = "Coin : 1000";
    }
    void OnItem_4()
    {
        Debug.Log("Item4!!");
        ItemUseTxt.text = "1스테이지 부스터\n\n\n1스테이지Skip\n\n(설명하시오)";
        ItemBuyCoin.text = "Coin : 1000";
    }

    void OnCoinShopEnter()
    {
        CoinShopEnter.SetActive(true);
    }

    void OnHeartShopEnter()
    {
        HeartShopEnter.SetActive(true);
    }

    void OnBackToTitle()
    {
        CoinShopEnter.SetActive(false);
        HeartShopEnter.SetActive(false);
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
    


