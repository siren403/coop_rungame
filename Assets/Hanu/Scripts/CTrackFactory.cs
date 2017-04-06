using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CTrackFactory : MonoBehaviour
{
    
    public const int TOTAL_TRACK = 11;
    public const int TRACK_SIZE = 18;

    public enum TRACKKIND
    {
        START = 0,
        VERTICAL = 1,
        HORIZONTAL = 2,
        UPLEFT = 3,
        LEFTUP = 4,
        UPRIGHT = 5,
        RIGHTUP = 6,
    }

    List<TRACKKIND> mNextTrackList = null;

    public Vector3 mBeforePos;
    public Vector3 mNextPos;
    public Vector3 mCurrentDirection;

    public int mRandomNum;
    public TRACKKIND mCurrentTrack;

   

    public Dictionary<TRACKKIND, List<TRACKKIND>> mNextTrackKind = null;


    public void CreateTrack()
    {
        int ti = 0;

        this.CreateNextTrackKind();
        this.CreateTrackSample();
        for (ti = 0; ti < TOTAL_TRACK; ti++)
        {
            this.DistinguishTrack();
        }

    }


    public void CreateNextTrackKind()
    {
        mNextTrackKind = new Dictionary<TRACKKIND, List<TRACKKIND>>();

        mNextTrackKind.Add(TRACKKIND.START, new List<TRACKKIND>() {TRACKKIND.VERTICAL});
        mNextTrackKind.Add(TRACKKIND.VERTICAL, new List<TRACKKIND>() { TRACKKIND.VERTICAL, TRACKKIND.UPLEFT, TRACKKIND.UPRIGHT });
        mNextTrackKind.Add(TRACKKIND.HORIZONTAL, new List<TRACKKIND>() { TRACKKIND.HORIZONTAL, TRACKKIND.LEFTUP, TRACKKIND.RIGHTUP });

        mNextTrackKind.Add(TRACKKIND.UPLEFT, new List<TRACKKIND>() { TRACKKIND.HORIZONTAL, TRACKKIND.LEFTUP });
        mNextTrackKind.Add(TRACKKIND.LEFTUP, new List<TRACKKIND>() { TRACKKIND.VERTICAL, TRACKKIND.UPLEFT, TRACKKIND.UPRIGHT });
        mNextTrackKind.Add(TRACKKIND.UPRIGHT, new List<TRACKKIND>() { TRACKKIND.HORIZONTAL, TRACKKIND.RIGHTUP });
        mNextTrackKind.Add(TRACKKIND.RIGHTUP, new List<TRACKKIND>() { TRACKKIND.VERTICAL, TRACKKIND.UPLEFT, TRACKKIND.UPRIGHT });

    }


    public void CreateTrackSample()
    {
        CStartTrack tStartTrack = null;
        tStartTrack = GameObject.Instantiate<CStartTrack>(CHanMapDataMgr.GetInst().PFStartTrack, Vector3.zero, Quaternion.identity);
        mBeforePos = tStartTrack.transform.position;
        mNextPos = Vector3.zero;
        mCurrentDirection = Vector3.forward;
        mNextPos = mCurrentDirection * TRACK_SIZE;
        SetCurrentTrack(TRACKKIND.START);
    }



    public void DistinguishTrack()
    {
        var tTrackList = mNextTrackKind[mCurrentTrack];
        var tNextTrackKind =  tTrackList[Random.Range(0, tTrackList.Count)];

        switch (tNextTrackKind)
         {
            case TRACKKIND.VERTICAL:
                CVerticalTrack tVerticalTrack = null;
                tVerticalTrack = GameObject.Instantiate<CVerticalTrack>(CHanMapDataMgr.GetInst().PFVerticalTrack, mNextPos, Quaternion.identity);
                mBeforePos = tVerticalTrack.transform.position;
                mNextPos = mBeforePos + mCurrentDirection * TRACK_SIZE;

                break;
            case TRACKKIND.HORIZONTAL:
                CHorizontalTrack tHorizontalTrack = null;
                tHorizontalTrack = GameObject.Instantiate<CHorizontalTrack>(CHanMapDataMgr.GetInst().PFHorizontalTrack, mNextPos, Quaternion.identity);
                mBeforePos = tHorizontalTrack.transform.position;
                mNextPos = mBeforePos + mCurrentDirection * TRACK_SIZE;
                break;
            case TRACKKIND.UPLEFT:
                CUpLeftTrack tUpLeftTrack = null;
                tUpLeftTrack = GameObject.Instantiate<CUpLeftTrack>(CHanMapDataMgr.GetInst().PFUpLeftTrack, mNextPos, Quaternion.identity);
                mBeforePos = tUpLeftTrack.transform.position;
                mCurrentDirection = tUpLeftTrack.mDirection;
                mNextPos = mBeforePos + mCurrentDirection * TRACK_SIZE;
                break;
            case TRACKKIND.UPRIGHT:
                CUpRightTrack tUpRightTrack = null;
                tUpRightTrack = GameObject.Instantiate<CUpRightTrack>(CHanMapDataMgr.GetInst().PFUpRightTrack, mNextPos, Quaternion.identity);
                mBeforePos = tUpRightTrack.transform.position;
                mCurrentDirection = tUpRightTrack.mDirection;
                mNextPos = mBeforePos + mCurrentDirection * TRACK_SIZE;
                break;
            case TRACKKIND.LEFTUP:
                CLeftUpTrack tLeftUpTrack = null;
                tLeftUpTrack = GameObject.Instantiate<CLeftUpTrack>(CHanMapDataMgr.GetInst().PFLeftUpTrack, mNextPos, Quaternion.identity);
                mBeforePos = tLeftUpTrack.transform.position;
                mCurrentDirection = tLeftUpTrack.mDirection;
                mNextPos = mBeforePos + mCurrentDirection * TRACK_SIZE;
                break;
            case TRACKKIND.RIGHTUP:
                CRightUpTrack tRightUpTrack = null;
                tRightUpTrack = GameObject.Instantiate<CRightUpTrack>(CHanMapDataMgr.GetInst().PFRightUpTrack, mNextPos, Quaternion.identity);
                mBeforePos = tRightUpTrack.transform.position;
                mCurrentDirection = tRightUpTrack.mDirection;
                mNextPos = mBeforePos + mCurrentDirection * TRACK_SIZE;
                break;
        }

        mCurrentTrack = tNextTrackKind;

    }


    public TRACKKIND GetCurrentTrack()
    {
        return mCurrentTrack;
    }

    public void SetCurrentTrack(TRACKKIND tTrackKind)
    {
        mCurrentTrack = tTrackKind;
    }
   
}
