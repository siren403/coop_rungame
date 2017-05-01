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
                GetComponentInParent<CTile>().Creator.OnSelectNextTheme(LeftOrRight);
            }
        }
    }
}
