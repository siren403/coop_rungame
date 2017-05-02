using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Map
{
    public class CTile : MonoBehaviour
    {

        private CTrackCreator mCreator = null;
        public CTrackCreator Creator
        {
            get
            {
                return mCreator;
            }
        }
        private int mIndex = 0;
        private CPlacementObject[] mPlacementObjects = null;
        private TrackType mTrackType;
        private CTrack mCachedParentTrack = null;
        private CTrack mParentTrack
        {
            get
            {
                if(mCachedParentTrack == null)
                {
                    mCachedParentTrack = GetComponentInParent<CTrack>();
                }
                return mCachedParentTrack;
            }
        }

        public virtual void Init(CTrackCreator tCreator,int tIndex,TrackType tTrackType)
        {
            mCreator = tCreator;
            mIndex = tIndex;
            Transform tObjectsParents = transform.FindChild("InstObjects");
            mPlacementObjects = tObjectsParents.GetComponentsInChildren<CPlacementObject>();
            mTrackType = tTrackType;
        }
        public void Show()
        {
            gameObject.SetActive(true);
            //ShowRenderer(GetComponentInChildren<Renderer>());

            //foreach (var obj in mPlacementObjects)
            //{
            //    ShowRenderer(obj.GetComponentInChildren<Renderer>());
            //}
        }
        public void Hide()
        {
            this.gameObject.SetActive(false);
            //HideRenderer(GetComponentInChildren<Renderer>());
        }
        private void ShowRenderer(Renderer target)
        {
            Color tColor = target.material.color;
            tColor.a = 0;
            target.material.color = tColor;

            tColor.a = 1;

            DOTween.To(() => target.material.color
            , (color) => target.material.color = color
            , tColor
            , 0.3f);
        }
        private void HideRenderer(Renderer target)
        {
            Color tColor = target.material.color;
            tColor.a = 1;
            target.material.color = tColor;

            tColor.a = 0;

            DOTween.To(() => target.material.color
            , (color) => target.material.color = color
            , tColor
            , 0.3f)
            .OnComplete(() => gameObject.SetActive(false));

        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag(CTag.TAG_PLAYER))
            {
                OnCollisionPlayer(other);
            }
        }
        protected virtual void OnCollisionPlayer(Collision other)
        {
            mCreator.UpdateTrackTile(mIndex);
        }

        public TrackType GetTrackType()
        {
            return mTrackType;
        }
        public void TileDestroy()
        {
            mParentTrack.CheckTileDestroy();
            Destroy(this.gameObject);
        }
    }
}
