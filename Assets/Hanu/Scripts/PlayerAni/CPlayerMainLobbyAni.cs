using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerMainLobbyAni : MonoBehaviour {

    private int mRandomAni = 0;

    public Animator mAnimator = null;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetRandomValue()
    {
        mRandomAni = Random.Range(0, 5);
    }

    public void OnMouseDown()
    {
        SetRandomValue();
        DoAni();
        mRandomAni = 0;
    }

    public void DoAni()
    {
        switch(mRandomAni)
        {
            case 0:
                mAnimator.SetTrigger("trigAni0");
                break;
            case 1:
                mAnimator.SetTrigger("trigAni1");
                break;
            case 2:
                mAnimator.SetTrigger("trigAni2");
                break;
            case 3:
                mAnimator.SetTrigger("trigAni3");
                break;
            case 4:
                mAnimator.SetTrigger("trigAni4");
                break;
            case 5:
                mAnimator.SetTrigger("trigAni5");
                break;
        }
    }

    
}
