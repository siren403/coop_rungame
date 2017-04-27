using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
public class CSceneMainLobby : MonoBehaviour {


    public CUIMainLobby mUIMainLobby;

    
    int mHeartCount = 0;
    //public float m_TotalSecTime = 0;
    public Text m_Timecontrol;

    public Text ItemUseTxt;
    public Text ItemBuyCoin;
    
    public GameObject[] HeartArray;
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
        
        /*
        SaveTime = (TimeSpan.FromTicks(DateTime.Now.Ticks) - start).TotalSeconds;

        Debug.Log("하트지워지게!!!"+SaveTime);

        PlayerPrefs.SetFloat("SaveTime", (float)SaveTime);
        
        if (Count < HeartArray.Length)
        {
            HeartArray[Count].SetActive(false);
            Count++;
        }*/
        
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
        ItemUseTxt.text = "Item1\n\n\n아이템1임\n\n(설명하시오)";
        ItemBuyCoin.text = "Coin : 250";
    }
    void OnItem_2()
    {
        Debug.Log("Item2!!");
        ItemUseTxt.text = "Item1\n\n\n아이템2임\n\n(설명하시오)";
        ItemBuyCoin.text = "Coin : 300";
    }
    void OnItem_3()
    {
        Debug.Log("Item3!!");
        ItemUseTxt.text = "Item1\n\n\n아이템3임\n\n(설명하시오)";
        ItemBuyCoin.text = "Coin : 350";
    }
    void OnItem_4()
    {
        Debug.Log("Item4!!");
        ItemUseTxt.text = "Item1\n\n\n아이템4임\n\n(설명하시오)";
        ItemBuyCoin.text = "Coin : 400";
    }

    void OnCoinShopEnter()
    {
        SceneManager.LoadScene("CoinShop");
    }

    void OnHeartShopEnter()
    {
        SceneManager.LoadScene("HeartShop");
    }

    void OnBackToTitle()
    {
        SceneManager.LoadScene("MainTitleScene");
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
    


