using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInTrack : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CTag.TAG_PLAYER))
        {
            other.GetComponent<CPlayer>().SetSpeedRatio(1.0f);
        }
    }
}
