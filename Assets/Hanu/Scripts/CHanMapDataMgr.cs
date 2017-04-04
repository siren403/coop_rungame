using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHanMapDataMgr
{

    public Material MMainTrack = null;
    public Material MLine = null;
    public Material MOutSide = null;

    public CStartTrack PFStartTrack = null;
    public CStraightTrack PFStraightTrack = null;
    public CLeftTrack PFLeftTrack = null;
    public CRightTrack PFRightTrack = null;

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

        PFStartTrack = Resources.Load<CStartTrack>("Prefabs/PFStartTrack");
        PFStraightTrack = Resources.Load<CStraightTrack>("Prefabs/PFStraightTrack");
        PFLeftTrack = Resources.Load<CLeftTrack>("Prefabs/PFLeftTrack");
        PFRightTrack = Resources.Load<CRightTrack>("Prefabs/PFRightTrack");
    }
}
