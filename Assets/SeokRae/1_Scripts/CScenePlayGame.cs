using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CScenePlayGame : MonoBehaviour {

    private float JumpAndSlidDirection = 0.0f;


    public CUIPlayGame mUIGame;
    public GameObject mCube;

    public float mPlayerSpeed = 0.0f;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        JumpAndSlid();
        //JoyStickMove();

        Debug.Log(mUIGame.GetJoystickDirection());

        Vector3 pos = mCube.transform.position;
        pos.x += mUIGame.GetJoystickDirection() * 10 * Time.deltaTime;
        mCube.transform.position = pos;

        mUIGame.SetOnSliding(OnSliding);
        mUIGame.SetOnJumping(OnJump);
        mUIGame.SetOnItemBtn_1(OnUseItem_1);
        mUIGame.SetOnItemBtn_2(OnUseItem_2);

    }


    void JumpAndSlid()
    {
        Vector3 JumpAndSlidPos = mCube.transform.position;
        JumpAndSlidPos.y = GetJumpAndSlidDirection();
        mCube.transform.position = JumpAndSlidPos;

    }
    /*
    void JoyStickMove()
    {
        Vector3 JoyStickPos = mCube.transform.position;
        JoyStickPos.x = mUIGame.GetJoyStickDirection();
        mCube.transform.position = JoyStickPos;
    }
    */



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
    void OnUseItem_1()
    {
        Debug.Log("ItemUse1");
    }
    void OnUseItem_2()
    {
        Debug.Log("ItemUse2");
    }



    
    public float GetJumpAndSlidDirection()
    {
        return JumpAndSlidDirection;
    }
}
