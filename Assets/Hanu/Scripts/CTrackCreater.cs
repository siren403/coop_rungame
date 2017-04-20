using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inspector;

public class CTrackCreater { 

    public const int TOTAL_TRACK = 50;
    public const int END_TRACK_COUNT = 1;
    public const int TRACK_SIZE = 18;
    public const int STRAIGHT_COUNT = 5;
    public const int STARTING_TRACK = 5;


    public const int VERTICAL_COUNT = 5;
    public const int HORIZONTAL_COUNT = 5;
    public const int UPLEFT_COUNT = 5;
    public const int LEFTUP_COUNT = 5;
    public const int UPRIGHT_COUNT = 5;
    public const int RIGHTUP_COUNT = 5;

    private const int mPrefabsCount = VERTICAL_COUNT + HORIZONTAL_COUNT + UPLEFT_COUNT + LEFTUP_COUNT + UPRIGHT_COUNT + RIGHTUP_COUNT + 2;


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

    // 다음 트랙 정보에 관한 것.
    public int RandomNum;
    public int TrackCount = 0;
    public TRACKKIND CurrentTrack;
    public Dictionary<TRACKKIND, List<TRACKKIND>> NextTrackKind = null;


    //생성해야될 트랙들의 정보를 담고있는 리스트.
    public Dictionary<int,TRACKKIND> TrackList = null;

    public Dictionary<TRACKKIND, List<CTrackTile>> TrackStorage = null;

    private Transform mTrackParent = null;

