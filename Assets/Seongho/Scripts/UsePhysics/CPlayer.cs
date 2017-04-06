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

        public bool mIsRun = false;

        private CacheComponent<Rigidbody> Body = null;

        public bool IsGround = false;
        public float Horizontal = 0;

        public CUIPlayGame mUIController = null;

        public bool mIsInputDirectionChecking = false;
        public Vector3 mInputDirection;
        public int mRotateDirection = 0;

        public Image mImage_0 = null;
        public Image mImage_1 = null;

        private bool mIsRotateFail = false;

        private void Awake()
        {
            Body = new CacheComponent<Rigidbody>(this);
        }

        void Start()
        {
            mUIController.SetOnItemBtn_1(() => this.transform.Rotate(Vector3.up * -90, Space.Self));
            mUIController.SetOnItemBtn_2(() => this.transform.Rotate(Vector3.up * 90, Space.Self));

            //StartCoroutine(Loop());
        }
        IEnumerator Loop()
        {
            while (true)
            {
                bool isRight = Random.Range(0.0f, 1.0f) >= 0.5f ? true : false;
                Image image = isRight ? mImage_0 : mImage_1;
                yield return new WaitForSeconds(4.0f);
                image.color = Color.red;
                yield return new WaitForSeconds(0.5f);
                image.color = Color.white;
                Body.Get().AddForce((isRight ? this.transform.right : -this.transform.right) * 10,
                    ForceMode.VelocityChange);

            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                MoveStart();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                DoJump();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {

            }
            else if(Input.GetKeyDown(KeyCode.Z))
            {
                //this.transform.Rotate(Vector3.up * -90,Space.Self);
                if (mIsInputDirectionChecking)
                {
                    mRotateDirection = -1;
                    mInputDirection = -this.transform.right;
                    mIsRotateFail = false;
                }
                else
                {
                    mIsRotateFail = true;
                }
            }
            else if(Input.GetKeyDown(KeyCode.C))
            {
                if (mIsInputDirectionChecking)
                {
                    //this.transform.Rotate(Vector3.up * 90, Space.Self);
                    mRotateDirection = 1;
                    mInputDirection = this.transform.right;
                    mIsRotateFail = false;
                }
                else
                {
                    mIsRotateFail = true;
                }
            }

            if (mIsInputDirectionChecking == false)
            {

                Horizontal = mUIController.GetJoystickDirection();
                if (Horizontal == 0)
                {
                    Horizontal = Input.GetAxis("Horizontal");
                }
            }
            else
            {
                Horizontal = 0;
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                Body.Get().AddForce(this.transform.right * 16, ForceMode.VelocityChange);
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
                mRotateDirection = 0;
            }
            mIsInputDirectionChecking = false;
        }

        void FixedUpdate()
        {
            DoMove();
        }

        public void MoveStart()
        {
            mIsRun = true;
        }
        private void DoJump()
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

            GUI.color = Color.green;
            GUI.Label(new Rect(Screen.width * 0.5f,Screen.height * 0.1f , 200, 80), mIsRotateFail ? "Rotate Fail" : "Rotate Success");

            GUI.contentColor = mIsInputDirectionChecking ? Color.green : Color.red;
            GUI.Button(new Rect(Screen.width * 0.5f, Screen.height * 0.2f, Screen.width * 0.2f, Screen.height * 0.1f), "Input Check");


            GUI.Label(new Rect(Screen.width * 0.01f, Screen.height - Screen.height * 0.1f, Screen.width, Screen.height * 0.1f),
                "좌우:A,S 또는 Joystack | 점프:X | 좌우회전:Z,C");

        }
    }
}
