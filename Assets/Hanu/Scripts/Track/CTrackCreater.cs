using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inspector;
using ResourceLoader;

public class CTrackCreater { 

    public const int TOTAL_TRACK = 70;
    public const int END_TRACK_COUNT = 6;

    public const int TRACK_SIZE = 18;


    public CTrackTileLoader TrackTileLoader
    {
        get
        {
            return TrackTileLoaderList[CurrentTrackTileLoaderIndex];
        }
    } 
    public List<CTrackTileLoader> TrackTileLoaderList = null;
    public int CurrentTrackTileLoaderIndex = 0;

    public enum NEXTROTATION
    {
        LEFT = -1,
        RIGHT = 1,
    }

    public enum TRACKKIND
    {
        START = 0,
        A = 1,
        B = 2,
        C = 3,
        D = 4,
        VERTICAL = 5,
        E = 6,
        F = 7,
        G = 8,
        END = 9,
    }

    public NEXTROTATION NextRotantion;

    //트랙타일을 조립하기 위해 쓰이는 위치값들
    public Vector3 BeforePos;
    public Vector3 NextPos;
    public Vector3 CurrentDirection;

    // 다음 트랙 정보에 관한 것.
    public int RandomNum;
    public int TrackCount = 0;
    public TRACKKIND CurrentTrack;
   // public Dictionary<TRACKKIND, List<TRACKKIND>> NextTrackKind = null;


    //생성해야될 트랙들의 정보를 담고있는 리스트.
    public Dictionary<int,TRACKKIND> TrackList = null;


    //지정된 곳에 자식으로 넣을 위치
    private Transform mTrackParent = null;

    public int PlayerIndex = 0;

    public int StageNumber = 0;

    public CTrackTile PFTrackA = null;

   /// <summary>
   /// 트랙이 생성 될 수 있도록 데이터를 생성하는 메소드
   /// </summary>
   /// <param name="tParent">트랙타일 프리팹이 어느자식으로 들어가는지 지정해주는 변수</param>
    public void CreateTrack(Transform tParent)
    {
        mTrackParent = tParent;

        SetTrackList();

        //설치될 트랙타일들의 정보들을 로그를 찍어봄
        for(int ti = 0; ti < TOTAL_TRACK+1;ti++)
        {
            if (TrackList.ContainsKey(ti) == true)
            {
                Debug.Log(TrackList[ti]);
                Debug.Log(ti);
            }
        }
        Debug.Log(GetTrackCount().ToString());

        
        TrackTileLoaderList = new List<CTrackTileLoader>();
        TrackTileLoaderList.Add(new CTheme0TileLoader());
        TrackTileLoaderList.Add(new CTheme1TileLoader());
        TrackTileLoaderList.Add(new CTheme2TileLoader());
        TrackTileLoaderList.Add(new CTheme3TileLoader());

        foreach(var loader in TrackTileLoaderList)
        {
            loader.Load();
            loader.InitTrackStorage(tParent, this);
        }

        CurrentTrackTileLoaderIndex =2;
        
        //타일프리팹들을 불러옴.
        //TrackTileLoader = new CTheme0TileLoader();
        //TrackTileLoader.Load();

        //트랙창고로 트랙타일들을 미리 만들어서 둔다.
        //TrackTileLoader.InitTrackStorage(tParent);

    }

    /*
    /// <summary>
    /// 현재트랙에서 다음 트랙타일이 나올 수 있는지를 담는 리스트 정보 메소드
    /// </summary>
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
    */

    
    /// <summary>
    /// 랜덤배치할 트랙타일들의 정보를 셋팅하는 메소드
    /// </summary>
    public void SetTrackList()
    {
        TrackList = new Dictionary<int, TRACKKIND>();

        SetCurrentTrack(TRACKKIND.START);
        TrackList.Add(GetTrackCount(), GetCurrentTrack());
        AddTrackCount();
        for (int ti = 0; ti < 4; ti++)
        {
            SetCurrentTrack(TRACKKIND.VERTICAL);
            TrackList.Add(GetTrackCount(), GetCurrentTrack());
            AddTrackCount();
        }


        for (TrackCount = GetTrackCount(); GetTrackCount() < TOTAL_TRACK-4; AddTrackCount())
        {

            int tNextTrackKind = 0;
           
            if (GetTrackCount() < 60)
            {
                tNextTrackKind = Random.Range(1, 5);
            }
            else
            {
                tNextTrackKind = Random.Range(5, 8);
            }

            var tNextTrack = (TRACKKIND)tNextTrackKind;
            SetCurrentTrack(tNextTrack);
            TrackList.Add(GetTrackCount(), GetCurrentTrack());
            if (1 <= tNextTrackKind && 4 >= tNextTrackKind)
            {
                for (int ti = 0; ti < 3; ti++)
                {
                    AddTrackCount();
                }
               
            }
          
        }
        for (int ti = 0; ti < 4; ti++)
        {
            SetCurrentTrack(TRACKKIND.VERTICAL);
            TrackList.Add(GetTrackCount(), GetCurrentTrack());
            AddTrackCount();
        }

        SetCurrentTrack(TRACKKIND.END);
        TrackList.Add(GetTrackCount(), GetCurrentTrack());
        AddTrackCount();

    }


