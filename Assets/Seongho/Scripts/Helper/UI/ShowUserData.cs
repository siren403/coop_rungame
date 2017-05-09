using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inspector;

public class ShowUserData : MonoBehaviour
{

    public Text InstTxtCoin = null;
    private UserData mUserData = null;

    private void Start()
    {
        UpdateUserData();
    }

    [Button]
    public void UpdateUserData()
    {
        if(mUserData == null)
        {
            mUserData = new UserData();
        }
        if (InstTxtCoin != null)
        {
            InstTxtCoin.text = string.Format("{0}", mUserData.Coin);
        }
    }
}
