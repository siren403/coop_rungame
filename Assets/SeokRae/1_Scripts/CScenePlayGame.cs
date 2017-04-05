using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CScenePlayGame : MonoBehaviour {

    private float JumpAndSlidDirection = 0.0f;


    public CUIPlayGame mUIGame;
    public GameObject mCube;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        JumpAndSlid();
        JoyStickMove();

        mUIGame.SetOnSliding(OnSliding);
        mUIGame.SetOnJumping(OnJump);
    }
    void JumpAndSlid()
    {
        Vector3 JumpAndSlidPos = mCube.transform.position;
        JumpAndSlidPos.y = GetJumpAndSlidDirection();
        mCube.transform.position = JumpAndSlidPos;

    }
    void JoyStickMove()
    {
        Vector3 JoyStickPos = mCube.transform.position;
        JoyStickPos.x = mUIGame.GetJoyStickDirection();
        mCube.transform.position = JoyStickPos;
    }






    void OnJump()
    {
        Debug.Log("Jump!!");
        JumpAndSlidDirection = 1;
        CScore.mInst.AddScore(10);
    }
    void OnSliding()
    {
        Debug.Log("Slid!!");
        JumpAndSlidDirection = -1;
        CScore.mInst.AddCombo(1);//Combo
    }
    public float GetJumpAndSlidDirection()
    {
        return JumpAndSlidDirection;
    }
}
