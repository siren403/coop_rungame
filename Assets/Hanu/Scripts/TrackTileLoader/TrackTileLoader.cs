using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceLoader
{
    public abstract class CTrackTileLoader : ResourceLoader
    {
        public struct TilePaths
        {
            public string PathStartTrack;

            public string PathVerticalTrack;
            public string PathHorizontalTrack;

            public string PathUpLeftTrack;
            public string PathLeftUpTrack;
            public string PathUpRightTrack;
            public string PathRightUpTrack;

            public string PathEndTrack;
        }

        private CTrackTile PFStartTrack = null;

        private CTrackTile PFVerticalTrack = null;
        private CTrackTile PFHorizontalTrack = null;

        private CTrackTile PFUpLeftTrack = null;
        private CTrackTile PFLeftUpTrack = null;
        private CTrackTile PFUpRightTrack = null;
        private CTrackTile PFRightUpTrack = null;

        private CTrackTile PFEndTrack = null;

        private Dictionary<CTrackCreater.TRACKKIND, CTrackTile> TrackKind = null;

        public Dictionary<CTrackCreater.TRACKKIND, List<CTrackTile>> TrackStorage = null;

        private CTrackCreater mTrackCreater = null;
        private Transform mTrackParent;

        protected abstract TilePaths InitTilePaths();

        public override void Load()
        {
            TilePaths paths = InitTilePaths();

            PFStartTrack = Resources.Load<CTrackTile>(paths.PathStartTrack);

            PFVerticalTrack = Resources.Load<CTrackTile>(paths.PathVerticalTrack);
            PFHorizontalTrack = Resources.Load<CTrackTile>(paths.PathHorizontalTrack);

            PFUpLeftTrack = Resources.Load<CTrackTile>(paths.PathUpLeftTrack);
            PFLeftUpTrack = Resources.Load<CTrackTile>(paths.PathLeftUpTrack);
            PFUpRightTrack = Resources.Load<CTrackTile>(paths.PathUpRightTrack);
            PFRightUpTrack = Resources.Load<CTrackTile>(paths.PathRightUpTrack);
    
            PFEndTrack = Resources.Load<CTrackTile>(paths.PathEndTrack);


            TrackKind = new Dictionary<CTrackCreater.TRACKKIND, CTrackTile>();

            TrackKind.Add(CTrackCreater.TRACKKIND.START, PFStartTrack);

            TrackKind.Add(CTrackCreater.TRACKKIND.VERTICAL, PFVerticalTrack);
            TrackKind.Add(CTrackCreater.TRACKKIND.HORIZONTAL, PFHorizontalTrack);

            TrackKind.Add(CTrackCreater.TRACKKIND.UPLEFT, PFUpLeftTrack);
            TrackKind.Add(CTrackCreater.TRACKKIND.LEFTUP, PFLeftUpTrack);
            TrackKind.Add(CTrackCreater.TRACKKIND.UPRIGHT, PFUpRightTrack);
            TrackKind.Add(CTrackCreater.TRACKKIND.RIGHTUP, PFRightUpTrack);
            TrackKind.Add(CTrackCreater.TRACKKIND.END, PFEndTrack);
        }

        public void InitTrackStorage(Transform tParent, CTrackCreater tTrackCreater)
        {
            mTrackParent = tParent;
            mTrackCreater = tTrackCreater;

            TrackStorage = new Dictionary<CTrackCreater.TRACKKIND, List<CTrackTile>>();
            var tTrackKinds = System.Enum.GetValues(typeof(CTrackCreater.TRACKKIND)).GetEnumerator();

            while (tTrackKinds.MoveNext())
            {
                CTrackCreater.TRACKKIND tKind = (CTrackCreater.TRACKKIND)tTrackKinds.Current;
                int tCreateCount = 0;

                if (tKind == CTrackCreater.TRACKKIND.START || tKind == CTrackCreater.TRACKKIND.END)
                {
                    tCreateCount = 1;
                }
                else if (tKind != CTrackCreater.TRACKKIND.TURN)
                {
                    tCreateCount = 5;
                }

                for (int i = 0; i < tCreateCount; i++)
                {
                    if (TrackStorage.ContainsKey(tKind) == false)
                    {
                        TrackStorage.Add(tKind, new List<CTrackTile>());
                    }
                    if (GetPrefab(tKind) == null)
                    {
                        Debug.Log("Prefab is Null");
                    }
                    CTrackTile tile = GameObject.Instantiate(GetPrefab(tKind), Vector3.zero, Quaternion.identity);
                    tile.gameObject.SetActive(false);
                    tile.transform.SetParent(tParent);
                    tile.SetTrackCreater(mTrackCreater);
                    TrackStorage[tKind].Add(tile);
                }
            }
        }



        public CTrackTile GetPrefab(CTrackCreater.TRACKKIND tTrackKind)
        {
            if (TrackKind.ContainsKey(tTrackKind))
            {
                return TrackKind[tTrackKind];
            }

            return null;
        }

        /// <summary>
        /// 트랙창고리스트에서 원하는 종류의 트랙타일을 받아오는 메소드 IF 창고안에 비활성화 된 트랙타일이 없을시 생성하여 가져옵니다.
        /// </summary>
        /// <param name="tTrackKind">원하는 트랙의 종류</param>
        /// <returns>원하는 트랙타일을 반환</returns>
        public CTrackTile GetTrackTile(CTrackCreater.TRACKKIND tTrackKind)
        {
            CTrackTile tTile = null;

            if (TrackStorage.ContainsKey(tTrackKind))
            {
                for (int i = 0; i < TrackStorage[tTrackKind].Count; i++)
                {
                    if (TrackStorage[tTrackKind][i].gameObject.activeSelf == false)
                    {
                        tTile = TrackStorage[tTrackKind][i];
                        return tTile;
                    }
                }

                tTile = GameObject.Instantiate(GetPrefab(tTrackKind), Vector3.zero, Quaternion.identity);
                tTile.gameObject.SetActive(false);
                tTile.transform.SetParent(mTrackParent);
                TrackStorage[tTrackKind].Add(tTile);
                return tTile;
            }

            return null;
        }



    }
}
