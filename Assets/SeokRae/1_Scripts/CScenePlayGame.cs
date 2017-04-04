using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CScenePlayGame : MonoBehaviour {

    public CUIPlayGame mUIGame;
    public GameObject mCube;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        JumpAndSlid();
        JoyStickMove();
	}
    void JumpAndSlid()
    {
        Vector3 JumpAndSlidPos = mCube.transform.position;
        JumpAndSlidPos.y = mUIGame.GetJumpAndSlidDirection();
        mCube.transform.position = JumpAndSlidPos;
    }
    void JoyStickMove()
    {
        Vector3 JoyStickPos = mCube.transform.position;
        JoyStickPos.x = mUIGame.GetJoyStickDirection();
        mCube.transform.position = JoyStickPos;
    }
}
