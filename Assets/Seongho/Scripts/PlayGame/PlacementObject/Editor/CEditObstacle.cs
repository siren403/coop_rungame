using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CObstacleObject))]
public class CEditObstacle : Editor
{
    private BoxCollider[] mColliders = null;

    private void OnEnable()
    {
        mColliders = ((CObstacleObject)target).GetComponents<BoxCollider>();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (mColliders != null)
        {
            for (int i = 0; i < mColliders.Length; i += 4)
            {
                GUILayout.BeginHorizontal();
                for (int j = 0; j < 4; j++)
                {
                    if ((j + i) < mColliders.Length)
                    {
                        GUI.color = mColliders[i + j].enabled == false ? Color.gray : Color.green;
                        if (GUILayout.Button((i+j + 1).ToString()))
                        {
                            mColliders[i + j].enabled = !mColliders[i + j].enabled;
                        }
                    }
                }
                GUILayout.EndHorizontal();
            }
        }
    }

    private void OnSceneGUI()
    {
        CObstacleObject tTarget = target as CObstacleObject;
        if (Selection.activeObject.Equals(tTarget.gameObject))
        {
            if (mColliders != null)
            {
                foreach(var box in mColliders)
                {
                    Handles.color = box.enabled == false ? Color.gray : Color.green;
                    Vector3 tCenter = box.center;
                    tCenter.z = tTarget.transform.position.z;
                    Handles.DrawWireCube(tCenter, box.size);
                }
            }
        }
    }
    //[Test]
    //public void CEditObstacleSimplePasses() {
    //	// Use the Assert class to test conditions.
    //}

    //// A UnityTest behaves like a coroutine in PlayMode
    //// and allows you to yield null to skip a frame in EditMode
    //[UnityTest]
    //public IEnumerator CEditObstacleWithEnumeratorPasses() {
    //	// Use the Assert class to test conditions.
    //	// yield to skip a frame
    //	yield return null;
    //}
}
