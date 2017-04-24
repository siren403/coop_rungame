using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInTrack : MonoBehaviour {

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(CTag.TAG_PLAYER))
        {
            other.collider.GetComponent<CPlayer>().SetDecrementSpeedRatio(1.0f);
        }
    }
}
