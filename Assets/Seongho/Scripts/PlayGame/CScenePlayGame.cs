using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inspector;
using PrefabLoader;

public class CScenePlayGame : MonoBehaviour
{
    public enum StageType { None = 0, Air, Desert, Snow, Sea }

    private PlayGamePrefabs mPlayGamePrefabs = new PlayGamePrefabs();

    //Game State


    //Ref
    [ReadOnly]
    public CPlayer InstPlayer = null;
    [ReadOnly]
    public CTargetCamera InstTargetCamera = null;
    public CTrackFactory InstTrackCreator = null;
    [ReadOnly]
    [SerializeField]
    private CUIPlayGame mUIPlayGame = null;

    //Editor Test
    private Vector3 mStartPosition = Vector3.zero;
    private Quaternion mStartRotation = Quaternion.identity;



    private void Awake()
    {
        CreateBasicPrefabs();
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
        InstPlayer.SetCallOnGameOver(() => mUIPlayGame.InstUIGameOver.SetActive(true));

        //재시작 콜백
        mUIPlayGame.InstBtnRestart.onClick.AddListener(OnRestartRun);

    }


    [Button]
    public void CreateBasicPrefabs()
    {
        mPlayGamePrefabs.Load();
        if(InstPlayer == null)
        {
            InstPlayer = Instantiate(mPlayGamePrefabs.PFPlayer, Vector3.zero, Quaternion.identity);
        }
        if(InstTargetCamera == null)
        {
            InstTargetCamera = Instantiate(mPlayGamePrefabs.PFTargetCamera, Vector3.zero, Quaternion.identity);
            InstTargetCamera.SetTarget(InstPlayer.gameObject);
            InstTargetCamera.UpdatePosition();
        }
    }

    [Button]
    public void OnStartRun()
    {
        mStartPosition = InstPlayer.transform.position;
        mStartRotation = InstPlayer.transform.rotation;
        InstPlayer.SetMoveStart(true);
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
}
