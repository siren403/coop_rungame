using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class CSceneMainLobby : MonoBehaviour {


    public CUIMainLobby mUIMainLobby;


 
    TimeSpan start = TimeSpan.FromTicks(DateTime.Now.Ticks);

    double SaveTime;
    double NowTime;


    int Count = 0;

    public GameObject[] HeartArray;
    //public Stack heartStack = new Stack();

    
    void Start () {

        mUIMainLobby.SetStart(OnStart);
        mUIMainLobby.SetJump(OnJump);
        mUIMainLobby.SetSlid(OnSlid);
        mUIMainLobby.SetItem_1(OnItem_1);
        mUIMainLobby.SetItem_2(OnItem_2);
        mUIMainLobby.SetCoinShop(OnCoinShopEnter);
        mUIMainLobby.SetHeartShop(OnHeartShopEnter);
        mUIMainLobby.SetBackTitle(OnBackToTitle);


    }

    void Update () {

        Timer();
    }
    
    void OnStart()
    {
        

        SaveTime = (TimeSpan.FromTicks(DateTime.Now.Ticks) - start).TotalSeconds;

        Debug.Log("하트지워지게!!!"+SaveTime);
        if (Count < HeartArray.Length)
        {
            HeartArray[Count].SetActive(false);

            Count++;
        }



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
    }

    void OnItem_2()
    {
        Debug.Log("Item2!!");
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

    void Timer()
    {
        Debug.Log((TimeSpan.FromTicks(DateTime.Now.Ticks) - start).TotalSeconds);
        NowTime = (TimeSpan.FromTicks(DateTime.Now.Ticks) - start).TotalSeconds;
        

    }




}
