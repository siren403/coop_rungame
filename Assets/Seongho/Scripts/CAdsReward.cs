using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Advertisements;



public class CAdsReward : MonoBehaviour
{
    public UnityEvent AdsFinished = null;
    public UnityEvent AdsSkipped = null;
    public UnityEvent AdsFailed = null;

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
                if (AdsFinished != null)
                {
                    AdsFinished.Invoke();
                }
                break;
            case ShowResult.Skipped:
                if (AdsSkipped != null)
                {
                    AdsSkipped.Invoke();
                }
                break;
            case ShowResult.Failed:
                if (AdsFailed != null)
                {
                    AdsFailed.Invoke();
                }
                break;
        }
    }
}
