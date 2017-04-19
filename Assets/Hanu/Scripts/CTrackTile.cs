using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTrackTile : MonoBehaviour {

    public Vector3 Direction;
    public CTrackCreater.TRACKKIND Kind;
<<<<<<< HEAD
    public int Index;

    public void SetIndex(int tIndex)
    {
        Index = tIndex;
=======


    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("tagPlayer"))
        {
            var player = other.collider.GetComponent<UsePhysics.CPlayer>();
            player.DoDirectionInputCheck();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("tagPlayer"))
        {
            var player = other.collider.GetComponent<UsePhysics.CPlayer>();
            player.DoRotate(Direction);
        }
>>>>>>> df8349728c48d901342990625a74ee37954456f7
    }

}
