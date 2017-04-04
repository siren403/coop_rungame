using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CUIPlayGame : MonoBehaviour {
    
    public Slider mHealthBarSlider;//체력바
    public Slider mJoyStick;//조이스틱
    public Vector3 mBackGround;

    private float JumpAndSlidDirection = 0.0f;
    private float JoyStcikDirection = 0.0f;

	// Use this for initialization
	void Start () {
        //InvokeRepeating("HealthDown", 1.0f, 1.0f);
        StartCoroutine(HealthBarDown());//체력바깎이는 코루틴시작
        StartCoroutine(JoyStcikLRMove());
    }
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonUp(0))
        {
            mJoyStick.value = 2.0f;
        }
            //JoyStickCtl();

        }
        IEnumerator HealthBarDown()//체력바깎이는 코루틴
    {
        while (true)
        {
            mHealthBarSlider.value -= 20;//체력바 밸류값-20
            yield return new WaitForSeconds(1.0f);
        }
    }
    IEnumerator JoyStcikLRMove()
    {
        while(true)
        {
            
                if (mJoyStick.value < 2)
                {
                    Debug.Log("Left");
                    // mJoyStick.value = 2;
                    JoyStcikDirection = -1.0f;
                }
                else if (mJoyStick.value > 2)
                {
                    Debug.Log("Right");
                    // mJoyStick.value = 2;
                    JoyStcikDirection = 1.0f;
                }


            yield return new WaitForSeconds(0.1f);
        }

    }
    public void OnClickJump()//점프버튼
    {
        Debug.Log("Jump!!");
        JumpAndSlidDirection = 1;
    }
    public void OnClickSlid()//슬라이드버튼
    {
        Debug.Log("Slid!!");
        JumpAndSlidDirection = -1;
    }

    public void OnClickUseItem1()
    {
        Debug.Log("UseItem1");
    }
    public void OnClickUseItem2()
    {
        Debug.Log("UseItem2");
    }
    /*
    public void JoyStickCtl()
    {
        if(mJoyStick.value<2)
        {
            Debug.Log("Left");
            // mJoyStick.value = 2;
            JoyStcikDirection = -1.0f;
        }
        else if(mJoyStick.value>2)
        {
            Debug.Log("Right");
            // mJoyStick.value = 2;
            JoyStcikDirection = 1.0f;
        }
    }
    */
    public float GetJumpAndSlidDirection()
    {
        return JumpAndSlidDirection;
    }
    public float GetJoyStickDirection()
    {
        return JoyStcikDirection;
    }
}
