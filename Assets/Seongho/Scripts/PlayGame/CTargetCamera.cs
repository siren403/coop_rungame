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

    private void LateUpdate()
    {
        if (Target == null)
        {
            return;
        }

        UpdatePosition();
    }

    public float Distance = 5.0f;
    public float Angle = 1.5f;

    [ContextMenu("Reposition")]
    public void UpdatePosition()
    {
        Vector3 pos = Target.transform.position;

        pos.x += Mathf.Cos(Mathf.PI * Angle) * Distance;
        pos.z += Mathf.Sin(Mathf.PI * Angle) * Distance;

        this.transform.position = pos;

        this.transform.LookAt(Target.transform.position);


        //pos += Offset;
        //this.transform.position = pos;

        //Vector3 targetPos = Target.transform.position;
        //targetPos += TargetOffset;

        //this.transform.LookAt(targetPos);
    }

}
