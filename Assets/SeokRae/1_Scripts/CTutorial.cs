using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CTutorial : MonoBehaviour {

    public GameObject mTuto1;
    public GameObject mTuto2;

    GameObject mDestroythisScene;


    private void Start()
    {
        mDestroythisScene = GameObject.FindGameObjectWithTag("Destroy");
    }
    public void OnClickTuto1Btn()
    {
        mTuto1.SetActive(false);
        mTuto2.SetActive(true);
    }
    public void OnClickTutoBtn2()
    {
        SceneManager.LoadScene("SceneMainLobby");
        Destroy(mDestroythisScene);
        
    }
}

