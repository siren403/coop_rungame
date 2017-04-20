using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CSceneMainLobby : MonoBehaviour {


    public CUIMainLobby mUIMainLobby;
    


    void Start () {

        
    }
	
	void Update () {
        mUIMainLobby.SetStart(OnStart);
        mUIMainLobby.SetJump(OnJump);
        mUIMainLobby.SetSlid(OnSlid);
        mUIMainLobby.SetItem_1(OnItem_1);
        mUIMainLobby.SetItem_2(OnItem_2);
        mUIMainLobby.SetCoinShop(OnCoinShopEnter);
        mUIMainLobby.SetHeartShop(OnHeartShopEnter);
        mUIMainLobby.SetBackTitle(OnBackToTitle);

    }
    
    void OnStart()
    {
        Debug.Log("하트지워지게!!!");
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
}
