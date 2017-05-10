using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUIPopUpHeart : MonoBehaviour {


    public GameObject[] HeartArray = null;
    private UserData mUserData = null;
    public CUILobby UILobby = null;

    public const int TOTAL_HEARTCOUNT = 5;
    public const int HAERT_PRICE = 3000;

    private int mHeart = 0;

    public void SetUILobby(CUILobby tUILobby)
    {
        UILobby = tUILobby;
    }

    private void Awake()
    {

        mUserData = UILobby.GetUserData();
        mHeart = UILobby.GetHeart();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
      //  UpdateHeartArray();

    }

    /*
    public void UpdateHeartArray()
    {
        mHeart = UILobby.GetHeart();
        for (int ti = 0; ti < TOTAL_HEARTCOUNT; ti++)
        {
            if (ti < TOTAL_HEARTCOUNT - mHeart)
            {
                HeartArray[ti].SetActive(false);
            }
            else
            {
                HeartArray[ti].SetActive(true);
            }
        }
    }
    */
    public void OnClickBuyHeart()
    {
        if(mHeart != 5)
        {
            if(mUserData.Coin >= HAERT_PRICE)
            {
                mHeart += 1;
                mUserData.Heart = mHeart;
                mUserData.Coin -= HAERT_PRICE;
                UILobby.UIItem.GetCoinText().UpdateUserData();
            }
            else
            {
                //돈없을때
            }
        }
        else
        {
            Debug.Log("Heart is Full");
        }
    }
}
