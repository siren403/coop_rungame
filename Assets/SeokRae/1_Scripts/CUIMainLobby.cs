using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUIMainLobby : MonoBehaviour {

    public delegate void CallBackBtn();

    CallBackBtn OnStarting = null;
    CallBackBtn OnJumping = null;
    CallBackBtn OnSliding = null;
    CallBackBtn OnItem_1 = null;
    CallBackBtn OnItem_2 = null;
    CallBackBtn OnItem_3 = null;
    CallBackBtn OnItem_4 = null;
    CallBackBtn OnCoinShop = null;
    CallBackBtn OnHeartShop = null;
    CallBackBtn OnBackToTitle = null;
    
    



    void Start () {
	}


	void Update () {
		
	}


    public void OnClickStart()
    {
        if(null != OnStarting)
        {
            OnStarting();
        }
       // Debug.Log("???");
    }
    public void SetStart(CallBackBtn tA)
    {
        OnStarting = tA;
    }

    public void OnClickJump()
    {
        if(null != OnJumping)
        {
            OnJumping();
        }
    }
    public void SetJump(CallBackBtn tA)
    {
        OnJumping = tA;
    }

    public void OnClickSlid()
    {
        if(null!=OnSliding)
        {
            OnSliding();
        }
    }
    public void SetSlid(CallBackBtn tA)
    {
        OnSliding = tA;
    }

    public void OnClickItem_1()
    {
        if(null!=OnItem_1)
        {
            OnItem_1();
        }
    }
    public void SetItem_1(CallBackBtn tA)
    {
        OnItem_1 = tA;
    }

    public void OnClickItem_2()
    {
        if(null!=OnItem_2)
        {
            OnItem_2();
        }
    }
    public void SetItem_2(CallBackBtn tA)
    {
        OnItem_2 = tA;
    }
    public void OnClickItem_3()
    {
        if (null != OnItem_3)
        {
            OnItem_3();
        }
    }
    public void SetItem_3(CallBackBtn tA)
    {
        OnItem_3 = tA;
    }
    public void OnClickItem_4()
    {
        if (null != OnItem_4)
        {
            OnItem_4();
        }
    }
    public void SetItem_4(CallBackBtn tA)
    {
        OnItem_4 = tA;
    }




    public void OnClickCoinShop()
    {
        if(null!=OnCoinShop)
        {
            OnCoinShop();
        }
    }
    public void SetCoinShop(CallBackBtn tA)
    {
        OnCoinShop = tA;
    }

    public void OnClickHeartShop()
    {
        if(null!=OnHeartShop)
        {
            OnHeartShop();
        }
    }
    public void SetHeartShop(CallBackBtn tA)
    {
        OnHeartShop = tA;
    }

    public void OnClickBackTitle()
    {
        if(null!=OnBackToTitle)
        {
            OnBackToTitle();
        }
    }
    public void SetBackTitle(CallBackBtn tA)
    {
        OnBackToTitle = tA;
    }
}
