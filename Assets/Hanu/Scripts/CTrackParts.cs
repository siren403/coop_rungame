using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTrackParts : MonoBehaviour {

    public Vector3 mDirection;
    public CTrackFactory.TRACKKIND mKind;

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("tagPlayer"))
        {
            Debug.Log("player");
            var player = other.collider.GetComponent<UsePhysics.CPlayer>();
            player.mIsInputDirectionChecking = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("tagPlayer"))
        {
            var player = other.collider.GetComponent<UsePhysics.CPlayer>();
            player.DoRotate(mDirection);
        }
    }

}
