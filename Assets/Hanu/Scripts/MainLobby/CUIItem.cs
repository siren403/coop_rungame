using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inspector;
using UnityEngine.UI;


public class CUIItem : MonoBehaviour {



    public enum ITEM
    {
        ITEM1 = 0,
        ITEM2 = 1,
        ITEM3 = 2,
        ITEM4 = 3,
    }

    public const int Item1Price = 1000;
    public const int Item2Price = 1000;
    public const int Item3Price = 1000;
    public const int Item4Price = 1000;

    public Dictionary<ITEM, int> ItemPrice = null;
    public int[] ItemList = null;

    public Text[] ItemNumber = null;

    public CSceneMainLobby SceneMainLobby = null;


    private ShowUserData mCoinText = null;
    private UserData mUserData = null;

    private int mCoin = 0;

    private void Awake()
    {
        SceneMainLobby = new CSceneMainLobby();

 
        mUserData = new UserData();
        mCoinText = this.GetComponent<ShowUserData>();
        mCoin = mUserData.Coin;

        ItemPrice = new Dictionary<ITEM, int>();

        ItemPrice.Add(ITEM.ITEM1, Item1Price);
        ItemPrice.Add(ITEM.ITEM2, Item2Price);
        ItemPrice.Add(ITEM.ITEM3, Item3Price);
        ItemPrice.Add(ITEM.ITEM4, Item4Price);

        ItemList = new int[4];
        ItemNumber = new Text[4];




        LoadItem();


    }

    // Use this for initialization
    void Start () {
       

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadItem()
    {
        ItemList[0] = mUserData.Item1;
        ItemList[1] = mUserData.Item2;
        ItemList[2] = mUserData.Item3;
        ItemList[3] = mUserData.Item4;
    }

    public void SaveItem()
    {
        mUserData.Item1 = ItemList[0];
        mUserData.Item2 = ItemList[1];
        mUserData.Item3 = ItemList[2];
        mUserData.Item4 = ItemList[3];
    }

    public void BuyItem(ITEM tItem)
    {
        if(mCoin >= ItemPrice[tItem])
        {
            ItemList[(int)tItem] += 1;
            mCoin -= ItemPrice[tItem];
            mUserData.Coin = mCoin;
            mCoinText.UpdateUserData();
        }
        else
        {
            Debug.Log("돈이 부족함");
        }
    }

    public void OnClickItem_1()
    {
        BuyItem(ITEM.ITEM1);
    }
    public void OnClickItem_2()
    {
        BuyItem(ITEM.ITEM2);
    }
    public void OnClickItem_3()
    {
        BuyItem(ITEM.ITEM3);
    }
    public void OnClickItem_4()
    {
        BuyItem(ITEM.ITEM4);
    }

    [Button]
    public void AddCoin()
    {
        mCoin = mUserData.Coin;
        mCoin += 1000;
        mUserData.Coin = mCoin;
    }
}
