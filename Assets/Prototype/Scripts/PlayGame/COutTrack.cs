using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COutTrack : MonoBehaviour {

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(CTagManager.TAG_PLAYER))
        {
            other.collider.GetComponent<CPlayer>().SetDecrementSpeed(15.0f);
        }
    }

   
}
