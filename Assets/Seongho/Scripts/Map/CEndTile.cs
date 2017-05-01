using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class CEndTile : CTile
    {
      

        public override void Init(CTrackCreator tCreator, int tIndex, TrackType tTrackType)
        {
            base.Init(tCreator, tIndex, tTrackType);
        }

        protected override void OnCollisionPlayer(Collision other)
        {
            base.OnCollisionPlayer(other);

        }

    }
}
