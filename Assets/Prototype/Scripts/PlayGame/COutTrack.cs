using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COutTrack : MonoBehaviour {

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CTag.TAG_PLAYER))
        {
            other.GetComponent<CPlayer>().SetSpeedRatio(0.5f);
            FindObjectOfType<CScenePlayGame>().HpTickPerHpRatio = 35.0f;
            other.GetComponent<CPlayer>().ScenePlayGame.UIPlayGame.AlphaValue();
            other.GetComponent<CPlayer>().ScenePlayGame.AudioData.OutLineSound();
            
        }
    }

   
}
