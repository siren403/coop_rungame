using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUIPopUpHeart : MonoBehaviour {


    public GameObject[] HeartArray = null;
    private UserData mUserData = null;
    public CUILobby UILobby = null;


    public GameObject CoinIsnull;
    public GameObject CoinBuyComprete;
    //public GameObject mBuyHeart;

    private CSceneMainLobby mSceneMainLobby;

    public const int TOTAL_HEARTCOUNT = 5;
    public const int HAERT_PRICE = 1;

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
                Debug.Log("구입완료!");
                this.CoinBuyComprete.SetActive(true);
            }
            else
            {
                // mSceneMainLobby.CoinIsNull.SetActive(true);
                this.CoinIsnull.SetActive(true);
                Debug.Log("하트거지");
            }
        }
        else
        {
            Debug.Log("Heart is Full");
        }
    }
    public void OnClickComprete()
    {
        this.CoinBuyComprete.SetActive(false);

    }
}
