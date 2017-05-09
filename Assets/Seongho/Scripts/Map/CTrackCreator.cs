//#define DIFFCULTY
#define LOW
//#define MEDIUM

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Map
{
    public enum TrackType
    {
        NONE = 0,
        END,
        EMPTY,
        A, B, C, D, E, F, G,
        H, I, J, K, L, M, N,
        O, P, Q, R, S, T, U,
        V, W, X, Y, Z,
    }

    public class CTrackCreator
    {

        private int mTileCount = 70;
        public int TileCount
        {
            set
            {
                mTileCount = value;
            }
            get
            {
                return mTileCount;
            }
        }
        private int mSight = 6;


        private List<TrackType> mTrackPlaceData = new List<TrackType>();
        private Dictionary<TrackType, CTrack> CurrentPFTracks
        {
            get
            {
                return mPFTrackList[Mathf.Clamp(mCurrentPFTrackIndex, 0, mPFTrackList.Count - 1)];
            }
        }
        private int mCurrentPFTrackIndex = 0;
        private int mStageIndex = 0;
        private List<Dictionary<TrackType, CTrack>> mPFTrackList = new List<Dictionary<TrackType, CTrack>>();

        private Dictionary<int,CTile> mInstTileList = new Dictionary<int, CTile>();
        private Queue<CTile> mShowTileQueue = new Queue<CTile>();

        private Transform mParent = null;
        private int mCurrentPivot = -1;
        private int mInstTrackIndex = 0;
        public int CurrentPivot
        {
            get
            {
                return mCurrentPivot - (73 * mStageIndex);
            }
        }
        public int TrackCount
        {
            get
            {
                return mInstTrackIndex - (73 * mStageIndex);
            }
        }

        public const int END_NEXT_TILE_COUNT = 3;
        public float TrackProgress
        {
            get
            {
                if (mCurrentPivot >= 0)
                {
                    return (float)(mCurrentPivot - (73 * mStageIndex)) /(mInstTrackIndex - (73 * mStageIndex));
                }
                return 0.0f;
            }
        }

        private System.Action<int,int> mOnShowEndTrack = null;
        public System.Action<int, int> OnShowEndTrack
        {
            set
            {
                mOnShowEndTrack = value;
            }
        }
        private System.Action<int,int> mOnChangeStage = null;
        public System.Action<int,int> OnChangeStage
        {
            set
            {
                mOnChangeStage = value;
            }
        }
        private Vector3 mTrackInstancePosition = Vector3.zero;
        private bool mIsNextTheme = false;

        private Stack<int> mThemeStack = new Stack<int>();

        private int LeftThemeIndex = 0;
        private int RightThemeIndex = 0;

        private CTrackData mTrackData = null;

        public CTrackCreator(Transform tParent)
        {
            mParent = tParent;
            mPFTrackList.Add(LoadThemePFTrack("Tracks/Theme1"));
            mPFTrackList.Add(LoadThemePFTrack("Tracks/Theme2"));
            mPFTrackList.Add(LoadThemePFTrack("Tracks/Theme3"));

            mTrackData = Resources.Load<CTrackData>("GameData/CTrackData");

            List<int> tStageNumbers = new List<int>() { 1, 2 };
            for (int i = 0; i < 2; i++)
            {
                int index = UnityEngine.Random.Range(0, tStageNumbers.Count);
                mThemeStack.Push(tStageNumbers[index]);
                tStageNumbers.RemoveAt(index);
            }
            mCurrentPFTrackIndex = 0;
        }

        private Dictionary<TrackType, CTrack> LoadThemePFTrack(string path)
        {
            Dictionary<TrackType, CTrack> storage = new Dictionary<TrackType, CTrack>();
            var prefabs = Resources.LoadAll<CTrack>(path);
            foreach (var track in prefabs)
            {
                if (storage.ContainsKey(track.Type) == false)
                {
                    storage.Add(track.Type, track);
                }
            }
            return storage;
        }

        public void CreateTrackData()
        {

            List<TrackType> tTypes = new List<TrackType>();
            tTypes.Add(TrackType.E);

            mTrackPlaceData.Clear();

            bool tIsStartRoad = false;
            bool tIsEndRoad = false;

            for (int i = 0; i < mTileCount - 1;)
            {
                TrackType addTrack = tTypes[UnityEngine.Random.Range(0, tTypes.Count)];
                mTrackPlaceData.Add(addTrack);

                if (CurrentPFTracks.ContainsKey(addTrack))
                {
                    i += CurrentPFTracks[addTrack].TileCount;
                }
                else
                {
                    i++;
                }

                if (tIsStartRoad == false && i >= 5)
                {
                    tIsStartRoad = true;
                    List<TrackType> tTrackDiffcultyList = null;
#if DIFFCULTY
                    tTrackDiffcultyList = mTrackData.DiffcultyList[Mathf.Clamp(mStageIndex, 0, mTrackData.DiffcultyList.Count - 1)];
#elif LOW
                    tTrackDiffcultyList = new List<TrackType>();
                    for (int start = (int)TrackType.A; start <= (int)TrackType.F; start++)
                    {
                        tTrackDiffcultyList.Add((TrackType)start);
                    }
#elif MEDIUM
                    tTrackDiffcultyList = new List<TrackType>();
                    for (int start = (int)TrackType.A; start <= (int)TrackType.P; start++)
                    {
                        tTrackDiffcultyList.Add((TrackType)start);
                    }
#endif


                    tTypes.AddRange(tTrackDiffcultyList);

                }
                else if (tIsEndRoad == false && i >= mTileCount - 5)
                {
                    tIsEndRoad = true;
                    tTypes.Clear();
                    tTypes.Add(TrackType.EMPTY);
                }
            }
            mTrackPlaceData.Add(TrackType.END);
            for (int i = 0; i < 3; i++)
            {
                mTrackPlaceData.Add(TrackType.EMPTY);
            }

        }

        public void PositionTracks()
        {
         
            foreach(var tTrackType in mTrackPlaceData)
            {
                if (CurrentPFTracks.ContainsKey(tTrackType))
                {
                    CTrack tTrack = GameObject.Instantiate<CTrack>(CurrentPFTracks[tTrackType], mTrackInstancePosition, Quaternion.identity);
                    tTrack.transform.SetParent(mParent);
                    mTrackInstancePosition += Vector3.forward * CurrentPFTracks[tTrackType].TrackLength;


                    tTrack.DisableTiles();
                    tTrack.gameObject.name = string.Format("[{0}] {1}", mInstTrackIndex, tTrack.gameObject.name);

                    foreach (var tile in tTrack.InstTileList)
                    {
                        mInstTileList.Add(mInstTrackIndex, tile);
                        tile.Init(this, mInstTrackIndex, tTrackType);
                        mInstTrackIndex++;
                    }

                }
            }
            mOnChangeStage.SafeInvoke(mStageIndex + 1, mCurrentPFTrackIndex);

        }

        public void UpdateTrackTile(int pivot)
        {
            if(mCurrentPivot >= pivot)
            {
                return;
            }
            mCurrentPivot = pivot;

            int start = Mathf.Clamp(pivot - 1, 0, mInstTrackIndex);
            int end = Mathf.Clamp(start + mSight, 0, mInstTrackIndex + 1);

            for (int i = start; i < end; i++)
            {

                if(mShowTileQueue.Contains(mInstTileList[i]) == false)
                {
                    mInstTileList[i].Show();

                    if(mInstTileList[i].GetTrackType() == TrackType.END)
                    {
                        if (mThemeStack.Count > 0)
                            LeftThemeIndex = mThemeStack.Pop();
                        else
                            LeftThemeIndex = mCurrentPFTrackIndex;

                        if (mThemeStack.Count > 0)
                            RightThemeIndex = mThemeStack.Pop();
                        else
                            RightThemeIndex = mCurrentPFTrackIndex;


                        mOnShowEndTrack.SafeInvoke(LeftThemeIndex,RightThemeIndex);
                        mIsNextTheme = true;
                    }

                    mShowTileQueue.Enqueue(mInstTileList[i]);
                    if(mShowTileQueue.Count > mSight)
                    {
                        mShowTileQueue.Dequeue().Hide();
                    }
                }
                else // 이미 보여지고 있음
                {

                }
            }
        }
        public CTile GetTile(int index)
        {
            CTile tTile = null;

            if (index >= 0 && mInstTileList.Count > index)
            {
                tTile = mInstTileList[index];
            }
            return tTile;
        }

        public void OnSelectNextTheme(int select)
        {
            if (mIsNextTheme)
            {
                if (select == -1)
                {
                    mCurrentPFTrackIndex = LeftThemeIndex;
                    if (mStageIndex < 1)
                    {
                        mThemeStack.Push(RightThemeIndex);
                    }
                }
                else if (select == 1)
                {
                    mCurrentPFTrackIndex = RightThemeIndex;
                    if (mStageIndex < 1)
                    {
                        mThemeStack.Push(LeftThemeIndex);
                    }
                }
                foreach(var num in mThemeStack)
                {
                    Debug.Log("theme num : " + num);
                }

                mIsNextTheme = false;
                int start = (70 - 2) * mStageIndex;
                int end = (70 - 2) * (mStageIndex + 1);

                foreach(var tile in mInstTileList)
                {
                    if(tile.Value.GetTrackType() == TrackType.END)
                    {
                        end = tile.Value.Index-1;
                        break;
                    }
                }

                Debug.Log(end);
                if (mStageIndex > 0)
                {
                    end += 4 + (mStageIndex);
                }
                for (int i = start; i < end; i++)
                {
                    if (mInstTileList.ContainsKey(i))
                    {
                        mInstTileList[i].TileDestroy();
                        mInstTileList.Remove(i);
                    }
                }
                mStageIndex++;
                CreateTrackData();
                PositionTracks();
            }
        }

        
    }
}
