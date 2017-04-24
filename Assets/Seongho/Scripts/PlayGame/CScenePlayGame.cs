using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inspector;
using ResourceLoader;

public class CScenePlayGame : MonoBehaviour
{
    public enum StageType { None = 0, Air, Desert, Snow, Sea }

    private PlayGamePrefabs mPlayGamePrefabs = new PlayGamePrefabs();

    //GameState
    private IntReactiveProperty mScore = null;
    private IntReactiveProperty mCoin = null;

    public float HpTickTime = 0.5f;
    public int HpTickPerHp = 10;
    public float HpTickPerHpRatio = 1.0f;
    public float ScoreTickTime = 0.01f;
    public float CoinPerScore = 20.0f;
    public float CoinPerBoost = 1.0f;

    //Ref
    public CPlayer InstPlayer = null;
    public CTargetCamera InstTargetCamera = null;
    public CTrackFactory InstTrackCreator = null;
    [ReadOnly]
    [SerializeField]
    private CUIPlayGame mUIPlayGame = null;

    private Coroutine mCoroutineTickHp = null;
    private Coroutine mCoroutineTickScore = null;

    //Editor Test
    private Vector3 mStartPosition = Vector3.zero;
    private Quaternion mStartRotation = Quaternion.identity;

    private void Awake()
    {
        InstPlayer.SetScene(this);

        //CreateBasicPrefabs();
        CHanMapDataMgr.GetInst().CreateHan();
        InstTrackCreator.CreateTrack();

        mUIPlayGame = FindObjectOfType<CUIPlayGame>();

        CPlayerController tController = null;
#if UNITY_EDITOR
        tController = GetComponentInChildren<CKeyboardPlayerController>();
#elif UNITY_ANDROID
        tController = GetComponentInChildren<CUIPlayerController>();
#endif


        //플레이어 컨트롤러 세팅
        tController.SetCallOnJump(InstPlayer.DoJump);
        tController.SetCallOnSlide(InstPlayer.DoSlide);
        tController.SetCallOnScreenSlide(InstPlayer.SetRotateInput);
        InstPlayer.SetFuncHorizontal(tController.GetHorizontal);

        //플레이어의 상태 변화 콜백
        InstPlayer.SetCallOnRotate(InstTargetCamera.RotateCamera);
        InstPlayer.SetCallOnGameOver(OnGameOver);

        //재시작 콜백
        mUIPlayGame.InstBtnRestart.onClick.AddListener(OnRestartRun);
        //일시정지 UI On,Off
        mUIPlayGame.InstBtnPause.onClick.AddListener(() => OnPause(true));
        mUIPlayGame.InstBtnPauseClose.onClick.AddListener(() => OnPause(false));
        //포기 확인
        mUIPlayGame.InstBtnSubmitRetire.onClick.AddListener(OnRetire);

        InstPlayer.CurrentHp.Subscribe((hp) => mUIPlayGame.InstSliderHPBar.value = (float)hp / InstPlayer.Hp);
        InstPlayer.CurrentBoost.Subscribe((boost) => mUIPlayGame.InstSliderBoostBar.value = boost / InstPlayer.Boost);

        mScore = new IntReactiveProperty();
        mScore.Subscribe((score) => mUIPlayGame.SetTxtScore(score + (mCoin.Value * (int)CoinPerScore)));
        mCoin = new IntReactiveProperty();
        mCoin.Subscribe((coin) => mUIPlayGame.SetTxtCoin(coin));
    }

    private void OnPause(bool isPause)
    {
        if(isPause)
        {
            Time.timeScale = 0;
            mUIPlayGame.ShowUIPause(mScore.Value);
        }
        else
        {
            Time.timeScale = 1;
            mUIPlayGame.HideUIPause();
        }
    }

    private void OnGameOver()
    {
        mUIPlayGame.InstUIGameOver.SetActive(true);
        if (mCoroutineTickHp != null)
            StopCoroutine(mCoroutineTickHp);
    }
    private void OnRetire()
    {
        
    }

    
    //[Button]
    //public void CreateBasicPrefabs()
    //{
    //    mPlayGamePrefabs.Load();
    //    if(InstPlayer == null)
    //    {
    //        InstPlayer = Instantiate(mPlayGamePrefabs.PFPlayer, Vector3.zero, Quaternion.identity);
    //    }
    //    if(InstTargetCamera == null)
    //    {
    //        InstTargetCamera = Instantiate(mPlayGamePrefabs.PFTargetCamera, Vector3.zero, Quaternion.identity);
    //        InstTargetCamera.SetTarget(InstPlayer.gameObject);
    //        InstTargetCamera.UpdatePosition();
    //    }
    //}

    [Button]
    public void OnStartRun()
    {
        mStartPosition = InstPlayer.transform.position;
        mStartRotation = InstPlayer.transform.rotation;
        InstPlayer.SetMoveStart(true);
        mCoroutineTickHp = StartCoroutine(TickHp());
        mCoroutineTickScore = StartCoroutine(TickScore());

    }
    private IEnumerator TickHp()
    {
        while(true)
        {
            yield return new WaitForSeconds(HpTickTime);
            InstPlayer.DecrementHp((int)(HpTickPerHp * HpTickPerHpRatio));
        }
    }
    private IEnumerator TickScore()
    {
        while(true)
        {
            yield return new WaitForSeconds(ScoreTickTime);
            mScore.Value += 1;
        }
    }

    [Button]
    public void OnRestartRun()
    {
        InstPlayer.transform.position = mStartPosition;
        InstPlayer.transform.rotation = mStartRotation;

        InstPlayer.OnReset();
        InstTargetCamera.ResetAngle();
        mUIPlayGame.InstUIGameOver.SetActive(false);
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
    public void OnIncrementCoin()
    {
        mCoin.Value += 1;
        InstPlayer.IncrementBoost(CoinPerBoost);
    }

}
