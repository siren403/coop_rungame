using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHanMapDataMgr
{

    public Material MMainTrack = null;
    public Material MLine = null;
    public Material MOutSide = null;

    public CTrack PFTrack = null;

    public CTrackParts PFStartTrack = null;
    public CTrackParts PFVerticalTrack = null;
    public CTrackParts PFHorizontalTrack = null;

    public CTrackParts PFUpLeftTrack = null;
    public CTrackParts PFLeftUpTrack = null;
    public CTrackParts PFUpRightTrack = null;
    public CTrackParts PFRightUpTrack = null;
    public CTrackParts PFEndTrack = null;

    public Dictionary<CTrackFactory.TRACKKIND,CTrackParts> mTrackKind = null;

    private static CHanMapDataMgr mInstance = null;

    private CHanMapDataMgr()
    {
        mInstance = null;
    }

    public static CHanMapDataMgr GetInst()
    {
        if(null == mInstance)
        {
            mInstance =new CHanMapDataMgr();
        }

        return mInstance;
    }
	
    public void CreateHan()
    {
        MMainTrack = Resources.Load<Material>("Material/MMainTrack");
        MLine = Resources.Load<Material>("Material/Mline");
        MOutSide = Resources.Load<Material>("Material/MOutSide");

        PFTrack = Resources.Load<CTrack>("Prefabs/PFTrack");

        PFStartTrack = Resources.Load<CTrackParts>("Prefabs/PFStartTrack");

        PFVerticalTrack = Resources.Load<CTrackParts>("Prefabs/PFVerticalTrack");
        PFHorizontalTrack = Resources.Load<CTrackParts>("Prefabs/PFHorizontalTrack");

        PFUpLeftTrack = Resources.Load<CTrackParts>("Prefabs/PFUpLeftTrack");
        PFLeftUpTrack = Resources.Load<CTrackParts>("Prefabs/PFLeftUpTrack");
        PFUpRightTrack = Resources.Load<CTrackParts>("Prefabs/PFUpRightTrack");
        PFRightUpTrack = Resources.Load<CTrackParts>("Prefabs/PFRightUpTrack");

        PFEndTrack = Resources.Load<CTrackParts>("Prefabs/PFEndTrack");

        mTrackKind = new Dictionary<CTrackFactory.TRACKKIND, CTrackParts>();

        mTrackKind.Add(CTrackFactory.TRACKKIND.START, PFStartTrack);

        mTrackKind.Add(CTrackFactory.TRACKKIND.VERTICAL, PFVerticalTrack);
        mTrackKind.Add(CTrackFactory.TRACKKIND.HORIZONTAL, PFHorizontalTrack);

        mTrackKind.Add(CTrackFactory.TRACKKIND.UPLEFT, PFUpLeftTrack);
        mTrackKind.Add(CTrackFactory.TRACKKIND.LEFTUP, PFLeftUpTrack);
        mTrackKind.Add(CTrackFactory.TRACKKIND.UPRIGHT, PFUpRightTrack);
        mTrackKind.Add(CTrackFactory.TRACKKIND.RIGHTUP, PFRightUpTrack);
        mTrackKind.Add(CTrackFactory.TRACKKIND.END, PFEndTrack);

    }

    public CTrackParts GetPrefab(CTrackFactory.TRACKKIND tTrackKind)
    {
        return mTrackKind[tTrackKind];
    }
}
