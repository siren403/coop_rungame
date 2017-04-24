using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inspector;

public class CEndTrackRightCollider : MonoBehaviour {

    public CTrackCreater TrackCreater = null;

    public void SetTrackCreater(CTrackCreater tTrackCreater)
    {
        TrackCreater = tTrackCreater;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(CTag.TAG_PLAYER))
        {
            TrackCreater.SetNextStage(CTrackCreater.NEXTROTATION.RIGHT);
        }
    }
}
