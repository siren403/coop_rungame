using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTrackTile : MonoBehaviour {

    public CTrackCreater TrackCreater = null;
    public Vector3 Direction;
    public CTrackCreater.TRACKKIND Kind;

    public int Index;

    private CPlacementObject mPlacementObject = null;
    

    public void SetTrackCreater(CTrackCreater tTrackCreater)
    {
        TrackCreater = tTrackCreater;
    }

    public void SetIndex(int tIndex)
    {
        Index = tIndex;
    }
    public int GetIndex()
    {
        return Index;
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
        if (Kind == CTrackCreater.TRACKKIND.VERTICAL)
        {
            mPlacementObject = GetRandomPlacementObject();
            mPlacementObject.transform.SetParent(this.transform);
            mPlacementObject.transform.rotation = Quaternion.Euler(Vector3.up * 90);
            mPlacementObject.transform.localPosition = Vector3.zero;
        }
        else if (Kind == CTrackCreater.TRACKKIND.HORIZONTAL)
        {
            mPlacementObject = GetRandomPlacementObject();
            mPlacementObject.transform.SetParent(this.transform);

            mPlacementObject.transform.localPosition = Vector3.zero;
        }
    }
    private CPlacementObject GetRandomPlacementObject()
    {
        CPlacementObject obj = null;
        int n = Random.Range(0, 3);
        switch (n)
        {
            case 0:
                obj = Instantiate(Resources.Load<CPlacementObject>("PlacementObject/PFCoin"));
                break;
            case 1:
                obj = Instantiate(Resources.Load<CPlacementObject>("PlacementObject/PFItem"));
                break;
            case 2:
                obj = Instantiate(Resources.Load<CPlacementObject>("PlacementObject/PFObstacle"));
                break;
        }
        return obj;
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
        if(mPlacementObject != null)
        {
            Destroy(mPlacementObject.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(CTag.TAG_PLAYER))
        {
            Debug.Log(GetIndex().ToString());
            TrackCreater.SetPlayerIndex(GetIndex());
            TrackCreater.UpdateTrack(GetIndex());
        }
    }

}
