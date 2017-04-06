using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CTrackFactory : MonoBehaviour
{
    
    

    public const int TOTAL_TRACK = 20;
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
        TURN = 7,
    }

    List<TRACKKIND> mNextTrackList = null;

    public Vector3 mBeforePos;
    public Vector3 mNextPos;
    public Vector3 mCurrentDirection;

    public int mRandomNum;
    public TRACKKIND mCurrentTrack;


    public Dictionary<TRACKKIND, List<TRACKKIND>> mNextTrackKind = null;

    /// <summary>
    /// 트랙을 설치하는 메소드
    /// </summary>
    public void CreateTrack()
    {
        int ti = 0;

        this.CreateNextTrackKind();
        this.CreateStartTrack();
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
        mNextTrackKind.Add(TRACKKIND.HORIZONTAL, new List<TRACKKIND>() { TRACKKIND.HORIZONTAL, TRACKKIND.TURN });

        mNextTrackKind.Add(TRACKKIND.UPLEFT, new List<TRACKKIND>() { TRACKKIND.HORIZONTAL, TRACKKIND.LEFTUP });
        mNextTrackKind.Add(TRACKKIND.LEFTUP, new List<TRACKKIND>() { TRACKKIND.VERTICAL, TRACKKIND.UPLEFT, TRACKKIND.UPRIGHT });
        mNextTrackKind.Add(TRACKKIND.UPRIGHT, new List<TRACKKIND>() { TRACKKIND.HORIZONTAL, TRACKKIND.RIGHTUP });
        mNextTrackKind.Add(TRACKKIND.RIGHTUP, new List<TRACKKIND>() { TRACKKIND.VERTICAL, TRACKKIND.UPLEFT, TRACKKIND.UPRIGHT });

    }

    /// <summary>
    /// 시작트랙를 먼저 설치를 한다.
    /// </summary>
    public void CreateStartTrack()
    {
        CTrackParts tStartTrack = null;
        tStartTrack = GameObject.Instantiate<CTrackParts>(CHanMapDataMgr.GetInst().PFStartTrack, Vector3.zero, Quaternion.identity);
        mBeforePos = tStartTrack.transform.position;
        mNextPos = Vector3.zero;
        mCurrentDirection = Vector3.forward;
        mNextPos = mCurrentDirection * TRACK_SIZE;
        SetCurrentTrack(TRACKKIND.START);
    }



    /// <summary>
    /// 트랙을 직접적으로 조립하는 메소드
    /// </summary>
    public void DistinguishTrack()
    {
        var tTrackList = mNextTrackKind[mCurrentTrack];
        var tNextTrackKind =  tTrackList[Random.Range(0, tTrackList.Count)];
        if (tNextTrackKind != TRACKKIND.TURN)
        {
            if (tNextTrackKind != TRACKKIND.HORIZONTAL)
            {
                CTrackParts tTrackParts = null;
                tTrackParts = GameObject.Instantiate<CTrackParts>(CHanMapDataMgr.GetInst().GetPrefab(tNextTrackKind), mNextPos, Quaternion.identity);
                mBeforePos = tTrackParts.transform.position;
                mCurrentDirection = tTrackParts.mDirection;
                mNextPos = mBeforePos + mCurrentDirection * TRACK_SIZE;
                mCurrentTrack = tNextTrackKind;
            }
            else
            {
                CTrackParts tTrackParts = null;
                tTrackParts = GameObject.Instantiate<CTrackParts>(CHanMapDataMgr.GetInst().GetPrefab(tNextTrackKind), mNextPos, Quaternion.identity);
                mBeforePos = tTrackParts.transform.position;
                mNextPos = mBeforePos + mCurrentDirection * TRACK_SIZE;
                mCurrentTrack = tNextTrackKind;
            }
        }
        else
        {
            if(Vector3.right == mCurrentDirection )
            {
                CTrackParts tTrackParts = null;
                tTrackParts = GameObject.Instantiate<CTrackParts>(CHanMapDataMgr.GetInst().GetPrefab(TRACKKIND.RIGHTUP), mNextPos, Quaternion.identity);
                mBeforePos = tTrackParts.transform.position;
                mCurrentDirection = tTrackParts.mDirection;
                mNextPos = mBeforePos + mCurrentDirection * TRACK_SIZE;
                mCurrentTrack = TRACKKIND.RIGHTUP;
            }
            else
            {
                CTrackParts tTrackParts = null;
                tTrackParts = GameObject.Instantiate<CTrackParts>(CHanMapDataMgr.GetInst().GetPrefab(TRACKKIND.LEFTUP), mNextPos, Quaternion.identity);
                mBeforePos = tTrackParts.transform.position;
                mCurrentDirection = tTrackParts.mDirection;
                mNextPos = mBeforePos + mCurrentDirection * TRACK_SIZE;
                mCurrentTrack = TRACKKIND.LEFTUP;
            }
        }
    

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
