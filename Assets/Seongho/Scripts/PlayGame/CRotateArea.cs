using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRotateArea : MonoBehaviour {

    public Vector3 mDirection;
    public bool IsEnd = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CTag.TAG_PLAYER))
        {
            var player = other.GetComponent<CPlayer>();
            player.DoDirectionInputCheck();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(CTag.TAG_PLAYER))
        {
            var player = other.GetComponent<CPlayer>();
            player.DoRotate(mDirection, IsEnd);
        }
    }


}
