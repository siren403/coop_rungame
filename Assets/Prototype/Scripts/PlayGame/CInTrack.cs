using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInTrack : MonoBehaviour {

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("tagPlayer"))
        {
            other.collider.GetComponent<UsePhysics.CPlayer>().SetDecrementSpeed(0.0f);
        }
    }
}
