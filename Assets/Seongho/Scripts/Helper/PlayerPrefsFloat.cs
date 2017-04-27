using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 

public class PlayerPrefsFloat
{

    public float Value
    {
        get
        {
            return PlayerPrefs.GetFloat(mKey, 0.0f);
        }
        set
        {
            PlayerPrefs.SetFloat(mKey, value);
            PlayerPrefs.Save();
        }
    }
    private string mKey = "";


    public PlayerPrefsFloat(string key, float value = 0)
    {
        mKey = key;
        if (PlayerPrefs.HasKey(key) == false)
        {
            PlayerPrefs.SetFloat(mKey, value);
        }
    }
}


