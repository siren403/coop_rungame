using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTargetCamera : MonoBehaviour
{
    public GameObject Target = null;

    public Vector3 Offset = Vector3.zero;
    public Vector3 TargetOffset = Vector3.zero;

    private void Start()
    {
        UpdatePosition();
    }

    //private void LateUpdate()
    //{
    //    if(Target == null)
    //    {
    //        return;
    //    }

    //    UpdatePosition();
    //}

    [ContextMenu("Reposition")]
    public void UpdatePosition()
    {
        Vector3 pos = Target.transform.position;
        pos += Offset;
        this.transform.position = pos;

        Vector3 targetPos = Target.transform.position;
        targetPos += TargetOffset;
        this.transform.LookAt(targetPos);
    }

}
