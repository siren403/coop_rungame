using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHanMapDataMgr
{

    public Material MMainTrack = null;
    public Material MLine = null;
    public Material MOutSide = null;

    public CTrack PFTrack = null;

    public CStartTrack PFStartTrack = null;
    public CVerticalTrack PFVerticalTrack = null;
    public CHorizontalTrack PFHorizontalTrack = null;

    public CUpLeftTrack PFUpLeftTrack = null;
    public CLeftUpTrack PFLeftUpTrack = null;
    public CUpRightTrack PFUpRightTrack = null;
    public CRightUpTrack PFRightUpTrack = null;


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

        PFStartTrack = Resources.Load<CStartTrack>("Prefabs/PFStartTrack");

        PFVerticalTrack = Resources.Load<CVerticalTrack>("Prefabs/PFVerticalTrack");
        PFHorizontalTrack = Resources.Load<CHorizontalTrack>("Prefabs/PFHorizontalTrack");

        PFUpLeftTrack = Resources.Load<CUpLeftTrack>("Prefabs/PFUpLeftTrack");
        PFLeftUpTrack = Resources.Load<CLeftUpTrack>("Prefabs/PFLeftUpTrack");
        PFUpRightTrack = Resources.Load<CUpRightTrack>("Prefabs/PFUpRightTrack");
        PFRightUpTrack = Resources.Load<CRightUpTrack>("Prefabs/PFRightUpTrack");
    }
}
