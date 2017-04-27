using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Map
{
    public class CTile : MonoBehaviour
    {

        private CTrackCreator mCreator = null;
        private int mIndex = 0;

        public void Init(CTrackCreator tCreator,int tIndex)
        {
            mCreator = tCreator;
            mIndex = tIndex;
        }
        public void Show()
        {
            gameObject.SetActive(true);
            var renderer = GetComponentInChildren<Renderer>();
            Color tColor = renderer.material.color;
            tColor.a = 0;
            renderer.material.color = tColor;

            tColor.a = 1;

            DOTween.To(() => renderer.material.color
            , (color) => renderer.material.color = color
            , tColor
            , 0.3f);
        }
        public void Hide()
        {
            var renderer = GetComponentInChildren<Renderer>();

            Color tColor = renderer.material.color;
            tColor.a = 1;
            renderer.material.color = tColor;

            tColor.a = 0;

            DOTween.To(() => renderer.material.color
            , (color) => renderer.material.color = color
            , tColor
            , 0.3f)
            .OnComplete(() => gameObject.SetActive(false));
        }
        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag(CTag.TAG_PLAYER))
            {
                mCreator.UpdateTrackTile(mIndex);
            }
        }
    }
}
