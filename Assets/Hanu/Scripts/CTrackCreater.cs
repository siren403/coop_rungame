using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTrackCreater { 

    public const int TOTAL_TRACK = 50;
    public const int END_TRACK_COUNT = 1;
    public const int TRACK_SIZE = 18;
    public const int STRAIGHT_COUNT = 5;
    public const int STARTING_TRACK = 5;


    public CLoadAboutMap LoadAboutMap = null;

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

    public Dictionary<int,TRACKKIND> Trackist = null;

    public CTrackTile[] TrackListSemple = null;

    /// <summary>
    /// 트랙을 설치하는 메소드
    /// </summary>
    public void CreateTrack()
    {
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
        CTrackTile tTrackTile = null;
        SetCurrentTrack(TRACKKIND.START);
        //tTrackTile = GameObject.Instantiate<CTrackTile>(LoadAboutMap.GetPrefab(TRACKKIND.START), Vector3.zero, Quaternion.identity);
        Trackist.Add(GetTrackCount(),GetCurrentTrack());

        //SetTrackList(tTrackTile);
        AddTrackCount();

        BeforePos = tTrackTile.transform.position;
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
        CTrackTile tTrackTile = null;

        if (tNextTrackKind != TRACKKIND.TURN)
        {
            // tTrackTile = GameObject.Instantiate<CTrackTile>(LoadAboutMap.TrackKind[tNextTrackKind], NextPos, Quaternion.identity);
            Trackist.Add(GetTrackCount(), GetCurrentTrack());


            if (tNextTrackKind != TRACKKIND.HORIZONTAL)
            {
                CurrentDirection = tTrackTile.Direction;
            }
            SetCurrentTrack(tNextTrackKind);
        }
        else
        {

            if (Vector3.right == CurrentDirection)
            {
                // tTrackTile = GameObject.Instantiate<CTrackTile>(LoadAboutMap.GetPrefab(TRACKKIND.RIGHTUP), NextPos, Quaternion.identity);
                Trackist.Add(GetTrackCount(), GetCurrentTrack());

                SetCurrentTrack(TRACKKIND.RIGHTUP);

            }
            else
            {
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
    }


    /// <summary>
    /// 커브트랙 다음에 생성되는 트랙이 최소 3개이상 직선트랙이 나오게 생성하는 메소드
    /// </summary>
    /// <param name="tTrackTile">얘는 트랙파츠ㅇㅇ</param>
    public void CreateStraightTrack(CTrackTile tTrackTile)
    {
        tTrackTile = null;

        for (int ti = 0; ti < STRAIGHT_COUNT; ti++)
        {
            if (TOTAL_TRACK > TrackCount)
            {
                if (TRACKKIND.LEFTUP == GetCurrentTrack() || TRACKKIND.RIGHTUP == GetCurrentTrack())
                {
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
               
                
                
            }
        }

    }

    /// <summary>
    /// 마지막트랙 생성 메소드
    /// </summary>
    /// <param name="tTrackParts">얘는 트랙파츠라니깐?</param>
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
        }


    }

    public void AddTrackCount()
    {
        TrackCount = TrackCount + 1;
    }

    public int GetTrackCount()
    {
        return TrackCount;
    }

    public void SetTrackList(CTrackTile tTrackTile)
    {
        TrackListSemple[GetTrackCount()] = tTrackTile;
        //tTrackTile.SetIndex(GetTrackCount());
    }

    public TRACKKIND GetCurrentTrack()
    {
        return CurrentTrack;
    }

    public void SetCurrentTrack(TRACKKIND tTrackKind)
    {
        CurrentTrack = tTrackKind;
    }
}
