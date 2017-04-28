using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inspector;

public class CExamTrackCreator : MonoBehaviour
{
    private Map.CTrackCreator mTrackCreator = null;

    void Start()
    {
        mTrackCreator = new Map.CTrackCreator(this.transform);
        mTrackCreator.CreateTrackData();
        mTrackCreator.PositionTracks();
        mTrackCreator.UpdateTrackTile(0);
    }



#if UNITY_EDITOR
    private Map.CTile mCurrentTile = null;
    private GUIStyle LabelStylePlayer = null;

    [Button]
    public void StartTrack()
    {
        StartCoroutine(SeqUpdateTrack());
    }
    private IEnumerator SeqUpdateTrack()
    {
        for (int i = 0; i < mTrackCreator.TileCount; i++)
        {
            mTrackCreator.UpdateTrackTile(i);
            mCurrentTile = mTrackCreator.GetTile(i);
            yield return new WaitForSeconds(0.6f);
        }
    }


    public void OnDrawGizmos()
    {
        if (mCurrentTile != null)
        {
            if(LabelStylePlayer == null)
            {
                LabelStylePlayer = new GUIStyle();
                LabelStylePlayer.fontSize = 40;
                LabelStylePlayer.normal.textColor = Color.white;
            }
            UnityEditor.Handles.Label(mCurrentTile.transform.position, "P", LabelStylePlayer);
        }
    }

    public int PlayerIndex = 0;
    [Button]
    public void ShowTrack()
    {
        mTrackCreator.UpdateTrackTile(PlayerIndex);
    }
#endif
}
