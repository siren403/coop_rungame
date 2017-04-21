using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Inspector;

public class CTargetCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject mTarget = null;

    public Vector3 Offset = Vector3.zero;
    public Vector3 TargetOffset = Vector3.zero;

    private void Start()
    {
        UpdatePosition();
    }

    private void LateUpdate()
    {
        if (mTarget == null)
        {
            return;
        }

        UpdatePosition();
    }

    public float Distance = 5.0f;
    public float Angle = 270.0f;

    [ContextMenu("Reposition")]
    public void UpdatePosition()
    {
        Vector3 pos = mTarget.transform.position;

        pos.x += Mathf.Cos(Angle * Mathf.Deg2Rad) * Distance;
        pos.z += Mathf.Sin(Angle * Mathf.Deg2Rad) * Distance;

        pos += Offset;
        pos.y = Mathf.Clamp(pos.y, 0, 100);
        this.transform.position = pos;

        Vector3 targetPos = mTarget.transform.position;
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

    public void SetTarget(GameObject tTarget)
    {
        mTarget = tTarget;
    }
    
}
