using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CSceneLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CHanMapDataMgr.GetInst().CreateHan();
        GoSceneMap();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GoSceneMap()
    {
        SceneManager.LoadScene("CSceneMap");
    }
}
