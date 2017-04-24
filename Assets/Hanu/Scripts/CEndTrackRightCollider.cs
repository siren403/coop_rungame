using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inspector;

public class CEndTrackRightCollider : MonoBehaviour {

    public CTrackCreater TrackCreater = null;

    private void Start()
    {
        TrackCreater = new CTrackCreater();
    }
    [Button]
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(CTag.TAG_PLAYER))
        {
            TrackCreater.SetNextStage(CTrackCreater.NEXTROTATION.RIGHT);
        }
    }
}
