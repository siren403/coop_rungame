using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CScenePrototype : MonoBehaviour
{
    public CTrackFactory mTrackMaker = null;

    private void Awake()
    {
        CHanMapDataMgr.GetInst().CreateHan();
        mTrackMaker.CreateTrack();
    }

   

}
