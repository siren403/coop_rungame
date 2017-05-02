using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class CEndThemePad : MonoBehaviour
    {
        [Range(-1, 1)]
        public int LeftOrRight = 0;

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(CTag.TAG_PLAYER))
            {
                var tPlayer = other.GetComponent<CPlayer>();
                if (tPlayer.PositionAbsMove(this.transform.position))
                {
                    GetComponentInParent<CTile>().Creator.OnSelectNextTheme(LeftOrRight);
                }
            }
        }
    }
}
