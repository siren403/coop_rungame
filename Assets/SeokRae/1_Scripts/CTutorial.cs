using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CTutorial : MonoBehaviour {

    public GameObject mTuto1;
    public GameObject mTuto2;
    public GameObject mTuto3;

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
        mTuto2.SetActive(false);
        mTuto3.SetActive(true);
    }
    public void OnClickTutoBtn3()
    {
        SceneManager.LoadScene("SceneMainLobby");
        Destroy(mDestroythisScene);
    }
}

