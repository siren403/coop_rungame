using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inspector;

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

    public CSceneMainLobby SceneMainLobby = null;



    private UserData mUserData = null;

    private int mCoin = 0;

    private void Awake()
    {
        SceneMainLobby = new CSceneMainLobby();
        SceneMainLobby.mUIMainLobby = new CUIMainLobby();
        mUserData = new UserData();
       
        mCoin = mUserData.Coin;

        ItemPrice = new Dictionary<ITEM, int>();

        ItemPrice.Add(ITEM.ITEM1, Item1Price);
        ItemPrice.Add(ITEM.ITEM2, Item2Price);
        ItemPrice.Add(ITEM.ITEM3, Item3Price);
        ItemPrice.Add(ITEM.ITEM4, Item4Price);

        ItemList = new int[4];
        LoadItem();

        SceneMainLobby.mUIMainLobby.SetItem_1(OnItem_1);

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

    [Button]
    public void BuyItem(ITEM tItem)
    {
        if(mCoin >= ItemPrice[tItem])
        {
            ItemList[(int)tItem] += 1;
        }
        else
        {
            Debug.Log("돈이 부족함");
        }
    }

    public void OnItem_1()
    {
        Debug.Log("내꺼인데 됨?");
    }



}
