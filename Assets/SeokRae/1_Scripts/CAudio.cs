using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAudio : MonoBehaviour
{

    public AudioClip JumpBtnSfx;
    public AudioClip SlidBtnSfx;
    public AudioClip UIBtnSfx;
    public AudioClip BuyItemSfx;
    public AudioClip CollidetCharSfx;
    public AudioClip GetCoin;
    public AudioClip GetItemSfx;
    public AudioClip GetDashSfx;
    public AudioClip DeadSoundSfx;
    public AudioClip EndTrackSfx;
    public AudioClip WindDangerousSfx;
    public AudioClip OutlineSfx;
    public AudioClip DesertSfx;

    public AudioSource MySource_2;
    public AudioSource MySource;

    // public static CAudio instance;
    private void Awake()
    {
    }

    void Start()
    {
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
    public void OnClickBuyItemSound()
    {
        MySource.PlayOneShot(BuyItemSfx);
        
    }
    public void OnClickColliderChar()
    {
        MySource.PlayOneShot(CollidetCharSfx);
    }
    public void GetCoinSound()
    {
        MySource.PlayOneShot(GetCoin);
    }
    public void GetItemSound()
    {
        MySource.PlayOneShot(GetItemSfx);
    }
    public void GetDashSound()
    {
        MySource.PlayOneShot(GetDashSfx);
    }
    public void DeadSound()
    {
        MySource.PlayOneShot(DeadSoundSfx);
    }
    public void EndTrackSound()
    {
        MySource.PlayOneShot(EndTrackSfx);
    }
    public void WindDangerousSound()
    {
        MySource.PlayOneShot(WindDangerousSfx);
    }
    public void OutLineSound()
    {
        MySource.PlayOneShot(OutlineSfx);
    }
    public void DesertPlaySound()
    {
        MySource_2.clip=(DesertSfx);
        MySource_2.Play();
    }
    public void DesertStopSound()
    {
        MySource_2.Stop();
    }
}
