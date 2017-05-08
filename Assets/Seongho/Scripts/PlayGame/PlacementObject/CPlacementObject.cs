using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CPlacementObject : MonoBehaviour
{

    protected float mMagnetDistanceDelta = 10.0f;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(CTag.TAG_PLAYER))
        {
            OnPlayerEnter(other.gameObject.GetComponent<CPlayer>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(CTag.TAG_PLAYER))
        {
            OnPlayerEnter(other.GetComponent<CPlayer>());
        }
        if(other.CompareTag(CTag.TAG_MAGNET))
        {
            OnPlayerTriggerEnter(other.GetComponentInParent<CPlayer>());
        }
    }

    protected abstract void OnPlayerEnter(CPlayer tPlayer);

    protected virtual void OnPlayerTriggerEnter(CPlayer tPlayer)
    {
       
    }
}
