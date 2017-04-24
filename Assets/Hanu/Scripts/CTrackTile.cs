using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTrackTile : MonoBehaviour {

    public CTrackCreater TrackCreater = null;
    public Vector3 Direction;
    public CTrackCreater.TRACKKIND Kind;

    public int Index;

    public void SetIndex(int tIndex)
    {
        Index = tIndex;
    }

    public int GetIndex()
    {
        return Index;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(CTag.TAG_PLAYER))
        {
            TrackCreater.SetPlayerIndex(GetIndex());
        }
    }

}
