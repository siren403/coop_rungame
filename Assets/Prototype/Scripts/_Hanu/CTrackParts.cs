using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTrackParts : MonoBehaviour {

    public Vector3 mDirection;
    public CTrackFactory.TRACKKIND mKind;

    private void OnCollisionEnter(Collision other)
    {
        return;
        if(other.collider.CompareTag("tagPlayer_"))
        {
            var player = other.collider.GetComponent<CPlayer>();
            player.DoDirectionInputCheck();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        return;
        if (other.collider.CompareTag("tagPlayer_"))
        {
            var player = other.collider.GetComponent<CPlayer>();
            player.DoRotate(mDirection,false);
        }
    }

}
