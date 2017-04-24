using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inspector;
using ResourceLoader;

public class CTrackCreater { 

    public const int TOTAL_TRACK = 70;
    public const int END_TRACK_COUNT = 6;
    public const int NOT_CURVE_COUNT = 10;

    public const int TRACK_SIZE = 18;
    public const int STRAIGHT_COUNT = 5;
    public const int STARTING_TRACK = 5;


    public const int VERTICAL_COUNT = 5;
    public const int HORIZONTAL_COUNT = 5;
    public const int UPLEFT_COUNT = 5;
    public const int LEFTUP_COUNT = 5;
    public const int UPRIGHT_COUNT = 5;
    public const int RIGHTUP_COUNT = 5;

   


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
        LEFT = 1,
        RIGHT = -1,
    }

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

    public NEXTROTATION NextRotantion;

    //트랙타일을 조립하기 위해 쓰이는 위치값들
    public Vector3 BeforePos;
    public Vector3 NextPos;
    public Vector3 CurrentDirection;

    // 다음 트랙 정보에 관한 것.
    public int RandomNum;
    public int TrackCount = 0;
    public TRACKKIND CurrentTrack;
    public Dictionary<TRACKKIND, List<TRACKKIND>> NextTrackKind = null;


    //생성해야될 트랙들의 정보를 담고있는 리스트.
    public Dictionary<int,TRACKKIND> TrackList = null;
    //미리 준비된 트랙타일 프리팹을 담고있는 리스트.
    //public Dictionary<TRACKKIND, List<CTrackTile>> TrackStorage0 = null;
    //public Dictionary<TRACKKIND, List<CTrackTile>> TrackStorage1 = null;
    //public Dictionary<TRACKKIND, List<CTrackTile>> TrackStorage2 = null;
    //public Dictionary<TRACKKIND, List<CTrackTile>> TrackStorage3 = null;


    //지정된 곳에 자식으로 넣을 위치
    private Transform mTrackParent = null;

    public int PlayerIndex = 0;


   /// <summary>
   /// 트랙이 생성 될 수 있도록 데이터를 생성하는 메소드
   /// </summary>
   /// <param name="tParent">트랙타일 프리팹이 어느자식으로 들어가는지 지정해주는 변수</param>
    public void CreateTrack(Transform tParent)
    {
        mTrackParent = tParent;

        this.CreateNextTrackKind();
        SetTrackList();

        //설치될 트랙타일들의 정보들을 로그를 찍어봄
        for(int ti = 0; ti < TrackList.Count;ti++)
        {
            if (ti == 64)
                Debug.Log("=======================================");

            Debug.Log(TrackList[ti]);
            
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
            loader.InitTrackStorage(tParent);
        }

        CurrentTrackTileLoaderIndex = 2;

        //타일프리팹들을 불러옴.
        //TrackTileLoader = new CTheme0TileLoader();
        //TrackTileLoader.Load();

        //트랙창고로 트랙타일들을 미리 만들어서 둔다.
        //TrackTileLoader.InitTrackStorage(tParent);

    }


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


    
    /// <summary>
    /// 랜덤배치할 트랙타일들의 정보를 셋팅하는 메소드
    /// </summary>
    public void SetTrackList()
    {
        TrackList = new Dictionary<int, TRACKKIND>();
        Vector3 tDirection = Vector3.zero;
        CTrackCreater.TRACKKIND tCurrentTrarck = TRACKKIND.END;
        ////
        SetCurrentTrack(TRACKKIND.START);
        TrackList.Add(GetTrackCount(), GetCurrentTrack());
        AddTrackCount();
   
        for (TrackCount = 1; TrackCount < TOTAL_TRACK - END_TRACK_COUNT;)
        {
            var tNextTrackList = NextTrackKind[CurrentTrack];
            var tNextTrackKind = tNextTrackList[Random.Range(0, tNextTrackList.Count)];

            if (TOTAL_TRACK - NOT_CURVE_COUNT < GetTrackCount())
            {
                tNextTrackKind = tNextTrackList[0];
            }


            if (tNextTrackKind != TRACKKIND.TURN)
            {
                SetCurrentTrack(tNextTrackKind);
                TrackList.Add(GetTrackCount(), GetCurrentTrack());


                if (tNextTrackKind == TRACKKIND.UPRIGHT)
                {
                    tDirection = Vector3.right;

                }
                else if (tNextTrackKind == TRACKKIND.UPLEFT)
                {
                    tDirection = Vector3.left;
                }

            }
            else
            {
                if (Vector3.right == tDirection)
                {
                    SetCurrentTrack(TRACKKIND.RIGHTUP);
                    TrackList.Add(GetTrackCount(), GetCurrentTrack());
                }
                else
                {
                    SetCurrentTrack(TRACKKIND.LEFTUP);
                    TrackList.Add(GetTrackCount(), GetCurrentTrack());
                }
            }
            AddTrackCount();


            for (int ti = 0; ti < STRAIGHT_COUNT; ti++)
            {
                if (TOTAL_TRACK - END_TRACK_COUNT > GetTrackCount())
                {
                    tCurrentTrarck = TRACKKIND.END;
                    if (TRACKKIND.LEFTUP == GetCurrentTrack() || TRACKKIND.RIGHTUP == GetCurrentTrack())
                    {
                        TrackList.Add(GetTrackCount(), TRACKKIND.VERTICAL);
                        AddTrackCount();
                        tCurrentTrarck = TRACKKIND.VERTICAL;
                    }
                    else if (TRACKKIND.UPLEFT == GetCurrentTrack() || TRACKKIND.UPRIGHT == GetCurrentTrack())
                    {
                        TrackList.Add(GetTrackCount(), TRACKKIND.HORIZONTAL);
                        AddTrackCount();
                        tCurrentTrarck = TRACKKIND.HORIZONTAL;
                    }
                }
                else
                {
                    if (tCurrentTrarck != TRACKKIND.END)
                    {
                        Debug.Log("a11asd" + tCurrentTrarck.ToString());
                        SetCurrentTrack(tCurrentTrarck);
                    }
                    break;
                }
            }
            if (tCurrentTrarck != TRACKKIND.END)
            {
                Debug.Log("a11" + tCurrentTrarck.ToString());
                SetCurrentTrack(tCurrentTrarck);
            }
        }

          /*
          if(TRACKKIND.VERTICAL == GetCurrentTrack() || TRACKKIND.HORIZONTAL == GetCurrentTrack())
          {
              SetCurrentTrack(tCurrentTrarck);
          }
          */
        Debug.Log("지금?!" + GetCurrentTrack().ToString());
        
        switch (GetCurrentTrack())
        {
            case TRACKKIND.UPLEFT:
                for (int ti = 0; ti < STRAIGHT_COUNT; ti++)
                {
                    TrackList.Add(GetTrackCount(), TRACKKIND.HORIZONTAL);
                    AddTrackCount();
                }
                TrackList.Add(GetTrackCount(), TRACKKIND.LEFTUP);
                AddTrackCount();
                break;
            case TRACKKIND.UPRIGHT:
                for (int ti = 0; ti < STRAIGHT_COUNT; ti++)
                {
                    TrackList.Add(GetTrackCount(), TRACKKIND.HORIZONTAL);
                    AddTrackCount();
                }
                TrackList.Add(GetTrackCount(), TRACKKIND.RIGHTUP);
                AddTrackCount();
                break;
            case TRACKKIND.HORIZONTAL:
                if (Vector3.right == tDirection)
                {
                    SetCurrentTrack(TRACKKIND.RIGHTUP);
                    TrackList.Add(GetTrackCount(), GetCurrentTrack());
                }
                else
                {
                    SetCurrentTrack(TRACKKIND.LEFTUP);
                    TrackList.Add(GetTrackCount(), GetCurrentTrack());
                }
                AddTrackCount();
                break;
        }
        
        

        for (int ti = 0; ti < STRAIGHT_COUNT; ti++)
        {
             TrackList.Add(GetTrackCount(), TRACKKIND.VERTICAL);
             AddTrackCount();
        }

        SetCurrentTrack(TRACKKIND.END);
        TrackList.Add(GetTrackCount(), GetCurrentTrack());
        AddTrackCount();

        TrackListAddToEndReadyTrack();
    }


    /// <summary>
    /// 원하는 트랙타일을 가져와 위치를 지정하고 화면에 보이게 해줍니다.
    /// </summary>
    /// <param name="tTrackKind">원하는 트랙타일</param>
    /// <returns>화면에 보이게 되는 타일을 반환</returns>
    public CTrackTile DistinguishTrack(TRACKKIND tTrackKind, int tIndex)
    {
        var tTile = TrackTileLoader.GetTrackTile(tTrackKind);
        
        tTile.SetIndex(tIndex);

        if(tTrackKind == TRACKKIND.END)
        {
            SetNextStage(NEXTROTATION.LEFT);
            
            
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (TrackList.Count - 3 != tIndex)
        {
            tTile.transform.position = NextPos + (CurrentDirection * TRACK_SIZE);
            NextPos = tTile.transform.position;
        }
        else
        {
            
            Debug.Log("너나옴?");
            
            tTile.transform.position = NextPos + (Vector3.right * 54 ) * (int)NextRotantion + Vector3.forward * 54 ;
            NextPos = tTile.transform.position;
            CurrentDirection = Vector3.right*(int)NextRotantion;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (tTrackKind != TRACKKIND.HORIZONTAL)
        {
            if(tTrackKind != TRACKKIND.END)
                CurrentDirection = tTile.Direction;
        }
       
      

        tTile.gameObject.SetActive(true);
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
    private int mSight = 5;
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
                        ActiveTrackTile.Dequeue().gameObject.SetActive(false);
                        ActiveTrackTile.Enqueue(tTile);
                    }
                }
            }
            else
            {
                break;
            }
        }
        //EndNextTrackReady();
    }

   

    public void SetNextStage(NEXTROTATION tNEXTROTATION)
    {
        NextRotantion = tNEXTROTATION;
    }

    public void ReSetTrackList(int tPlayerPositionIndex)
    {
        if(TrackList.Count == tPlayerPositionIndex)
        {
            TrackList.Clear();

        }
    }


    public void TrackListAddToEndReadyTrack()
    {
        for (int ti = 0; ti < 3; ti++)
        {
            TrackList.Add(GetTrackCount(), TRACKKIND.HORIZONTAL);
            AddTrackCount();
        }
    }

    public void EndNextTrackReady()
    {
        if (ActiveTrackTileIndex.Contains(TrackList.Count) == true)
        {
            var tTile = TrackTileLoader.GetTrackTile(TRACKKIND.HORIZONTAL);

            tTile.transform.position = NextPos;// + (Vector3.right) * 100;// + (CurrentDirection*54);
            NextPos = tTile.transform.position;
            /*
            if (tTrackKind != TRACKKIND.HORIZONTAL)
            {
                if (tTrackKind != TRACKKIND.END)
                    CurrentDirection = tTile.Direction;
            }
            */

            tTile.gameObject.SetActive(true);
            ActiveTrackTile.Enqueue(tTile);
        }
    }

    ///////////////////
    
    



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
