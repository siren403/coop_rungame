using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using Inspector;


public class CUILobby : MonoBehaviour {

    public CSceneMainLobby SceneMainLobby = null;
    public GameObject[] HeartArray = null;

    public const int TOTAL_HEARTCOUNT = 5;
    public const int WAITINGTIME = 30;


    public int CurrentHeartCount = 0;
    public int Total_Timer = 0;
    public int Min = 0;
    public int Sec = 0;


    private int mHeart = 0;
    private int mSpareTime = 0;
    private IntReactiveProperty mTime = null;

    private UserData mUserData = null;

    private void Awake()
    {
        HeartArray = new GameObject[TOTAL_HEARTCOUNT];
        HeartArray = SceneMainLobby.HeartArray;
        mUserData = new UserData();
        mTime = new IntReactiveProperty();
        mTime.Subscribe((time) =>
        {
            Total_Timer = time;
            Min = Total_Timer / 60;
            Sec = Total_Timer % 60;
            
            SceneMainLobby.m_Timecontrol.text = string.Format("{0} :{1:D2}", Min,Sec);
        });

       
    }

    private void Update()
    {
        UpdateHeartArray();
        CheckHeartTime();
    }


    public void OnClickBtnGoPlayGameScene()
    {
        mHeart -= 1;
        if(mHeart <0)
        {
            mHeart = 0;
        }
        mUserData.Heart = mHeart;
        if(mHeart != 0)
        {
            SceneManager.LoadScene("ScenePlayGame");
        }
        else
        {
            Debug.Log("Heart is null");
        }
    }


    [Button]
    public void SetUserData()
    {
        mUserData.Heart = 3;
    }

    [Button]
    public void CheckHeartTime()
    {
        if(mHeart == TOTAL_HEARTCOUNT)
        {
            SceneMainLobby.m_Timecontrol.gameObject.SetActive(false);
            return;
        }
        else
        {
            SceneMainLobby.m_Timecontrol.gameObject.SetActive(true);
        }

        var now = DateTime.Now.ToLocalTime();

        var span = (now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
        int nowTime = (int)span.TotalSeconds;

        int tExitTime = 0;

        if (mUserData.ExitTime == 0)
        {
            mUserData.ExitTime = nowTime;
        }
        else
        {
            tExitTime = mUserData.ExitTime;

        }


        mHeart = mUserData.Heart;

        int gapTime = nowTime - tExitTime;


        int roundCount = gapTime / WAITINGTIME;
        int remainder = gapTime % WAITINGTIME;


        if (roundCount > 0)
        {
            mUserData.ExitTime = nowTime;

            if (roundCount >= TOTAL_HEARTCOUNT - mHeart)
            {
                roundCount = TOTAL_HEARTCOUNT - mHeart;
                if (roundCount < 0) roundCount = 0;
            }


            mHeart += roundCount;

        }



        mUserData.SpareTime = WAITINGTIME - remainder;
        mUserData.Heart = mHeart;

        mTime.Value = mUserData.SpareTime;

    }

    public void UpdateHeartArray()
    {
        for(int ti = 0; ti< TOTAL_HEARTCOUNT; ti++)
        {
            if(ti< TOTAL_HEARTCOUNT - mHeart)
            {
                HeartArray[ti].SetActive(false);
            }
            else
            {
                HeartArray[ti].SetActive(true);
            }
        }
    }
}


