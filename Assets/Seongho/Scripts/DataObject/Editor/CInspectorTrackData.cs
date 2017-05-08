using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CTrackData))]
public class CInspectorTrackData : Editor {

    public override void OnInspectorGUI()
    {
        CTrackData tTarget = target as CTrackData;

        for (int i = 0; i < tTarget.DiffcultyList.Count; i++)
        {
            for (int j = 0; j < tTarget.DiffcultyList[i].Count; j++)
            {

                

            }
        }

        base.OnInspectorGUI();
    }
}