    private int mSight = 1;
    /// <summary>
    /// 트랙을 설치하는 메소드
    /// </summary>
    public void CreateTrack(Transform tParent)
    {
        mTrackParent = tParent;

        this.CreateNextTrackKind();
        SetTrackList();


        for(int ti = 0; ti < TrackList.Count;ti++)
        {
            Debug.Log(TrackList[ti]);
        }



        //타일프리팹들을 불러옴.
        LoadAboutMap = new CLoadAboutMap();
        LoadAboutMap.LoadAboutPrefabs();

        TrackStorage = new Dictionary<TRACKKIND, List<CTrackTile>>();
        var tTrackKinds = System.Enum.GetValues(typeof(TRACKKIND)).GetEnumerator();
        while(tTrackKinds.MoveNext())
        {
            TRACKKIND tKind = (TRACKKIND)tTrackKinds.Current;
            int createCount = 0;

            if(tKind == TRACKKIND.START || tKind == TRACKKIND.END)
            {
                createCount = 1;
            }
            else if(tKind != TRACKKIND.TURN)
            {
                createCount = 5;
            }

            for (int i = 0; i < createCount; i++)
            {
                if (TrackStorage.ContainsKey(tKind) == false)
                    TrackStorage.Add(tKind, new List<CTrackTile>());

                if(LoadAboutMap.GetPrefab(tKind) == null)
                {
                    Debug.Log("Prefab is Null");
                }
                CTrackTile tile = GameObject.Instantiate(LoadAboutMap.GetPrefab(tKind), Vector3.zero, Quaternion.identity);
                tile.gameObject.SetActive(false);
                tile.transform.SetParent(tParent);
                TrackStorage[tKind].Add(tile);
            }
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




    public void SetTrackList()
    {
        TrackList = new Dictionary<int, TRACKKIND>();
        Vector3 tDirection = Vector3.zero;

        SetCurrentTrack(TRACKKIND.START);
        TrackList.Add(GetTrackCount(), GetCurrentTrack());
        AddTrackCount();

        for (TrackCount = 1; TrackCount < TOTAL_TRACK;)
        {
            var tNextTrackList = NextTrackKind[CurrentTrack];
            var tNextTrackKind = tNextTrackList[Random.Range(0, tNextTrackList.Count)];

            if (tNextTrackKind != TRACKKIND.TURN)
            {
                SetCurrentTrack(tNextTrackKind);
                TrackList.Add(GetTrackCount(), GetCurrentTrack());


                if (tNextTrackKind == TRACKKIND.UPRIGHT)
                {
                    tDirection = Vector3.right;

                }
                else if(tNextTrackKind == TRACKKIND.UPLEFT)
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
                if (TOTAL_TRACK > TrackCount)
                {
                    if (TRACKKIND.LEFTUP == GetCurrentTrack() || TRACKKIND.RIGHTUP == GetCurrentTrack())
                    {
                        TrackList.Add(GetTrackCount(), TRACKKIND.VERTICAL);
                        AddTrackCount();

                    }
                    else if (TRACKKIND.UPLEFT == GetCurrentTrack() || TRACKKIND.UPRIGHT == GetCurrentTrack())
                    {
                        TrackList.Add(GetTrackCount(), TRACKKIND.HORIZONTAL);
                        AddTrackCount();
                    }
                }  
            }
        }


        SetCurrentTrack(TRACKKIND.END);
        TrackList.Add(GetTrackCount(), GetCurrentTrack());
        AddTrackCount();

    }




    /// <summary>
    /// 시작트랙를 먼저 설치를 한다.
    /// </summary>
    public void CreateStartTrack()
    {

        //CTrackTile tTrackTile = null;
        SetCurrentTrack(TRACKKIND.START);
        //tTrackTile = GameObject.Instantiate<CTrackTile>(LoadAboutMap.GetPrefab(TRACKKIND.START), Vector3.zero, Quaternion.identity);
        TrackList.Add(GetTrackCount(),GetCurrentTrack());

        //SetTrackList(tTrackTile);
        AddTrackCount();

        /*
        BeforePos = tTrackTile.transform.position;

        NextPos = Vector3.zero;
        CurrentDirection = Vector3.forward;
        NextPos = CurrentDirection * TRACK_SIZE;
        */
    }



    /// <summary>
    /// 트랙을 직접적으로 조립하는 메소드
    /// </summary>
    public CTrackTile DistinguishTrack(TRACKKIND tTrackKind)
    {
        var tTile = GetTrackTile(tTrackKind);

        tTile.transform.position = NextPos + (CurrentDirection * TRACK_SIZE);
        NextPos = tTile.transform.position;

        if (tTrackKind != TRACKKIND.HORIZONTAL)
        {
            CurrentDirection = tTile.Direction;
        }


        tTile.gameObject.SetActive(true);
        return tTile;
    }

    private CTrackTile GetTrackTile(TRACKKIND tTrackKind)
    {
        CTrackTile tTile = null;

        if(TrackStorage.ContainsKey(tTrackKind))
        {
            for (int i = 0; i < TrackStorage[tTrackKind].Count; i++)
            {
                if(TrackStorage[tTrackKind][i].gameObject.activeSelf == false)
                {
                    tTile = TrackStorage[tTrackKind][i];
                    return tTile;
                }
            }

            tTile = GameObject.Instantiate(LoadAboutMap.GetPrefab(tTrackKind), Vector3.zero, Quaternion.identity);
            tTile.gameObject.SetActive(false);
            tTile.transform.SetParent(mTrackParent);
            TrackStorage[tTrackKind].Add(tTile);
            return tTile;
        }

        return null;
    }
   // private Queue<int> ActiveTrackTileIndex = new Queue<int>();
   // private Queue<CTrackTile> ActiveTrackTile = new Queue<CTrackTile>();

    public void UpdateTrack(int index)
    {

        for (int i = 0; i < mSight; i++)
        {
            if (index + i < TrackList.Count)
            {
                TRACKKIND tKind = TrackList[index + i];
                /*
                if (ActiveTrackTileIndex.Contains(index + i) == false)
                {
                    CTrackTile tTile = DistinguishTrack(tKind);
                    ActiveTrackTileIndex.Enqueue(index + i);

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
                */
                CTrackTile tTile = DistinguishTrack(tKind);

            }
            else
            {
                break;
            }
        }
    }
   

    /// <summary>
    /// 커브트랙 다음에 생성되는 트랙이 최소 3개이상 직선트랙이 나오게 생성하는 메소드
    /// </summary>

    /// <param name="tTrackTile">얘는 트랙파츠ㅇㅇ</param>
    public void CreateStraightTrack()
    {
        //tTrackTile = null;


        for (int ti = 0; ti < STRAIGHT_COUNT; ti++)
        {
            if (TOTAL_TRACK > TrackCount)
            {
                if (TRACKKIND.LEFTUP == GetCurrentTrack() || TRACKKIND.RIGHTUP == GetCurrentTrack())
                {

                    //tTrackTile = GameObject.Instantiate<CTrackTile>(LoadAboutMap.GetPrefab(TRACKKIND.VERTICAL), NextPos, Quaternion.identity);
                    TrackList.Add(GetTrackCount(), TRACKKIND.VERTICAL);


                    //SetTrackList(tTrackTile);
                    AddTrackCount();

                    //BeforePos = tTrackTile.transform.position;
                    //NextPos = BeforePos + CurrentDirection * TRACK_SIZE;
                }
                else if (TRACKKIND.UPLEFT == GetCurrentTrack() || TRACKKIND.UPRIGHT == GetCurrentTrack())
                {
                    //tTrackTile = GameObject.Instantiate<CTrackTile>(LoadAboutMap.GetPrefab(TRACKKIND.HORIZONTAL), NextPos, Quaternion.identity);
                    TrackList.Add(GetTrackCount(), TRACKKIND.HORIZONTAL);


                    //SetTrackList(tTrackTile);
                    AddTrackCount();

                    //BeforePos = tTrackTile.transform.position;
                    //NextPos = BeforePos + CurrentDirection * TRACK_SIZE;
                }
               
                
                

            }
        }

    }

    /// <summary>
    /// 마지막트랙 생성 메소드
    /// </summary>
    /// <param name="tTrackParts">얘는 트랙파츠라니깐?</param>

    public void CreateEndTrack()
    {
        if (TOTAL_TRACK == TrackCount)
        {
            //tTrackTile = GameObject.Instantiate<CTrackTile>(LoadAboutMap.GetPrefab(TRACKKIND.END), NextPos, Quaternion.identity);
            SetCurrentTrack(TRACKKIND.END);
            TrackList.Add(GetTrackCount(), GetCurrentTrack());
            /*
            if (CurrentDirection == Vector3.right)
            {
                tTrackTile.transform.Rotate(new Vector3(0.0f, 90.0f, 0.0f));
            }
            else if (CurrentDirection == Vector3.left)
            {
                tTrackTile.transform.Rotate(new Vector3(0.0f, -90.0f, 0.0f));
            }
            */
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
       // TrackCreateList[GetTrackCount()] = tTrackTile;
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
