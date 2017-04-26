using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEndTrackLeftCollider : MonoBehaviour {

    public CTrackCreater TrackCreater = null;

    private void Start()
    {
        TrackCreater = GetComponentInParent<CTrackTile>().TrackCreater;
    }

    public void SetTrackCreater(CTrackCreater tTrackCreater)
    {
        TrackCreater = tTrackCreater;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(CTag.TAG_PLAYER))
        {
            TrackCreater.SetNextStage(CTrackCreater.NEXTROTATION.LEFT);
        }
    }
}
