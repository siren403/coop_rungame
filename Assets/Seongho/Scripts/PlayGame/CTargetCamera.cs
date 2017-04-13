using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


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
    public float Angle = 270.0f;

    private Coroutine CurrentSeqRotate = null;

    [ContextMenu("Reposition")]
    public void UpdatePosition()
    {
        Vector3 pos = Target.transform.position;

        pos.x += Mathf.Cos(Angle * Mathf.Deg2Rad) * Distance;
        pos.z += Mathf.Sin(Angle * Mathf.Deg2Rad) * Distance;

        pos += Offset;
        this.transform.position = pos;

        Vector3 targetPos = Target.transform.position;
        targetPos += TargetOffset;
        this.transform.LookAt(targetPos);

    }

    public void ResetAngle()
    {
        Angle = -90.0f;
    }

    public void RotateCamera(int tDirection)
    {
        if (DOTween.IsTweening("AngleRotate"))
            return;

        DOTween.To(() => Angle, (n) => Angle = n, Angle + (-tDirection * 90), 0.15f).SetId("AngleRotate");
    }

    
}
