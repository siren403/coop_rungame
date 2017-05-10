using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CItemData : ScriptableObject
{

    [SerializeField]
    private PlayerPrefsInt mItem1 = null;
    public int Item1
    {
        get
        {
            return mItem1.Value;
        }
        set
        {
            mItem1.Value = value;
        }
    }
    [SerializeField]
    private PlayerPrefsInt mItem2 = null;
    public int Item2
    {
        get
        {
            return mItem2.Value;
        }
        set
        {
            mItem2.Value = value;
        }
    }
    [SerializeField]
    private PlayerPrefsInt mItem3 = null;
    public int Item3
    {
        get
        {
            return mItem3.Value;
        }
        set
        {
            mItem3.Value = value;
        }
    }
    [SerializeField]
    private PlayerPrefsInt mItem4 = null;
    public int Item4
    {
        get
        {
            return mItem4.Value;
        }
        set
        {
            mItem4.Value = value;
        }
    }

    public CItemData()
    {
        mItem1 = new PlayerPrefsInt("mItem1");
        mItem2 = new PlayerPrefsInt("mItem2");
        mItem3 = new PlayerPrefsInt("mItem3");
        mItem4 = new PlayerPrefsInt("mItem4");

    }

    public void RsetData()
    {
        Item1 = 0;
        Item2 = 0;
        Item3 = 0;
        Item4 = 0;
    }

}
