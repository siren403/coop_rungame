using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CItemTimer : MonoBehaviour
{
    private CScenePlayGame mScene = null;

    private List<CTrackItem> mTrackItemList = new List<CTrackItem>();

    public void SetScene(CScenePlayGame tScene)
    {
        mScene = tScene;
    }

    public void AddTrackItem(CTrackItem tItem)
    {
        tItem.Activate();
        mTrackItemList.Add(tItem);
    }

    public void Update()
    {
        if (mScene != null && mScene.IsPlaying)
        {
            for (int i = 0; i < mTrackItemList.Count; i++)
            {
                if (mTrackItemList[i].MoveNext() == false)
                {
                    mTrackItemList[i].Deactivate();
                    mTrackItemList[i].Dispose();
                }
                else
                {
                    //Debug.Log(mTrackItemList[i].Current);
                }
            }
            for (int i = mTrackItemList.Count - 1; i >= 0; i--)
            {
                if (mTrackItemList[i].IsDispose)
                {
                    mTrackItemList.RemoveAt(i);
                }
            }
        }
    }

    public void Reset()
    {
        foreach(var item in mTrackItemList)
        {
            item.Deactivate();
            item.Reset();
            item.Dispose();
        }
        mTrackItemList.Clear();
    }
}
