using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
public class CUITitle : MonoBehaviour
{
    public AudioClip BtnSfx;
    
    AudioSource MySource;

    public GameObject mFind;

    public GameObject mInstLobbyBtn;

    public static CUITitle instance;

    private void Awake()
    {
        if(CUITitle.instance==null)
        {
            CUITitle.instance = this;
        }
    }
    private void Start()
    {
        MySource = this.gameObject.GetComponent<AudioSource>();
        mFind = GameObject.Find("Main Camera");

        DontDestroyOnLoad(mFind);
        DOTween.To(() => mInstLobbyBtn.GetComponent<Text>().color, (color) =>
                mInstLobbyBtn.GetComponent<Text>().color = color, new Color(0, 0, 0, 0),
                0.5f).OnComplete(() => { mInstLobbyBtn.GetComponent<Text>().color = new Color(0, 0, 0, 1); }).
                SetLoops(-1, LoopType.Restart).SetId("ABCD");
    }

    

    public void OnClickBtnMoveLobby()
    {
        MySource.PlayOneShot(BtnSfx);
        Invoke("invokeLoadScene", 0.5f);
        
       // SceneManager.LoadScene("MainTitleScene",LoadSceneMode.Additive);
    }

    void invokeLoadScene()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
