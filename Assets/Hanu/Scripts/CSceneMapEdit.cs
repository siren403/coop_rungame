using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inspector;


public class CSceneMapEdit : MonoBehaviour {


    public CTrackCreater TrackCreater = null;
    public GameObject hi = null;
    public int PlayerPosition = 0;

    void Start () {
        TrackCreater = new CTrackCreater();

        TrackCreater.CreateTrack(this.transform);
        

    }

    #region Track Generate Test
    private IEnumerator SeqTrack()
    {
        while(PlayerPosition < TrackCreater.TrackList.Count)
        {
            TrackCreater.UpdateTrack(PlayerPosition);
            yield return new WaitForSeconds(0.1f);
            //yield return null;
            PlayerPosition++;
        }
    }
    [Button]
    public void TrackGenerateTest()
    {
        StartCoroutine(SeqTrack());
    }
    #endregion

}
