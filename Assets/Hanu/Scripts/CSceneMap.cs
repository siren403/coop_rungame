using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSceneMap : MonoBehaviour {



    //CTrack mTrack = null;
    public CTrackFactory mFactory = null;
    
	// Use this for initialization
	void Start () {

        mFactory.CreateTrack();


    }

    // Update is called once per frame
    void Update () {
		
	}
}
