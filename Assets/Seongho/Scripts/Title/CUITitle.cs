using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CUITitle : MonoBehaviour
{

    public void OnClickBtnMoveLobby()
    {
        SceneManager.LoadScene("SceneMainLobby");
        SceneManager.LoadScene("MainTitleScene",LoadSceneMode.Additive);
    }
}
