﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CWall : MonoBehaviour {

    public Material mMaterial = null;

    // Use this for initialization
    void Start () {

        CeateHan();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CeateHan()
    {
        MeshRenderer tManinTrack = GetComponent<MeshRenderer>();
        mMaterial = CHanMapDataMgr.GetInst().MWall;
        tManinTrack.material = mMaterial;
    }
}
