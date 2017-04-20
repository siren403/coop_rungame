using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPlayer : MonoBehaviour
{
    public float JumpPower = 10.0f;

    public ForceMode JumpForceMode;


    public float Speed = 20.0f;

    public float SideSpeed = 10.0f;

    public float DecrementSpeed = 0.0f;

    private float CurrentSpeed
    {
        get
        {
            return Speed - DecrementSpeed;
        }
    }

    private bool mIsRun = false;
    private bool IsGround = true;
    private bool mIsRotateFail = false;
    private bool mIsSlide = false;


    private CacheComponent<Rigidbody> Body = null;
    private CacheComponent<Animator> Anim = null;

    private float Horizontal = 0;

    private bool mIsDirectionInputChecking = false;
    private Vector3 mInputDirection;
    private int mRotateDirection = 0;


    private bool mIsToLeftWind = false;
    private bool mIsToRightWind = false;

    private System.Func<int> FuncHorizontal = null;
    private System.Action<int> CallOnRotate = null;
    private System.Action CallOnGameOver = null;

    public Collider StandCollider = null;
    public Collider SlideCollider = null;

    private void Awake()
    {
        Body = new CacheComponent<Rigidbody>(this.gameObject);
        Anim = new CacheComponent<Animator>(this.transform.GetChild(0).gameObject);
<<<<<<< HEAD
=======

        CurrentHp.Value = Hp;

        SwitchPlayerCollider(true);
>>>>>>> master
    }

    public void SetFuncHorizontal(System.Func<int> callFunc)
    {
        FuncHorizontal = callFunc;
    }
    public void SetCallOnRotate(System.Action<int> callBack)
    {
        CallOnRotate = callBack;
    }
    public void SetCallOnGameOver(System.Action callBack)
    {
        CallOnGameOver = callBack;
    }
<<<<<<< HEAD
    IEnumerator Loop()
    {
        while (true)
        {
=======
    private void SwitchPlayerCollider(bool isStand)
    {
        StandCollider.enabled = isStand;
        SlideCollider.enabled = !isStand;
    }
    //IEnumerator Loop()
    //{
    //    while (true)
    //    {

    //        bool isRight = Random.Range(0.0f, 1.0f) >= 0.5f ? true : false;
    //        yield return new WaitForSeconds(4.0f);

    //        if (isRight)
    //            mIsToLeftWind = true;
    //        else
    //            mIsToRightWind = true;
>>>>>>> master

            bool isRight = Random.Range(0.0f, 1.0f) >= 0.5f ? true : false;
            yield return new WaitForSeconds(4.0f);

            if (isRight)
                mIsToLeftWind = true;
            else
                mIsToRightWind = true;

            yield return new WaitForSeconds(0.5f);

            if (isRight)
                mIsToLeftWind = false;
            else
                mIsToRightWind = false;

            Body.Get().AddForce((isRight ? this.transform.right : -this.transform.right) * 10,
                ForceMode.VelocityChange);

        }
    }
    private void Update()
    {
        if (mIsDirectionInputChecking == false &&
            mIsSlide == false)
        {
            Horizontal = FuncHorizontal.SafeInvoke();
        }
        else
        {
            Horizontal = 0;
        }

        if (this.transform.position.y < -10)
        {
            GameOver();
            this.gameObject.SetActive(false);
        }
    }
    public void DoDirectionInputCheck()
    {
        mIsDirectionInputChecking = true;
    }

    public void SetRotateInput(int direction)
    {
        if (mIsDirectionInputChecking)
        {
            mRotateDirection = direction;
            mInputDirection = this.transform.right * direction;
            mIsRotateFail = false;
        }
        else
        {
            mIsRotateFail = true;
        }
    }

    public void DoRotate(Vector3 direction)
    {
        if (mInputDirection == direction)
        {
            if (mRotateDirection == -1)
            {
                this.transform.Rotate(Vector3.up * -90, Space.Self);
            }
            else if (mRotateDirection == 1)
            {
                this.transform.Rotate(Vector3.up * 90, Space.Self);
            }
            CallOnRotate.SafeInvoke(mRotateDirection);
            mRotateDirection = 0;
            mInputDirection = Vector2.zero;
        }
        else//게임오버 처리
        {
            GameOver();
        }

        mIsDirectionInputChecking = false;
    }

    private void GameOver()
    {
        SetMoveStart(false);
        Anim.Get().SetTrigger("AnimTrigGameOver");
        CallOnGameOver.SafeInvoke();
    }

    void FixedUpdate()
    {
        DoMove();
    }

    public void SetMoveStart(bool isRun)
    {
        mIsRun = isRun;
        Anim.Get().SetBool("AnimIsRun", mIsRun);
    }
    public void DoJump()
    {
        if (IsGround && mIsSlide == false)
        {
            IsGround = false;
            Anim.Get().SetTrigger("AnimIsJump");
            Body.Get().AddForce(this.transform.up * JumpPower, JumpForceMode);
        }
    }
    private void DoMove()
    {
        if (mIsRun)
        {
            Vector3 pos = this.transform.position;

            pos += ((this.transform.forward * CurrentSpeed) +
                (this.transform.right * SideSpeed * Horizontal)) * Time.deltaTime;

            Body.Get().MovePosition(pos);

        }
    }

    public void DoSlide()
    {
        if (mIsSlide == false)
        {
            mIsSlide = true;
            Anim.Get().SetTrigger("AnimIsSlide");
            SwitchPlayerCollider(false);
        }
    }
    public void ResetSlide()
    {
        mIsSlide = false;
        SwitchPlayerCollider(true);
    }
    public void SetGround()
    {
        IsGround = true;
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(CTag.TAG_GROUND))
        {
            Anim.Get().SetTrigger("AnimTrigJumpToGround");
        }
    }

    public void SetDecrementSpeed(float decSpeed)
    {
        if (decSpeed > this.Speed)
        {
            decSpeed = this.Speed;
        }
        this.DecrementSpeed = decSpeed;
    }

    public void OnReset()
    {
        IsGround = true;
        this.gameObject.SetActive(true);
        Anim.Get().CrossFade("Idle", 0.0f);
    }