    /// <summary>
    /// 원하는 트랙타일을 가져와 위치를 지정하고 화면에 보이게 해줍니다.
    /// </summary>
    /// <param name="tTrackKind">원하는 트랙타일</param>
    /// <returns>화면에 보이게 되는 타일을 반환</returns>
    public CTrackTile DistinguishTrack(TRACKKIND tTrackKind, int tIndex)
    {
        var tTile = TrackTileLoader.GetTrackTile(tTrackKind);
        
        switch (tTrackKind)
        {
            case TRACKKIND.A:
            case TRACKKIND.B:
            case TRACKKIND.C:
            case TRACKKIND.D:
                var tTilecomponets = tTile.GetComponentsInChildren<CTrackTile>();
                for (int ti = 0; ti < 4; ti++)
                {
                    tTilecomponets[ti].SetIndex(tIndex + ti);
                }
                tTile.transform.position = NextPos + Vector3.forward * 72;
                NextPos = tTile.transform.position;
                break;
            default:
                tTile.SetIndex(tIndex);
                tTile.transform.position = NextPos + Vector3.forward * TRACK_SIZE;
                NextPos = tTile.transform.position;
                break;
        }


        tTile.Show();
        return tTile;
    }


    /*
    /// <summary>
    /// 트랙창고리스트에서 원하는 종류의 트랙타일을 받아오는 메소드 IF 창고안에 비활성화 된 트랙타일이 없을시 생성하여 가져옵니다.
    /// </summary>
    /// <param name="tTrackKind">원하는 트랙의 종류</param>
    /// <returns>원하는 트랙타일을 반환</returns>
    private CTrackTile GetTrackTile(TRACKKIND tTrackKind)
    {
        CTrackTile tTile = null;

        if(TrackStorage0.ContainsKey(tTrackKind))
        {
            for (int i = 0; i < TrackStorage0[tTrackKind].Count; i++)
            {
                if(TrackStorage0[tTrackKind][i].gameObject.activeSelf == false)
                {
                    tTile = TrackStorage0[tTrackKind][i];
                    return tTile;
                }
            }

            tTile = GameObject.Instantiate(TrackTileLoader.GetPrefab(tTrackKind), Vector3.zero, Quaternion.identity);
            tTile.gameObject.SetActive(false);
            tTile.transform.SetParent(mTrackParent);
            TrackStorage0[tTrackKind].Add(tTile);
            return tTile;
        }

        return null;
    }
    */



    //플레이어 위치에 따른 보여지는 값 = mSight
    private int mSight = 9;
    //한번 활성화 된 트랙타일의 인덱스값을 갖는 자료구조
    private Queue<int> ActiveTrackTileIndex = new Queue<int>();
    //화면에 보이는 트랙타일을 갖고있는 자료구조
    private Queue<CTrackTile> ActiveTrackTile = new Queue<CTrackTile>();


    /// <summary>
    /// 플레이어 위치값에 따른 트랙타일을 화면에서 꺼주는 메소드
    /// </summary>
    /// <param name="tPlayerPositionIndex">플레이어의 위치값</param>
    public void UpdateTrack(int tPlayerPositionIndex)
    {

        for (int ti = 0; ti < mSight; ti++)
        {
            if (tPlayerPositionIndex + ti < TrackList.Count)
            {
                TRACKKIND tKind = TrackList[tPlayerPositionIndex + ti];
                
                if (ActiveTrackTileIndex.Contains(tPlayerPositionIndex + ti) == false)
                {

                    CTrackTile tTile = DistinguishTrack(tKind, tPlayerPositionIndex+ti);

                    ActiveTrackTileIndex.Enqueue(tPlayerPositionIndex + ti);


                    if(ActiveTrackTile.Count < mSight)
                    {
                        ActiveTrackTile.Enqueue(tTile);
                    }
                    else
                    {
                        ActiveTrackTile.Dequeue().Hide();
                        ActiveTrackTile.Enqueue(tTile);
                    }
                }
            }
            else
            {
                break;
            }
        }
        if(tPlayerPositionIndex == TrackList.Count-1)
        {
            Debug.Log("여긴옴?");
            AddStageNumber();
            ReSetData();
        }
       
    }

   

    public void SetNextStage(NEXTROTATION tNEXTROTATION)
    {
        NextRotantion = tNEXTROTATION;
    }

    public void ReSetTrackList(int tPlayerPositionIndex)
    {
            Debug.Log("리셋??");
    }

    public void ReSetData()
    {
        
        while(ActiveTrackTile.Count > 3)
        {
            ActiveTrackTile.Dequeue().Hide();
        }
        ActiveTrackTileIndex.Clear();
        
        ReSetTrackCount();
    }



   

    public void AddStageNumber()
    {
        StageNumber++;
    }

    public int GetStageNumber()
    {
        return StageNumber;
    }




    public void SetPlayerIndex(int tIndex)
    {
        PlayerIndex = tIndex;
    }

    public int GetPlayerIndex()
    {
        return PlayerIndex;
    }

    public void AddTrackCount()
    {
        TrackCount = TrackCount + 1;
    }

    public void ReSetTrackCount()
    {
        TrackCount = 0;
    }

    public int GetTrackCount()
    {
        return TrackCount;
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
