using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COutTrack : MonoBehaviour {



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CTag.TAG_PLAYER))
        {

            CPlayer tPlayer = other.GetComponent<CPlayer>();
            tPlayer.SetSpeedRatio(0.5f);
            FindObjectOfType<CScenePlayGame>().HpTickPerHpRatio = 20.0f;
            tPlayer.ScenePlayGame.UIPlayGame.AlphaValue();
            tPlayer.ScenePlayGame.UIPlayGame.ShowWarning();
            tPlayer.ScenePlayGame.AudioData.OutLineSound();


        }
    }


}
