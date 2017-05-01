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


        private List<TrackType> mTrackData = new List<TrackType>();
        private Dictionary<TrackType, CTrack> CurrentPFTracks
        {
            get
            {
                return mPFTrackList[Mathf.Clamp(mCurrentPFTrackIndex, 0, mPFTrackList.Count - 1)];
            }
        }
        private int mCurrentPFTrackIndex = 0;
        private List<Dictionary<TrackType, CTrack>> mPFTrackList = new List<Dictionary<TrackType, CTrack>>();

        private Dictionary<int,CTile> mInstTileList = new Dictionary<int, CTile>();
        private Queue<CTile> mShowTileQueue = new Queue<CTile>();

        private Transform mParent = null;
        private int mCurrentPivot = -1;
        private int mInstTrackIndex = 0;

        public const int END_NEXT_TILE_COUNT = 3;
        public float TrackProgress
        {
            get
            {
                if (mCurrentPivot >= 0)
                {
                    return (float)(mCurrentPivot - (73 * mCurrentPFTrackIndex)) /(mInstTrackIndex - (73 * mCurrentPFTrackIndex));
                }
                return 0.0f;
            }
        }

        private System.Action mOnShowEndTrack = null;
        private Vector3 mTrackInstancePosition = Vector3.zero;
        private bool mIsNextTheme = false;


        public CTrackCreator(Transform tParent)
        {
            mParent = tParent;
            mPFTrackList.Add(LoadThemePFTrack("Tracks/Theme1"));
            mPFTrackList.Add(LoadThemePFTrack("Tracks/Theme2"));
            mPFTrackList.Add(LoadThemePFTrack("Tracks/Theme3"));
            mPFTrackList.Add(LoadThemePFTrack("Tracks/Theme4"));
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
                    //Debug.Log(string.Format("Load Prefab {0}", track.Type));
                }
            }
            return storage;
        }

        public void CreateTrackData()
        {
            //TrackType[] types = (TrackType[])Enum.GetValues(typeof(TrackType));

            List<TrackType> tTypes = new List<TrackType>();
            tTypes.Add(TrackType.E);

            //mTrackData.Capacity = mTileCount;
            mTrackData.Clear();

            bool tIsStartRoad = false;
            bool tIsEndRoad = false;

            for (int i = 0; i < mTileCount - 1;)
            {
                TrackType addTrack = tTypes[UnityEngine.Random.Range(0, tTypes.Count)];
                mTrackData.Add(addTrack);

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

                    for (int tTrackValue = (int)TrackType.A; tTrackValue < (int)TrackType.Z; tTrackValue++)
                    {
                        if (CurrentPFTracks.ContainsKey((TrackType)tTrackValue))
                        {
                            tTypes.Add((TrackType)tTrackValue);
                            //Debug.Log((TrackType)tTrackValue);
                        }
                    }

                }
                else if (tIsEndRoad == false && i >= mTileCount - 5)
                {
                    tIsEndRoad = true;
                    tTypes.Clear();
                    tTypes.Add(TrackType.EMPTY);
                }
            }
            mTrackData.Add(TrackType.END);
            for (int i = 0; i < 3; i++)
            {
                mTrackData.Add(TrackType.EMPTY);
            }

        }

        public void PositionTracks()
        {
         
            foreach(var tTrackType in mTrackData)
            {
                if (CurrentPFTracks.ContainsKey(tTrackType))
                {
                    CTrack tTrack = GameObject.Instantiate<CTrack>(CurrentPFTracks[tTrackType], mTrackInstancePosition, Quaternion.identity);
                    tTrack.transform.SetParent(mParent);
                    mTrackInstancePosition += Vector3.forward * CurrentPFTracks[tTrackType].TrackLength;


                    tTrack.DisableTiles();

                    foreach (var tile in tTrack.InstTileList)
                    {
                        mInstTileList.Add(mInstTrackIndex, tile);
                        tile.Init(this, mInstTrackIndex, tTrackType);
                        mInstTrackIndex++;
                    }
                    tTrack.gameObject.name = string.Format("[{0}] {1}", mInstTrackIndex, tTrack.gameObject.name);

                }
            }
            Debug.Log(mInstTrackIndex);
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
                        mOnShowEndTrack.SafeInvoke();
                        mIsNextTheme = true;
                        Debug.Log("Show End Track");
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
                mIsNextTheme = false;
                int start = (70 - 2) * mCurrentPFTrackIndex;
                int end = (70 - 2) * (mCurrentPFTrackIndex + 1);
                if (mCurrentPFTrackIndex > 0)
                {
                    end += 4 + (mCurrentPFTrackIndex);
                }
                //Debug.Log(start + "/" + end);
                //Debug.Log(mCurrentPivot);
                for (int i = start; i < end; i++)
                {
                    if (mInstTileList.ContainsKey(i))
                    {
                        mInstTileList[i].TileDestroy();
                        mInstTileList.Remove(i);
                    }
                }
                Debug.Log("Destroy Tile Count : " + (end - start));
                Debug.Log(select);
                mCurrentPFTrackIndex++;
                CreateTrackData();
                PositionTracks();
            }
          
        }
        
    }
}
