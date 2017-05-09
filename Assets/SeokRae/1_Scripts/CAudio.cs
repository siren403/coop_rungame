using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAudio : MonoBehaviour {

    public AudioClip JumpBtnSfx;
    public AudioClip SlidBtnSfx;
    public AudioClip UIBtnSfx;
    AudioSource MySource;
   // public static CAudio instance;
    private void Awake()
    {
    }

    void Start () {
        MySource = this.gameObject.GetComponent<AudioSource>();
	}
	public void OnClickJumpSound()
    {
        MySource.PlayOneShot(JumpBtnSfx);
    }
    public void OnClickSlidSound()
    {
        MySource.PlayOneShot(SlidBtnSfx);
    }
    public void OnClickUISound()
    {
        MySource.PlayOneShot(UIBtnSfx);
    }
}
