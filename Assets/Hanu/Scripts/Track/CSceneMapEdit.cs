using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inspector;


public class CSceneMapEdit : MonoBehaviour {


    public CTrackCreater TrackCreater = null;

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
            yield return new WaitForSeconds(0.5f);
            //yield return null;
            PlayerPosition++;
            if(TrackCreater.TrackList.Count == PlayerPosition)
            {
                PlayerPosition = 0;
            }
        }
    }
    [Button]
    public void TrackGenerateTest()
    {
        StartCoroutine(SeqTrack());
    }
  
    #endregion
    
}
