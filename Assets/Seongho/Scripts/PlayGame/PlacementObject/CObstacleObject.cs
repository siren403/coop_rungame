using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CObstacleObject : CPlacementObject
{
    [SerializeField]
    private bool mIsGameOver = false;
    [SerializeField]
    private int mDamageValue = 100;

    protected override void OnPlayerEnter(CPlayer tPlayer)
    {
        if (mIsGameOver)
        {
            tPlayer.DecrementHp(tPlayer.CurrentHp.Value);
        }
        else
        {
            if (tPlayer.IsShield == false)
            {
                 tPlayer.DecrementHp(mDamageValue);
            }
            else
            {
                Debug.Log("막음?");
            }
        }
    }

#if UNITY_EDITOR
    private GUIStyle LabelStyleItemType = null;
    private void OnDrawGizmos()
    {
        if (LabelStyleItemType == null)
        {
            LabelStyleItemType = new GUIStyle();
            LabelStyleItemType.fontSize = 40;
            LabelStyleItemType.normal.textColor = Color.white;
        }
        UnityEditor.Handles.Label(this.transform.position, "Obstacle");
    }
#endif
}
