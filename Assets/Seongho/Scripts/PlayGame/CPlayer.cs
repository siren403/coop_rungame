using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inspector;
using DG.Tweening;

public class CPlayer : MonoBehaviour
{
    public float JumpPower = 10.0f;

    public ForceMode JumpForceMode;

    public SkinnedMeshRenderer PlayerColor;

    //State Values
    [SerializeField]
    private CPlayerData mData = null;
    private CItemData mItemData = null;

    public int Hp
    {
        get
        {
            return mData.Hp;
        }
    }


    private IntReactiveProperty mCurrentHp = null;
    public IntReactiveProperty CurrentHp
    {
        get
        {
            if(mCurrentHp == null)
            {
                mCurrentHp = new IntReactiveProperty();
            }
            return mCurrentHp;
        }
    }

    private IntReactiveProperty mAddHp = null;
    public IntReactiveProperty AddHp
    {
        get
        {
            if(mAddHp == null)
            {
                mAddHp = new IntReactiveProperty();
            }
            return mAddHp;
        }
    }
    public int AddHpValue
    {
        get
        {
            return 200;
        }
    }




    public float Boost = 100.0f;
    private FloatReactiveProperty mCurrentBoost = null;
    public FloatReactiveProperty CurrentBoost
    {
        get
        {
            if(mCurrentBoost == null)
            {
                mCurrentBoost = new FloatReactiveProperty();
            }
            return mCurrentBoost;
        }
    }

    public float Speed
    {
        get
        {
            return mData.Speed;
        }
    }
    private float mSideSpeed = 10.0f;
    public float SideSpeed
    {
        get
        {
            //return mData.SideSpeed;
            return mSideSpeed;
        }
        set
        {
            mSideSpeed = value;
        }
    }
    [ReadOnly]
    [SerializeField]
    private float SpeedRatio = 1.0f;
    [ReadOnly]
    [SerializeField]
    private float BoostSpeedRatio = 1.0f;
    public float TotalSpeedRatio
    {
        get
        {
            return SpeedRatio * BoostSpeedRatio;
        }
    }

    public float CurrentSpeed
    {
        get
        {
            return Speed * TotalSpeedRatio;
        }
    }

    private bool mIsRun = false;
    [SerializeField]
    private bool mIsGround = true;
    private bool mIsRotateFail = false;
    private bool mIsSlide = false;
    public bool IsSlide
    {
        get
        {
            return mIsSlide;
        }
    }
    private float mHorizontal = 0;
    private bool mIsShield = false;
    public bool IsShield
    {
        get
        {
            return mIsShield;
        }
        set
        {
            mIsShield = value;
        }
    }

    private bool mIsMagnet = false;
    public bool IsMagnet
    {
        get
        {
            return mIsMagnet;
        }
        set
        {
            mIsMagnet = value;
        }
    }

    private CacheComponent<Rigidbody> Body = null;
    private CacheComponent<Animator> Anim = null;

    [ReadOnly]
    [SerializeField]
    private bool mIsDirectionInputChecking = false;
    [SerializeField]
    private Vector3 mInputDirection;
    [ReadOnly]
    [SerializeField]
    private int mRotateDirection = 0;

    //private bool mIsToLeftWind = false;
    //private bool mIsToRightWind = false;

    private System.Func<int> mFuncHorizontal = null;
    private System.Action<int> mCallOnRotate = null;
    private System.Action mCallOnGameOver = null;

    public BoxCollider StandCollider = null;

    private CScenePlayGame mScenePlayGame = null;
    public CScenePlayGame ScenePlayGame
    {
        get
        {
            return mScenePlayGame;
        }
    }

    private bool mIsRotateDelay = false;

    public bool IsImmotal = false;

    public bool IsControl = true;


    public GameObject instMagnet = null;
    public GameObject instShield = null;
    public GameObject instBoost = null;


    private void Awake()
    {
        Body = new CacheComponent<Rigidbody>(this.gameObject);
        Anim = new CacheComponent<Animator>(this.transform.GetChild(0).gameObject);
        mItemData = new CItemData();
        CurrentHp.Value = Hp;
        AddHp.Value = 200;
        ResetSideSpeed();
        SwitchPlayerCollider(true);
    }

    public void SetScene(CScenePlayGame tScene)
    {
        mScenePlayGame = tScene;
    }
    public void SetFuncHorizontal(System.Func<int> callFunc)
    {
        mFuncHorizontal = callFunc;
    }
    public void SetCallOnRotate(System.Action<int> callBack)
    {
        mCallOnRotate = callBack;
    }
    public void SetCallOnGameOver(System.Action callBack)
    {
        mCallOnGameOver = callBack;
    }
    private void SwitchPlayerCollider(bool isStand)
    {
        if(isStand)
        {
            StandCollider.center = new Vector3(0, 1.5f, 0);
            StandCollider.size = new Vector3(1.5f, 3, 1.5f);
        }
        else
        {
            StandCollider.center = new Vector3(0, 0.5f, 0);
            StandCollider.size = new Vector3(1.5f, 1, 1.5f);
        }
    }

    public void ResetSideSpeed()
    {
        mSideSpeed = mData.SideSpeed;
    }

