using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CScore : MonoBehaviour
{
    public static  CScore mInst;
    public Text mScoreText;
    private int mScore = 0;
    public Text mComboText;
    private int mCombo = 0;

    private void Awake()
    {
        if (!mInst)
        {
            mInst = this;
        }
    }

    public void AddScore(int num)
    {
        mScore += num;
        mScoreText.text = "SCORE : " + mScore;
    }

    public void AddCombo(int num)
    {
        mCombo += num;
        mComboText.text = "COMBO : " + mCombo;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
