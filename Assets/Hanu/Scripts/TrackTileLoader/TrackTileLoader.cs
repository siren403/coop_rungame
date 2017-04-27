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
            public string PathEndTrack;
            public string PathTrackA;
            public string PathTrackB;
            public string PathTrackC;
            public string PathTrackD;
            public string PathTrackE;
            public string PathTrackF;
            public string PathTrackG;

        }

        private CTrackTile PFStartTrack = null;
        private CTrackTile PFVerticalTrack = null;
        private CTrackTile PFEndTrack = null;

        private CTrackTile PFTrackA = null;
        private CTrackTile PFTrackB = null;
        private CTrackTile PFTrackC = null;
        private CTrackTile PFTrackD = null;
        private CTrackTile PFTrackE = null;
        private CTrackTile PFTrackF = null;
        private CTrackTile PFTrackG = null;





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
            PFEndTrack = Resources.Load<CTrackTile>(paths.PathEndTrack);

            PFTrackA = Resources.Load<CTrackTile>(paths.PathTrackA);
            PFTrackB = Resources.Load<CTrackTile>(paths.PathTrackB);
            PFTrackC = Resources.Load<CTrackTile>(paths.PathTrackC);
            PFTrackD = Resources.Load<CTrackTile>(paths.PathTrackD);
            PFTrackE = Resources.Load<CTrackTile>(paths.PathTrackE);
            PFTrackF = Resources.Load<CTrackTile>(paths.PathTrackF);
            PFTrackG = Resources.Load<CTrackTile>(paths.PathTrackG);



            TrackKind = new Dictionary<CTrackCreater.TRACKKIND, CTrackTile>();

            TrackKind.Add(CTrackCreater.TRACKKIND.START, PFStartTrack);
            TrackKind.Add(CTrackCreater.TRACKKIND.VERTICAL, PFVerticalTrack);
            TrackKind.Add(CTrackCreater.TRACKKIND.A, PFTrackA);
            TrackKind.Add(CTrackCreater.TRACKKIND.B, PFTrackB);
            TrackKind.Add(CTrackCreater.TRACKKIND.C, PFTrackC);
            TrackKind.Add(CTrackCreater.TRACKKIND.D, PFTrackD);
            TrackKind.Add(CTrackCreater.TRACKKIND.E, PFTrackE);
            TrackKind.Add(CTrackCreater.TRACKKIND.F, PFTrackF);
            TrackKind.Add(CTrackCreater.TRACKKIND.G, PFTrackG);
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
                else
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
                    CTrackTile tTile = GameObject.Instantiate(GetPrefab(tKind), Vector3.zero, Quaternion.identity);
                    tTile.gameObject.SetActive(false);
                    tTile.transform.SetParent(tParent);
                    tTile.SetTrackCreater(mTrackCreater);

             
                    
                    TrackStorage[tKind].Add(tTile);
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
                tTile.SetTrackCreater(mTrackCreater);

                TrackStorage[tTrackKind].Add(tTile);
                return tTile;
            }

            return null;
        }



    }
}
