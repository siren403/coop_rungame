using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSceneMapEdit : MonoBehaviour {

    public CTrackCreater TrackCreater = null;

    void Start () {
        TrackCreater = new CTrackCreater();

        TrackCreater.CreateTrack();
        
    }
	

}
