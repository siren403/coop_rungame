using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UsePhysics
{
    public class CPlayer : MonoBehaviour
    {
        public float JumpPower = 10.0f;

        public ForceMode JumpForceMode;
        public float Speed = 100.0f;
        public float DecrementSpeed = 0.0f;

        private float CurrentSpeed
        {
            get
            {
                return Speed - DecrementSpeed;
            }
        }

        private bool mIsRun = false;

        private CacheComponent<Rigidbody> Body = null;
        private CacheComponent<Animator> Anim = null;

        public bool IsGround = false;
        public float Horizontal = 0;


        public bool mIsInputDirectionChecking = false;
        public Vector3 mInputDirection;
        public int mRotateDirection = 0;


        private bool mIsRotateFail = false;

        private bool mIsToLeftWind = false;
        private bool mIsToRightWind = false;

        private System.Func<int> FuncHorizontal = null;
        private System.Action<int> CallOnRotate = null;

        private void Awake()
        {
            Body = new CacheComponent<Rigidbody>(this.gameObject);
            Anim = new CacheComponent<Animator>(this.transform.GetChild(0).gameObject);
        }

        public void SetFuncHorizontal(System.Func<int> callFunc)
        {
            FuncHorizontal = callFunc;
        }
        public void SetCallOnRotate(System.Action<int> callBack)
        {
            CallOnRotate = callBack;
        }

        IEnumerator Loop()
        {
            while (true)
            {

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
            if (mIsInputDirectionChecking == false)
            {
                Horizontal = FuncHorizontal.SafeInvoke();
            }
            else
            {
                Horizontal = 0;
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                bool isRight = Random.Range(0.0f, 1.0f) >= 0.5f ? true : false;
                mIsToLeftWind = false;
                mIsToRightWind = false;

                if (isRight)
                    mIsToLeftWind = true;
                else
                    mIsToRightWind = true;

                Body.Get().AddForce((isRight ? this.transform.right : -this.transform.right) * 10,
                    ForceMode.VelocityChange);
            }
        }

        public void SetRotateInput(int direction)
        {
            if (mIsInputDirectionChecking)
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
            if(mInputDirection == direction)
            {
                if(mRotateDirection == -1)
                {
                    this.transform.Rotate(Vector3.up * -90,Space.Self);
                }
                else if(mRotateDirection == 1)
                {
                    this.transform.Rotate(Vector3.up * 90, Space.Self);
                }
                CallOnRotate.SafeInvoke(mRotateDirection);
                mRotateDirection = 0;
            }
            mIsInputDirectionChecking = false;
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
            if (IsGround)
            {
                IsGround = false;
                Body.Get().AddForce(this.transform.up * JumpPower, JumpForceMode);
            }
        }
        private void DoMove()
        {
            if (mIsRun)
            {
                Vector3 pos = this.transform.position;

                pos += (this.transform.forward + (this.transform.right * Horizontal)) * CurrentSpeed * Time.deltaTime;

                Body.Get().MovePosition(pos);

            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("tagGround"))
            {
                IsGround = true;
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


        private void OnGUI()
        {
            GUIRect guiRect = new GUIRect();
           

            GUI.color = Color.green;
            guiRect.center = new Vector2(Screen.width * 0.5f, Screen.height * 0.15f);
            guiRect.size = new Vector2(Screen.width * 0.18f, Screen.height * 0.08f);
            GUI.Button(guiRect.rect, mIsRotateFail ? "Rotate Fail" : "Rotate Success");

            GUI.contentColor = mIsInputDirectionChecking ? Color.green : Color.red;
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
    }
}