#if UNITY_EDITOR
    public bool IsOnGUI = true;
    private void OnGUI()
    {
        if (IsOnGUI == false)
        {
            return;
        }

        GUIRect guiRect = new GUIRect();


        GUI.color = Color.green;
        guiRect.center = new Vector2(Screen.width * 0.5f, Screen.height * 0.15f);
        guiRect.size = new Vector2(Screen.width * 0.18f, Screen.height * 0.08f);
        GUI.Button(guiRect.rect, mIsRotateFail ? "Rotate Fail" : "Rotate Success");

        GUI.contentColor = mIsDirectionInputChecking ? Color.green : Color.red;
        guiRect.center = new Vector2(Screen.width * 0.5f, Screen.height * 0.25f);
        GUI.Button(guiRect.rect, "Input Check");

        GUI.contentColor = mIsToLeftWind ? Color.green : Color.black;
        guiRect.center = new Vector2(Screen.width * 0.25f, Screen.height * 0.05f);
        guiRect.size = new Vector2(Screen.width * 0.45f, Screen.height * 0.1f);
        GUI.Button(guiRect.rect, "ToLeft");

        GUI.contentColor = mIsToRightWind ? Color.green : Color.black;
        guiRect.center = new Vector2(Screen.width * 0.75f, Screen.height * 0.05f);
        GUI.Button(guiRect.rect, "ToRight");


        GUI.Label(new Rect(Screen.width * 0.01f, Screen.height - Screen.height * 0.1f, Screen.width, Screen.height * 0.1f),
            "좌우:A,S 또는 Joystack | 점프:X | 좌우회전:Z,C");

        GUI.contentColor = Color.white;
        GUI.Button(new Rect(Screen.width * 0.03f, Screen.height * 0.75f, Screen.width * 0.3f, Screen.height * 0.05f), "JoyStick : " + FuncHorizontal.SafeInvoke());

    }
#endif
}
