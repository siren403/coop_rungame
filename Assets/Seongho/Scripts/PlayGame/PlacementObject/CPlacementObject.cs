using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CPlacementObject : MonoBehaviour
{

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(CTag.TAG_PLAYER))
        {
            OnPlayerEnter(other.gameObject.GetComponent<CPlayer>());
        }
    }

    protected abstract void OnPlayerEnter(CPlayer tPlayer);

}
