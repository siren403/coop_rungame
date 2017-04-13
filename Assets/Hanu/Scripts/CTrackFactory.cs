using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CTrackFactory : MonoBehaviour
{
    
    

    public const int TOTAL_TRACK = 50;
    public const int TRACK_SIZE = 18;
    public const int STRAIGHT_COUNT = 5;
    public int mTrackCount = 0;

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
        END = 8,
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
       // int ti = 0;

        this.CreateNextTrackKind();
        this.CreateStartTrack();
        for (mTrackCount = 0; mTrackCount < TOTAL_TRACK;)
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
        SetCurrentTrack(TRACKKIND.START);
        tStartTrack = GameObject.Instantiate<CTrackParts>(CHanMapDataMgr.GetInst().GetPrefab(this.GetCurrentTrack()), Vector3.zero, Quaternion.identity);
        mBeforePos = tStartTrack.transform.position;
        mNextPos = Vector3.zero;
        mCurrentDirection = Vector3.forward;
        mNextPos = mCurrentDirection * TRACK_SIZE;
        
    }



    /// <summary>
    /// 트랙을 직접적으로 조립하는 메소드
    /// </summary>
    public void DistinguishTrack()
    {
        var tTrackList = mNextTrackKind[mCurrentTrack];
        var tNextTrackKind =  tTrackList[Random.Range(0, tTrackList.Count)];
        CTrackParts tTrackParts = null;

        if (tNextTrackKind != TRACKKIND.TURN)
        {
            tTrackParts = GameObject.Instantiate<CTrackParts>(CHanMapDataMgr.GetInst().GetPrefab(tNextTrackKind), mNextPos, Quaternion.identity);
            if (tNextTrackKind != TRACKKIND.HORIZONTAL)
            {
                mCurrentDirection = tTrackParts.mDirection;
            }
            SetCurrentTrack(tNextTrackKind);
        }
        else
        {
           
            if(Vector3.right == mCurrentDirection )
            { 
                tTrackParts = GameObject.Instantiate<CTrackParts>(CHanMapDataMgr.GetInst().GetPrefab(TRACKKIND.RIGHTUP), mNextPos, Quaternion.identity);
                SetCurrentTrack (TRACKKIND.RIGHTUP);
                
            }
            else
            {
                tTrackParts = GameObject.Instantiate<CTrackParts>(CHanMapDataMgr.GetInst().GetPrefab(TRACKKIND.LEFTUP), mNextPos, Quaternion.identity);
                SetCurrentTrack(TRACKKIND.LEFTUP);
            }
           
            mCurrentDirection = tTrackParts.mDirection;
        }
        AddTrackCount();
        mBeforePos = tTrackParts.transform.position;
        mNextPos = mBeforePos + mCurrentDirection * TRACK_SIZE;
        CreateStraightTrack(tTrackParts);
        CreateEndTrack(tTrackParts);
    }


    /// <summary>
    /// 커브트랙 다음에 생성되는 트랙이 최소 3개이상 직선트랙이 나오게 생성하는 메소드
    /// </summary>
    /// <param name="tTrackParts">얘는 트랙파츠ㅇㅇ</param>
    public void CreateStraightTrack(CTrackParts tTrackParts)
    {
        tTrackParts = null;

        for (int ti = 0; ti < STRAIGHT_COUNT; ti++)
        {
            if (TOTAL_TRACK > mTrackCount)
            {
                if (TRACKKIND.LEFTUP == GetCurrentTrack() || TRACKKIND.RIGHTUP == GetCurrentTrack())
                {
                    tTrackParts = GameObject.Instantiate<CTrackParts>(CHanMapDataMgr.GetInst().GetPrefab(TRACKKIND.VERTICAL), mNextPos, Quaternion.identity);
                }
                else if (TRACKKIND.UPLEFT == GetCurrentTrack() || TRACKKIND.UPRIGHT == GetCurrentTrack())
                {
                    tTrackParts = GameObject.Instantiate<CTrackParts>(CHanMapDataMgr.GetInst().GetPrefab(TRACKKIND.HORIZONTAL), mNextPos, Quaternion.identity);
                }
                else
                {
                    return;
                }
                mBeforePos = tTrackParts.transform.position;
                AddTrackCount();
                mNextPos = mBeforePos + mCurrentDirection * TRACK_SIZE;
            }
        }

    }

    /// <summary>
    /// 마지막트랙 생성 메소드
    /// </summary>
    /// <param name="tTrackParts">얘는 트랙파츠라니깐?</param>
    public void CreateEndTrack(CTrackParts tTrackParts)
    {
        if(TOTAL_TRACK == mTrackCount)
        {
            tTrackParts = GameObject.Instantiate<CTrackParts>(CHanMapDataMgr.GetInst().GetPrefab(TRACKKIND.END), mNextPos, Quaternion.identity);
            Debug.Log(mCurrentDirection.ToString());
            if(mCurrentDirection == Vector3.right)
            {
                tTrackParts.transform.Rotate(new Vector3(0.0f, 90.0f, 0.0f));
            }
            else if(mCurrentDirection == Vector3.left)
            {
                tTrackParts.transform.Rotate(new Vector3(0.0f,-90.0f,0.0f));
            }
        } 
        

    }

    public void AddTrackCount()
    {
        mTrackCount = mTrackCount + 1;
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
