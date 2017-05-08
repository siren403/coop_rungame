﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inspector;
using ResourceLoader;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CScenePlayGame : MonoBehaviour
{
    public enum StageType { None = 0, Air, Desert, Snow, Sea }

    private PlayGamePrefabs mPlayGamePrefabs = new PlayGamePrefabs();

    private UserData mUserData = null;
    private CPlayGameData mPlayGameData = null;
    public CPlayGameData PlayGameData
    {
        get
        {
            return mPlayGameData;
        }
    }

    //GameState
    [ReadOnly]
    [SerializeField]
    private bool mIsPlaying = false;
    public bool IsPlaying
    {
        get
        {
            return mIsPlaying;
        }
    }
    private FloatReactiveProperty mScore = null;
    private IntReactiveProperty mCoin = null;

    public float HpTickPerHpRatio = 1.0f;
    public int TotalScore = 0;
    //Ref
    public CPlayer InstPlayer = null;
    public CTargetCamera InstTargetCamera = null;
    public CItemTimer InstItemTimer = null;
    [ReadOnly]
    [SerializeField]
    private CUIPlayGame mUIPlayGame = null;
    public CUIPlayGame UIPlayGame
    {
        get {
            return mUIPlayGame;
        }
    }

    private Coroutine mCoroutineTickHp = null;
    private Coroutine mCoroutineTickScore = null;

    private Map.CTrackCreator mTrackCreator = null;
    private Coroutine mCurrentStageTick = null;

    //Editor Test
    private Vector3 mStartPosition = Vector3.zero;
    private Quaternion mStartRotation = Quaternion.identity;

    private bool mIsInputJumpAndSlide = false;

    private void Awake()
    {
        InstPlayer.SetScene(this);
        InstItemTimer.SetScene(this);

       
        mUserData = new UserData();
        mPlayGameData = Resources.Load<CPlayGameData>("GameData/CPlayGameData");

        mUIPlayGame = FindObjectOfType<CUIPlayGame>();

        CPlayerController tController = null;
#if UNITY_EDITOR
        tController = GetComponentInChildren<CKeyboardPlayerController>();
#elif UNITY_ANDROID
        tController = GetComponentInChildren<CUIPlayerController>();
#endif


        //플레이어 컨트롤러 세팅
        tController.SetCallOnJump(InstPlayer.DoJump);
        tController.SetCallOnJump(() => mIsInputJumpAndSlide = true);
        tController.SetCallOnSlide(InstPlayer.DoSlide);
        tController.SetCallOnSlide(() => mIsInputJumpAndSlide = true);

        tController.SetCallOnScreenSlide(InstPlayer.SetRotateInput);
        InstPlayer.SetFuncHorizontal(tController.GetHorizontal);

        //플레이어의 상태 변화 콜백
        InstPlayer.SetCallOnRotate(InstTargetCamera.RotateCamera);
        InstPlayer.SetCallOnGameOver(OnGameOver);

        //게임오버 - 로비 이동
        mUIPlayGame.InstBtnMoveLobby.onClick.AddListener(OnMoveLobby);
        
        //일시정지 UI On,Off
        mUIPlayGame.InstBtnPause.onClick.AddListener(() => OnPause(true));
        mUIPlayGame.InstBtnPauseClose.onClick.AddListener(() => OnPause(false));

        //포기 확인
        mUIPlayGame.InstBtnSubmitRetire.onClick.AddListener(OnRetire);

        InstPlayer.CurrentHp.Subscribe((hp) => mUIPlayGame.InstSliderHPBar.value = (float)hp / InstPlayer.Hp);
        InstPlayer.CurrentBoost.Subscribe((boost) => mUIPlayGame.InstSliderBoostBar.value = boost / InstPlayer.Boost);

        mScore = new FloatReactiveProperty();
        mScore.Subscribe((score) =>
        {
            TotalScore = (int)score + (mCoin.Value * (int)mPlayGameData.CoinPerScore);
            mUIPlayGame.SetTxtScore(TotalScore);
            mUIPlayGame.SetTxtPauseScore(TotalScore);
        });
        mCoin = new IntReactiveProperty();
        mCoin.Subscribe((coin) => mUIPlayGame.SetTxtCoin(coin));


        mTrackCreator = new Map.CTrackCreator(this.transform);
        mTrackCreator.OnShowEndTrack = (left,right) => 
        {
            mUIPlayGame.ShowTxtSelectTheme(left, right);
        };
        mTrackCreator.OnChangeStage = (stage,theme) => 
        {
            mUIPlayGame.SetTxtStageNumber(stage);
            mUIPlayGame.HideTxtSelectTheme();

            if(mCurrentStageTick != null)
            {
                StopCoroutine(mCurrentStageTick);
            }
            switch(theme)
            {
                case 0:
                    mCurrentStageTick = StartCoroutine(ThemeTick_1());
                    break;
                case 1:
                    mCurrentStageTick = StartCoroutine(ThemeTick_2());
                    break;
                case 2:
                    break;
            }
        };

        mTrackCreator.CreateTrackData();
        mTrackCreator.PositionTracks();
        mTrackCreator.UpdateTrackTile(0);
    }
    private IEnumerator Start()
    {
        mUIPlayGame.FadeInPanel();
        yield return new WaitForSeconds(1.0f);
        OnStartRun();
    }
    private void Update()
    {
        if(mIsPlaying)
        {
            mUIPlayGame.InstSliderTrackProgress.value = mTrackCreator.TrackProgress;
        }
    }

    private void OnPause(bool isPause)
    {
        if(isPause)
        {
            Time.timeScale = 0;
            mUIPlayGame.ShowUIPause();
        }
        else
        {
            Time.timeScale = 1;
            mUIPlayGame.HideUIPause();
        }
    }

    private void OnGameOver()
    {
        mUIPlayGame.ShowUIGameOver(0, TotalScore, mCoin.Value);
        InstItemTimer.Reset();
        mUserData.Coin += mCoin.Value;
        mIsPlaying = false;
    }

    private void OnRetire()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SceneMainLobby");
    }


    [Button]
    public void OnStartRun()
    {
        mIsPlaying = true;
        mStartPosition = InstPlayer.transform.position;
        mStartRotation = InstPlayer.transform.rotation;
        InstPlayer.SetMoveStart(true);
        mCoroutineTickHp = StartCoroutine(TickHp());
        mCoroutineTickScore = StartCoroutine(TickScore());

    }
    private IEnumerator TickHp()
    {
        while(mIsPlaying)
        {
            InstPlayer.DecrementHp((int)(mPlayGameData.HpTickPerHp * HpTickPerHpRatio));
            yield return new WaitForSeconds(mPlayGameData.HpTickTime);
        }
    }
    private IEnumerator TickScore()
    {
        while(mIsPlaying)
        {
            mScore.Value += 1 * InstPlayer.TotalSpeedRatio;
            yield return new WaitForSeconds(mPlayGameData.ScoreTickTime);
        }
    }


    #region Stage Tick
    private bool mIsTrackEffect = false;
    private IEnumerator ThemeTick_1()
    {
        while(true)
        {
            if (mIsPlaying)
            {
                if (mIsTrackEffect == false)
                {
                    if (mTrackCreator.CurrentPivot < 65 &&
                        mTrackCreator.CurrentPivot != 0 && mTrackCreator.CurrentPivot % mPlayGameData.Theme1EffectCount == 0)
                    {
                        mIsTrackEffect = true;
                        Debug.Log("Effect");

                        float tDelay = mPlayGameData.Theme1EffectDelay;

                        int tIsDir = Random.value > 0.5f ? -1 : 1;
                        mUIPlayGame.ShowTheme1UI(tIsDir, tDelay);
                        InstPlayer.transform.DOMoveX(tIsDir == 1 ? -3.0f : 3.0f, 0.25f)
                            .SetDelay(tDelay)
                            .SetRelative()
                            .OnStart(() =>
                            {
                                InstPlayer.IsControl = false;
                                Debug.Log("Effect Start");
                            })
                            .OnComplete(() =>
                            {
                                Debug.Log("Effect Active");
                                mIsTrackEffect = false;
                                InstPlayer.IsControl = true;
                            })
                            .SetId("TweenWind");
                    }
                }
            }
            else
            {
                if(DOTween.IsTweening("TweenWind"))
                {
                    DOTween.Kill("TweenWind");
                }
            }
            yield return null;
        }
    }
    private float mNotInputTime = 0.0f;
    private IEnumerator ThemeTick_2()
    {
        while (true)
        {

            yield return null;
            if (mIsPlaying)
            {
                if (mTrackCreator.CurrentPivot < 65 && mTrackCreator.CurrentPivot > 5)
                {
                    mNotInputTime += Time.deltaTime;

                    if (mNotInputTime >= mPlayGameData.Theme2NotInputDuration)
                    {
                        Debug.Log("Down Speed");
                        mUIPlayGame.ShowTheme2UI(true);
                        InstPlayer.SetSpeedRatio(mPlayGameData.Theme2EffectSpeedRatio);
                    }
                }
                else
                {
                    mNotInputTime = 0.0f;
                    InstPlayer.SetSpeedRatio(1.0f);
                    mUIPlayGame.ShowTheme2UI(false);
                }

                if (mIsInputJumpAndSlide)
                {
                    mIsInputJumpAndSlide = false;
                    mNotInputTime = 0.0f;
                    InstPlayer.SetSpeedRatio(1.0f);
                    mUIPlayGame.ShowTheme2UI(false);
                }
            }
        }
    }
    private IEnumerator StageTick_3()
    {
        while (true)
        {
            if(mIsPlaying)
            {

            }
            yield return null;
        }
    }

    #endregion

    public void PlayerOutTrack()
    {
        InstPlayer.SetSpeedRatio(mPlayGameData.OutTrackSpeedRatio);
        HpTickPerHpRatio = mPlayGameData.OutTrackHpDecrementRatio;
    }

    [Button]
    public void OnRestartRun()
    {
        mIsPlaying = true;
        InstPlayer.transform.position = mStartPosition;
        InstPlayer.transform.rotation = mStartRotation;

        InstPlayer.OnReset();
        InstTargetCamera.ResetAngle();
        mUIPlayGame.HideUIGameOver();
        mCoroutineTickHp = StartCoroutine(TickHp());
        mCoroutineTickScore = StartCoroutine(TickScore());

        mCoin.Value = 0;
        mScore.Value = 0;
        HpTickPerHpRatio = 1.0f;

    }

    private void OnMoveLobby()
    {
        SceneManager.LoadScene("SceneMainLobby");
    }

    [Button]
    public void OnDecrementHp()
    {
        InstPlayer.DecrementHp(10);
    }
    [Button]
    public void OnIncrementBoost()
    {
        InstPlayer.IncrementBoost(5.7f);
    }
    [Button]
    public void OnIncrementScore()
    {
        mScore.Value += Random.Range(50, 150);
    }
    [Button]
    public void OnIncrementCoin()
    {
        mCoin.Value += 1;
        InstPlayer.IncrementBoost(mPlayGameData.CoinPerBoost);
    }

    
}
