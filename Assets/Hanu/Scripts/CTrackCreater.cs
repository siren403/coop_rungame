using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public class CTrackCreater { 

    public const int TOTAL_TRACK = 50;
    public const int END_TRACK_COUNT = 1;
    public const int TRACK_SIZE = 18;
    public const int STRAIGHT_COUNT = 5;
    public const int STARTING_TRACK = 5;


    public CLoadAboutMap LoadAboutMap = null;
=======
public class CTrackCreater
{ 

    public const int TOTAL_TRACK = 50;
    public const int TRACK_SIZE = 18;
    public const int STRAIGHT_COUNT = 5;
    
>>>>>>> df8349728c48d901342990625a74ee37954456f7

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

    private List<TRACKKIND> mNextTrackList = null;

    public Vector3 BeforePos;
    public Vector3 NextPos;
    public Vector3 CurrentDirection;

    public int RandomNum;
    public int TrackCount = 0;
    public TRACKKIND CurrentTrack;


    public Dictionary<TRACKKIND, List<TRACKKIND>> NextTrackKind = null;

<<<<<<< HEAD
    public Dictionary<int,TRACKKIND> Trackist = null;

    public CTrackTile[] TrackListSemple = null;

=======
>>>>>>> df8349728c48d901342990625a74ee37954456f7
    /// <summary>
    /// 트랙을 설치하는 메소드
    /// </summary>
    public void CreateTrack()
    {
<<<<<<< HEAD
        TrackListSemple = new CTrackTile[TOTAL_TRACK + END_TRACK_COUNT];
        //타일프리팹들을 불러옴.
        LoadAboutMap = new CLoadAboutMap();
        LoadAboutMap.LoadAboutPrefabs();

        Trackist = new Dictionary<int, TRACKKIND>();

        this.CreateNextTrackKind();
        this.CreateStartTrack();
        for (TrackCount = 1; TrackCount < TOTAL_TRACK;)
        {
            this.DistinguishTrack();
           
        }

        //시작트랙부터 5개트랙외에 
        for(int ti= STARTING_TRACK; ti<TrackListSemple.Length;ti++)
        {
            Debug.Log(TrackListSemple[ti]);
            TrackListSemple[ti].gameObject.SetActive(false);
        }
      
=======
        // int ti = 0;

        this.CreateNextTrackKind();
        this.CreateStartTrack();
        for (TrackCount = 0; TrackCount < TOTAL_TRACK;)
        {
            this.DistinguishTrack();
        }

>>>>>>> df8349728c48d901342990625a74ee37954456f7
    }


    public void CreateNextTrackKind()
    {
        NextTrackKind = new Dictionary<TRACKKIND, List<TRACKKIND>>();

        NextTrackKind.Add(TRACKKIND.START, new List<TRACKKIND>() { TRACKKIND.VERTICAL });
        NextTrackKind.Add(TRACKKIND.VERTICAL, new List<TRACKKIND>() { TRACKKIND.VERTICAL, TRACKKIND.UPLEFT, TRACKKIND.UPRIGHT });
        NextTrackKind.Add(TRACKKIND.HORIZONTAL, new List<TRACKKIND>() { TRACKKIND.HORIZONTAL, TRACKKIND.TURN });

        NextTrackKind.Add(TRACKKIND.UPLEFT, new List<TRACKKIND>() { TRACKKIND.HORIZONTAL, TRACKKIND.LEFTUP });
        NextTrackKind.Add(TRACKKIND.LEFTUP, new List<TRACKKIND>() { TRACKKIND.VERTICAL, TRACKKIND.UPLEFT, TRACKKIND.UPRIGHT });
        NextTrackKind.Add(TRACKKIND.UPRIGHT, new List<TRACKKIND>() { TRACKKIND.HORIZONTAL, TRACKKIND.RIGHTUP });
        NextTrackKind.Add(TRACKKIND.RIGHTUP, new List<TRACKKIND>() { TRACKKIND.VERTICAL, TRACKKIND.UPLEFT, TRACKKIND.UPRIGHT });

    }

    /// <summary>
    /// 시작트랙를 먼저 설치를 한다.
    /// </summary>
    public void CreateStartTrack()
    {
<<<<<<< HEAD
        CTrackTile tTrackTile = null;
        SetCurrentTrack(TRACKKIND.START);
        //tTrackTile = GameObject.Instantiate<CTrackTile>(LoadAboutMap.GetPrefab(TRACKKIND.START), Vector3.zero, Quaternion.identity);
        Trackist.Add(GetTrackCount(),GetCurrentTrack());

        //SetTrackList(tTrackTile);
        AddTrackCount();

        BeforePos = tTrackTile.transform.position;
=======
        CTrackParts tStartTrack = null;
        SetCurrentTrack(TRACKKIND.START);
       // tStartTrack = GameObject.Instantiate<CTrackParts>(CHanMapDataMgr.GetInst().GetPrefab(this.GetCurrentTrack()), Vector3.zero, Quaternion.identity);
        BeforePos = tStartTrack.transform.position;
>>>>>>> df8349728c48d901342990625a74ee37954456f7
        NextPos = Vector3.zero;
        CurrentDirection = Vector3.forward;
        NextPos = CurrentDirection * TRACK_SIZE;

    }



    /// <summary>
    /// 트랙을 직접적으로 조립하는 메소드
    /// </summary>
    public void DistinguishTrack()
    {
        var tTrackList = NextTrackKind[CurrentTrack];
        var tNextTrackKind = tTrackList[Random.Range(0, tTrackList.Count)];
<<<<<<< HEAD
        CTrackTile tTrackTile = null;

        if (tNextTrackKind != TRACKKIND.TURN)
        {
            // tTrackTile = GameObject.Instantiate<CTrackTile>(LoadAboutMap.TrackKind[tNextTrackKind], NextPos, Quaternion.identity);
            Trackist.Add(GetTrackCount(), GetCurrentTrack());


            if (tNextTrackKind != TRACKKIND.HORIZONTAL)
            {
                CurrentDirection = tTrackTile.Direction;
=======
        CTrackParts tTrackParts = null;

        if (tNextTrackKind != TRACKKIND.TURN)
        {
            //tTrackParts = GameObject.Instantiate<CTrackParts>(CHanMapDataMgr.GetInst().GetPrefab(tNextTrackKind), NextPos, Quaternion.identity);
            if (tNextTrackKind != TRACKKIND.HORIZONTAL)
            {
                CurrentDirection = tTrackParts.mDirection;
>>>>>>> df8349728c48d901342990625a74ee37954456f7
            }
            SetCurrentTrack(tNextTrackKind);
        }
        else
        {

            if (Vector3.right == CurrentDirection)
            {
<<<<<<< HEAD
                // tTrackTile = GameObject.Instantiate<CTrackTile>(LoadAboutMap.GetPrefab(TRACKKIND.RIGHTUP), NextPos, Quaternion.identity);
                Trackist.Add(GetTrackCount(), GetCurrentTrack());

=======
               // tTrackParts = GameObject.Instantiate<CTrackParts>(CHanMapDataMgr.GetInst().GetPrefab(TRACKKIND.RIGHTUP), NextPos, Quaternion.identity);
>>>>>>> df8349728c48d901342990625a74ee37954456f7
                SetCurrentTrack(TRACKKIND.RIGHTUP);

            }
            else
            {
<<<<<<< HEAD
                // tTrackTile = GameObject.Instantiate<CTrackTile>(LoadAboutMap.GetPrefab(TRACKKIND.LEFTUP), NextPos, Quaternion.identity);
                Trackist.Add(GetTrackCount(), GetCurrentTrack());


                SetCurrentTrack(TRACKKIND.LEFTUP);
            }

            CurrentDirection = tTrackTile.Direction;
        }
        //SetTrackList(tTrackTile);
        AddTrackCount();

        BeforePos = tTrackTile.transform.position;
        NextPos = BeforePos + CurrentDirection * TRACK_SIZE;
        CreateStraightTrack(tTrackTile);
        CreateEndTrack(tTrackTile);
=======
                //tTrackParts = GameObject.Instantiate<CTrackParts>(CHanMapDataMgr.GetInst().GetPrefab(TRACKKIND.LEFTUP), NextPos, Quaternion.identity);
                SetCurrentTrack(TRACKKIND.LEFTUP);
            }

            CurrentDirection = tTrackParts.mDirection;
        }
        AddTrackCount();
        BeforePos = tTrackParts.transform.position;
        NextPos = BeforePos + CurrentDirection * TRACK_SIZE;
        CreateStraightTrack(tTrackParts);
        CreateEndTrack(tTrackParts);
>>>>>>> df8349728c48d901342990625a74ee37954456f7
    }


    /// <summary>
    /// 커브트랙 다음에 생성되는 트랙이 최소 3개이상 직선트랙이 나오게 생성하는 메소드
    /// </summary>
<<<<<<< HEAD
    /// <param name="tTrackTile">얘는 트랙파츠ㅇㅇ</param>
    public void CreateStraightTrack(CTrackTile tTrackTile)
    {
        tTrackTile = null;
=======
    /// <param name="tTrackParts">얘는 트랙파츠ㅇㅇ</param>
    public void CreateStraightTrack(CTrackParts tTrackParts)
    {
        tTrackParts = null;
>>>>>>> df8349728c48d901342990625a74ee37954456f7

        for (int ti = 0; ti < STRAIGHT_COUNT; ti++)
        {
            if (TOTAL_TRACK > TrackCount)
            {
                if (TRACKKIND.LEFTUP == GetCurrentTrack() || TRACKKIND.RIGHTUP == GetCurrentTrack())
                {
<<<<<<< HEAD
                    //tTrackTile = GameObject.Instantiate<CTrackTile>(LoadAboutMap.GetPrefab(TRACKKIND.VERTICAL), NextPos, Quaternion.identity);
                    Trackist.Add(GetTrackCount(), GetCurrentTrack());


                    //SetTrackList(tTrackTile);
                    AddTrackCount();

                    BeforePos = tTrackTile.transform.position;
                    NextPos = BeforePos + CurrentDirection * TRACK_SIZE;
                }
                else if (TRACKKIND.UPLEFT == GetCurrentTrack() || TRACKKIND.UPRIGHT == GetCurrentTrack())
                {
                    //tTrackTile = GameObject.Instantiate<CTrackTile>(LoadAboutMap.GetPrefab(TRACKKIND.HORIZONTAL), NextPos, Quaternion.identity);
                    Trackist.Add(GetTrackCount(), GetCurrentTrack());


                    //SetTrackList(tTrackTile);
                    AddTrackCount();

                    BeforePos = tTrackTile.transform.position;
                    NextPos = BeforePos + CurrentDirection * TRACK_SIZE;
                }
               
                
                
=======
                    //tTrackParts = GameObject.Instantiate<CTrackParts>(CHanMapDataMgr.GetInst().GetPrefab(TRACKKIND.VERTICAL), NextPos, Quaternion.identity);
                }
                else if (TRACKKIND.UPLEFT == GetCurrentTrack() || TRACKKIND.UPRIGHT == GetCurrentTrack())
                {
                    //tTrackParts = GameObject.Instantiate<CTrackParts>(CHanMapDataMgr.GetInst().GetPrefab(TRACKKIND.HORIZONTAL), NextPos, Quaternion.identity);
                }
                else
                {
                    return;
                }
                BeforePos = tTrackParts.transform.position;
                AddTrackCount();
                NextPos = BeforePos + CurrentDirection * TRACK_SIZE;
>>>>>>> df8349728c48d901342990625a74ee37954456f7
            }
        }

    }

    /// <summary>
    /// 마지막트랙 생성 메소드
    /// </summary>
    /// <param name="tTrackParts">얘는 트랙파츠라니깐?</param>
<<<<<<< HEAD
    public void CreateEndTrack(CTrackTile tTrackTile)
    {
        if (TOTAL_TRACK == TrackCount)
        {
            //tTrackTile = GameObject.Instantiate<CTrackTile>(LoadAboutMap.GetPrefab(TRACKKIND.END), NextPos, Quaternion.identity);
            Trackist.Add(GetTrackCount(), GetCurrentTrack());

            if (CurrentDirection == Vector3.right)
            {
                tTrackTile.transform.Rotate(new Vector3(0.0f, 90.0f, 0.0f));
            }
            else if (CurrentDirection == Vector3.left)
            {
                tTrackTile.transform.Rotate(new Vector3(0.0f, -90.0f, 0.0f));
            }
            //SetTrackList(tTrackTile);
            AddTrackCount();
=======
    public void CreateEndTrack(CTrackParts tTrackParts)
    {
        if (TOTAL_TRACK == TrackCount)
        {
           // tTrackParts = GameObject.Instantiate<CTrackParts>(CHanMapDataMgr.GetInst().GetPrefab(TRACKKIND.END), NextPos, Quaternion.identity);
            if (CurrentDirection == Vector3.right)
            {
                tTrackParts.transform.Rotate(new Vector3(0.0f, 90.0f, 0.0f));
            }
            else if (CurrentDirection == Vector3.left)
            {
                tTrackParts.transform.Rotate(new Vector3(0.0f, -90.0f, 0.0f));
            }
>>>>>>> df8349728c48d901342990625a74ee37954456f7
        }


    }

    public void AddTrackCount()
    {
        TrackCount = TrackCount + 1;
    }

<<<<<<< HEAD
    public int GetTrackCount()
    {
        return TrackCount;
    }

    public void SetTrackList(CTrackTile tTrackTile)
    {
        TrackListSemple[GetTrackCount()] = tTrackTile;
        //tTrackTile.SetIndex(GetTrackCount());
    }

=======
>>>>>>> df8349728c48d901342990625a74ee37954456f7
    public TRACKKIND GetCurrentTrack()
    {
        return CurrentTrack;
    }

    public void SetCurrentTrack(TRACKKIND tTrackKind)
    {
        CurrentTrack = tTrackKind;
    }
}
