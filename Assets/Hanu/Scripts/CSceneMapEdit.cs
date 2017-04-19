using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSceneMapEdit : MonoBehaviour {

<<<<<<< HEAD
    public CTrackCreater TrackCreater = null;

=======
    public CLoadAboutMap LoadAboutMap = null;
    public CTrackCreater TrackCreater = null;


    void Awake()
    {
        LoadAboutMap = new CLoadAboutMap();
        LoadAboutMap.LoadAboutPrefabs();
    }

    // Use this for initialization
>>>>>>> df8349728c48d901342990625a74ee37954456f7
    void Start () {
        TrackCreater = new CTrackCreater();

        TrackCreater.CreateTrack();
<<<<<<< HEAD
        
    }
	

=======
    }
	
	// Update is called once per frame
	void Update () {
		
	}
>>>>>>> df8349728c48d901342990625a74ee37954456f7
}
