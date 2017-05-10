using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class CAdsReward : MonoBehaviour
{
    public Text mTxtAdsResult = null;


    public void OnClickBtnShowAds()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                mTxtAdsResult.text = "The ad was successfully shown.";
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                mTxtAdsResult.text = "The ad was skipped before reaching the end.";

                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                mTxtAdsResult.text = "The ad failed to be shown.";

                break;
        }
    }
}