    private void Update()
    {
        if (mIsDirectionInputChecking == false &&
            mIsSlide == false)
        {
            mHorizontal = mFuncHorizontal.SafeInvoke();
        }
        else
        {
            mHorizontal = 0;
        }

        if (this.transform.position.y < -10)
        {
            GameOver();
            this.gameObject.SetActive(false);
        }

   
    }
    public void DoDirectionInputCheck()
    {
        if (mIsRotateDelay)
            return;

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

    public void DoRotate(Vector3 direction, bool tIsEnd)
    {
        if ((mInputDirection == direction 
            || (tIsEnd && (mInputDirection == Vector3.left || mInputDirection == Vector3.right)))
            && mIsGround && !mIsSlide)
        {
            if (mRotateDirection == -1)
            {
                this.transform.Rotate(Vector3.up * -90, Space.Self);
            }
            else if (mRotateDirection == 1)
            {
                this.transform.Rotate(Vector3.up * 90, Space.Self);
            }
            mCallOnRotate.SafeInvoke(mRotateDirection);
            mRotateDirection = 0;
            mInputDirection = Vector2.zero;
            mIsRotateDelay = true;
            Invoke("Rotateable", 0.3f);
        }
        else//게임오버 처리
        {
            GameOver();
        }

        mIsDirectionInputChecking = false;
    }
    private void Rotateable()
    {
        mIsRotateDelay = false;
    }

    private void GameOver()
    {
        if(IsImmotal)
        {
            return;
        }
        SetMoveStart(false);
        Anim.Get().SetTrigger("AnimTrigGameOver");
        mCallOnGameOver.SafeInvoke();
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
        if (mIsGround && mIsSlide == false)
        {
            mIsGround = false;
            //Anim.Get().SetTrigger("AnimIsJump");
            Anim.Get().CrossFade("Jumping", 0.0f);
            Body.Get().AddForce(this.transform.up * JumpPower, JumpForceMode);
        }
    }
    private void DoMove()
    {
        if (mIsRun)
        {
            Vector3 pos = this.transform.position;

            pos += ((this.transform.forward * CurrentSpeed) +
                (IsControl? (this.transform.right * SideSpeed * mHorizontal):Vector3.zero)) * Time.deltaTime;

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
        mIsGround = true;
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(CTag.TAG_GROUND))
        {
            Anim.Get().SetTrigger("AnimTrigJumpToGround");
            SetGround();
        }
    }
  

    public void SetAddHeal(int tHeal)
    {
        Debug.Log("HP 전 " + CurrentHp.Value.ToString());
        CurrentHp.Value += tHeal;
        Debug.Log("HP 후 " + CurrentHp.Value.ToString());

    }


    public void SetShield(bool tIsShield)
    {
        IsShield = tIsShield;
        if(IsShield == true)
        {
            instShield.SetActive(true);
        }
        else
        {
            instShield.SetActive(false);
        }
    }

    public void SetMagnet(bool tIsMagnet)
    {
        IsMagnet = tIsMagnet;
        if(IsMagnet == true)
        {
            instMagnet.SetActive(true);
        }
        else
        {
            instMagnet.SetActive(false);
        }
            
    }


    public void SetSpeedRatio(float ratio)
    {
        this.SpeedRatio = ratio;
    }
    public void SetBoostSpeedRatio(float ratio)
    {
        this.BoostSpeedRatio = ratio;
    }
    public void OnReset()
    {
        mIsGround = true;
        this.gameObject.SetActive(true);
        Anim.Get().CrossFade("Idle", 0.0f);
    }

    public void DecrementHp(int value)
    {
        if(mItemData.Item1 == 1 && mAddHp.Value > 0)
        {
            mAddHp.Value -= value;
            if(mAddHp.Value < 0)
            {
                mAddHp.Value = 0;
            }
        }
        else
        {
            mCurrentHp.Value -= value;
            if (mCurrentHp.Value < 0)
            {
                mCurrentHp.Value = 0;
                GameOver();
            }
        }
        
    }
    public void IncrementBoost(float value)
    {
        mCurrentBoost.Value += value;
        if(mCurrentBoost.Value >= Boost)
        {
            mCurrentBoost.Value = 0;
            Debug.Log("Boost On");
        }
    }

    public bool PositionAbsMove(Vector3 pos)
    {
        if(DOTween.IsTweening("isAbsMove") == false)
        {
            IsControl = false;
            this.transform.DOMove(pos, 0.75f)
                .SetEase(Ease.Linear)
                .OnComplete(()=> { IsControl = true; })
                .SetId("isAbsMove");
            return true;
        }
        return false;
    }
    public void EnableControl()
    {
        if (DOTween.IsTweening("isAbsMove") == false)
        {
            IsControl = true;
        }
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

        //GUI.contentColor = mIsToLeftWind ? Color.green : Color.black;
        //guiRect.center = new Vector2(Screen.width * 0.25f, Screen.height * 0.05f);
        //guiRect.size = new Vector2(Screen.width * 0.45f, Screen.height * 0.1f);
        //GUI.Button(guiRect.rect, "ToLeft");

        //GUI.contentColor = mIsToRightWind ? Color.green : Color.black;
        //guiRect.center = new Vector2(Screen.width * 0.75f, Screen.height * 0.05f);
        //GUI.Button(guiRect.rect, "ToRight");


        GUI.Label(new Rect(Screen.width * 0.01f, Screen.height - Screen.height * 0.1f, Screen.width, Screen.height * 0.1f),
            "좌우:A,S 또는 Joystack | 점프:X | 좌우회전:Z,C");

        GUI.contentColor = Color.white;
        GUI.Button(new Rect(Screen.width * 0.03f, Screen.height * 0.75f, Screen.width * 0.3f, Screen.height * 0.05f), "JoyStick : " + mFuncHorizontal.SafeInvoke());

    }
#endif
}
