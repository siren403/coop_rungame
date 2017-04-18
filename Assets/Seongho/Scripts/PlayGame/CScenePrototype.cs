using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CScenePrototype : MonoBehaviour
{
    //Map
    public CTrackFactory mTrackMaker = null;

    //Player
    public UsePhysics.CPlayer mPlayer = null;

    //Controller
    public CUIPlayerController mController = null;
    public CKeyboardPlayerController mKeyboardController = null;

    //UI
    public GameObject mUIGameOver = null;

    public CTargetCamera TargetCamera = null;

    private bool mIsPlaying = false;

    private Vector3 mStartPosition = Vector3.zero;
    private Quaternion mStartRotation = Quaternion.identity;

    private void Awake()
    {
        CHanMapDataMgr.GetInst().CreateHan();
        mTrackMaker.CreateTrack();

#if UNITY_EDITOR
        mController.SetCallOnJump(mPlayer.DoJump);
        mController.SetCallOnSlide(mPlayer.DoSlide);
        mKeyboardController.SetCallOnScreenSlide(mPlayer.SetRotateInput);
        mPlayer.SetFuncHorizontal(mController.GetHorizontal);
#elif UNITY_ANDROID
        mController.SetCallOnJump(mPlayer.DoJump);
        mController.SetCallOnSlide(mPlayer.DoSlide);
        mController.SetCallOnScreenSlide(mPlayer.SetRotateInput);
        mPlayer.SetFuncHorizontal(mController.GetHorizontal);
#endif
        mPlayer.SetCallOnRotate(TargetCamera.RotateCamera);
        mPlayer.SetCallOnGameOver(OnGameOver);
    }


    private IEnumerator SeqStartStage()
    {
        mStartPosition = mPlayer.transform.position;
        mStartRotation = mPlayer.transform.rotation;

        yield return new WaitForSeconds(1.0f);

        mPlayer.SetMoveStart(true);
    }
    private void OnGameOver()
    {
        mUIGameOver.SetActive(true);
    }

    public void OnRestart()
    {
        mPlayer.transform.position = mStartPosition;
        mPlayer.transform.rotation = mStartRotation;

        mPlayer.OnReset();
        TargetCamera.ResetAngle();
        mUIGameOver.SetActive(false);

        mIsPlaying = false;
    }

    private void OnGUI()
    {
        GUIRect guiRect = new GUIRect();
        guiRect.center = new Vector2(Screen.width * 0.45f, Screen.height * 0.9f);
        guiRect.size = new Vector2(Screen.width * 0.1f, Screen.height * 0.08f);
        if (mIsPlaying == false &&
            (GUI.Button(guiRect.rect, "START") || Input.GetKeyDown(KeyCode.Q)))
        {
            mIsPlaying = true;
            StartCoroutine(SeqStartStage());
        }

        guiRect.center = new Vector2(Screen.width * 0.55f, Screen.height * 0.9f);
        if (GUI.Button(guiRect.rect, "RESET"))
        {
            OnRestart();
        }
    }

}
