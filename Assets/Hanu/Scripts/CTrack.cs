using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTrack : MonoBehaviour {

    public List<CTrackParts> mTrackList;
    public CTrackFactory tTrackFactory = null;
	// Use this for initialization
	void Start () {
        /*
        tTrackFactory = new CTrackFactory();

        tTrackFactory.CreateTrackSample();
        tTrackFactory.PuchTrackList();
        */
	}
	
	// Update is called once per frame
	void Update () {
		
	}
  
   public void AddTrack(CTrackParts tTrack)
    {
        mTrackList.Add(tTrack);
    }
}
