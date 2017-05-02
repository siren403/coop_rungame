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
    public bool[] IsApplyItem = null;

    public CSceneMainLobby SceneMainLobby = null;

    private ITEM mSelectItem = ITEM.ITEM1;

    private ShowUserData mCoinText = null;
    private UserData mUserData = null;

    private int mCoin = 0;

    public CUILobby UILobby = null;


    private void Awake()
    {

        UILobby.SetUIItem(this);

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

        for(int ti = 0; ti <4; ti++)
        {
            ItemNumber[ti] = SceneMainLobby.ItemArrayTxt[ti];
        }
        IsApplyItem = new bool[4];
        for(int ti = 0; ti< 4; ti++)
        {
            IsApplyItem[ti] = false;
        }


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
        ItemNumber[0].text = ItemList[0].ToString();

        ItemList[1] = mUserData.Item2;
        ItemNumber[1].text = ItemList[1].ToString();

        ItemList[2] = mUserData.Item3;
        ItemNumber[2].text = ItemList[2].ToString();

        ItemList[3] = mUserData.Item4;
        ItemNumber[3].text = ItemList[3].ToString();


    }
    [Button]
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
            ItemNumber[(int)tItem].text = ItemList[(int)tItem].ToString();
        }
        else
        {
            Debug.Log("돈이 부족함");
        }
    }

    public void OnClickItem_1()
    {
        SetSelectItem(ITEM.ITEM1);
    }
    public void OnClickItem_2()
    {
        SetSelectItem(ITEM.ITEM2);
    }
    public void OnClickItem_3()
    {
        SetSelectItem(ITEM.ITEM3);
    }
    public void OnClickItem_4()
    {
        SetSelectItem(ITEM.ITEM4);
    }

    public void OnClickBuyItem()
    {
        BuyItem(GetSelectItem());
        SaveItem();
    }

    [Button]
    public void AddCoin()
    {
        mCoin = mUserData.Coin;
        mCoin += 1000;
        mUserData.Coin = mCoin;
    }

    [Button]
    public void ApplyItem()
    {
        for (int ti = 0; ti < ItemList.Length; ti++)
        {
            if (0 < ItemList[ti])
            {
                IsApplyItem[ti] = true;
                ItemList[ti] -= 1;
                ItemNumber[ti].text = ItemList[ti].ToString();
            }
            else
            {
                IsApplyItem[ti] = false;
            }
        }
        for (int ti = 0; ti < IsApplyItem.Length; ti++)
        {
            Debug.Log(IsApplyItem[ti].ToString());
        }
        SaveItem();
    }


    public void SetSelectItem(ITEM tItem)
    {
        mSelectItem = tItem;
    }

    public ITEM GetSelectItem()
    {
        return mSelectItem;
    }

    public ShowUserData GetCoinText()
    {
        return mCoinText;
    }


}
