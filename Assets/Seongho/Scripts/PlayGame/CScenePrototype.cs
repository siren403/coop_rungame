using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CScenePrototype : MonoBehaviour
{
    public CTrackFactory mTrackMaker = null;
    public UsePhysics.CPlayer mPlayer = null;
    public CUIPlayerController mController = null;
    public CKeyboardPlayerController mKeyboardController = null;
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
        mKeyboardController.SetCallOnScreenSlide(mPlayer.SetRotateInput);
        mPlayer.SetFuncHorizontal(mController.GetHorizontal);
#elif UNITY_STANDALONE_WIN
        mKeyboardController.SetCallOnJump(mPlayer.DoJump);
        mKeyboardController.SetCallOnItem_1(() => mPlayer.SetRotateInput(-1));
        mKeyboardController.SetCallOnItem_2(() => mPlayer.SetRotateInput(1));
        mKeyboardController.SetCallOnScreenSlide(mPlayer.SetRotateInput);
        mPlayer.SetFuncHorizontal(mKeyboardController.GetHorizontal);
#elif UNITY_ANDROID
        mController.SetCallOnJump(mPlayer.DoJump);
        mController.SetCallOnItem_1(() => mPlayer.SetRotateInput(-1));
        mController.SetCallOnItem_2(() => mPlayer.SetRotateInput(1));
        mController.SetCallOnScreenSlide(mPlayer.SetRotateInput);
        mPlayer.SetFuncHorizontal(mController.GetHorizontal);
#endif
        mPlayer.SetCallOnRotate(TargetCamera.RotateCamera);
    }

  

    private IEnumerator SeqStartStage()
    {
        mStartPosition = mPlayer.transform.position;
        mStartRotation = mPlayer.transform.rotation;

        yield return new WaitForSeconds(1.0f);

        mPlayer.SetMoveStart(true);
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
            mPlayer.transform.position = mStartPosition;
            mPlayer.transform.rotation = mStartRotation;
            TargetCamera.ResetAngle();
        }
    }

}
