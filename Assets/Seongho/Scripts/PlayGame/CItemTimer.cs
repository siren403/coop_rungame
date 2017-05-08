using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CItemTimer : MonoBehaviour
{
    private CScenePlayGame mScene = null;

    private List<CTrackItem> mTrackItemList = new List<CTrackItem>();

    private Dictionary<CItemObject.ItemType, CTrackItem> mTrackItem = new Dictionary<CItemObject.ItemType, CTrackItem>();


    private void Awake()
    {
        CTrackItem tItem = null;
        mTrackItem.Add(CItemObject.ItemType.Boost, tItem);
        mTrackItem.Add(CItemObject.ItemType.Shield, tItem);
        mTrackItem.Add(CItemObject.ItemType.Magnet, tItem);
        mTrackItem.Add(CItemObject.ItemType.Heal, tItem);
        mTrackItem.Add(CItemObject.ItemType.Dash, tItem);
    }

    public void SetScene(CScenePlayGame tScene)
    {
        mScene = tScene;
    }

    public void AddTrackItem(CItemObject.ItemType tItemType, CTrackItem tItem)
    {
       // tItem.Activate();
        if(mTrackItem.ContainsKey(tItemType) == false)
        {
            mTrackItem.Add(tItemType, tItem);
            Debug.Log("님?");
        }
        else
        {
            Debug.Log("갱신?");
            mTrackItem[tItemType] = tItem; 
        }
        mTrackItem[tItemType].Activate();
        mTrackItem[tItemType].IsExecution = true;

    }

    public void Update()
    {
        if (mScene != null && mScene.IsPlaying)
        {

            for (int ti = 0; ti < mTrackItem.Count; ti++)
            {
                if (mTrackItem.ContainsKey((CItemObject.ItemType)ti) == true)
                {
                    if (mTrackItem[(CItemObject.ItemType)ti] != null)
                    {

                        if (mTrackItem[(CItemObject.ItemType)ti].IsExecution == true)
                        {
                            mTrackItem[(CItemObject.ItemType)ti].IsExecution = false;
                            Debug.Log(mTrackItem[(CItemObject.ItemType)ti].Current.ToString());
                            Debug.Log("발동중에 또 발동한거");

                            mTrackItem[(CItemObject.ItemType)ti].Reset();
                        }
                        
                        if (mTrackItem[(CItemObject.ItemType)ti].MoveNext() == false)
                        {
                            Debug.Log("끝?" + mTrackItem[(CItemObject.ItemType)ti].Current.ToString());

                            mTrackItem[(CItemObject.ItemType)ti].Deactivate();
                            mTrackItem[(CItemObject.ItemType)ti].Dispose();
                        }
                    }
                    else
                    {
                       // Debug.Log("null");
                    }
                }
                else
                {
                    Debug.Log("키가없는데요");
                }

                for (int tj = mTrackItem.Count - 1; tj >= 0; tj--)
                {
                    if (mTrackItem[(CItemObject.ItemType)tj] != null)
                    {
                        if (mTrackItem[(CItemObject.ItemType)tj].IsDispose)
                        {
                            //mTrackItem.Remove((CItemObject.ItemType)tj);
                            mTrackItem[(CItemObject.ItemType)tj] = null;
                        }

                    }
                }


            }



            /*
           for(int ti = mTrackItem.Count - 1; ti >= 0; ti--)
            {
                if(mTrackItem[(CItemObject.ItemType)ti].IsDispose)
                {
                    mTrackItem.Remove((CItemObject.ItemType)ti);
                }
            }
           */

            /*
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
            */


        }
    }
    
    public void Reset()
    {
       /* for (int ti = 0; ti < mTrackItem.Count; ti++)
        {
           // mTrackItem[(CItemObject.ItemType)ti].Deactivate();
           // mTrackItem[(CItemObject.ItemType)ti].Reset();
            //mTrackItem[(CItemObject.ItemType)ti].Dispose();
        }
        mTrackItem.Clear();*/
    }
    
}
