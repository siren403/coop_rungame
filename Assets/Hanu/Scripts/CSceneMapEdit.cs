using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSceneMapEdit : MonoBehaviour {

    //public CLoadAboutMap LoadAboutMap = null;
    public CTrackCreater TrackCreater = null;

    /*
    void Awake()
    {
        LoadAboutMap = new CLoadAboutMap();
        LoadAboutMap.LoadAboutPrefabs();
    }
    */
    // Use this for initialization
    void Start () {
        TrackCreater = new CTrackCreater();

        TrackCreater.CreateTrack();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
