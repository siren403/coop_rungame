using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSceneMap : MonoBehaviour {

    //CStartTrack mStartTrack = null;
    //CStraightTrack mStraightTrack = null;

    CTrack mTrack = null;
    public CTrackFactory mFactory = null;
    
	// Use this for initialization
	void Start () {


        StartCoroutine( mFactory.CreateTrack());

        //mTrack.asd();    

        //mStartTrack = Instantiate<CStartTrack>(CHanMapDataMgr.GetInst().PFStartTrack,Vector3.zero , Quaternion.identity);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
