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

        public Image mImage_0 = null;
        public Image mImage_1 = null;

        private void Awake()
        {
            Body = new CacheComponent<Rigidbody>(this);
        }

        void Start()
        {
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
                Body.Get().AddForce((isRight ? Vector3.right : Vector3.left) * 10,
                    ForceMode.VelocityChange);

            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                MoveStart();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DoJump();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {

            }
            else if(Input.GetKeyDown(KeyCode.Z))
            {
                this.transform.Rotate(Vector3.up * -90,Space.Self);
            }
            else if(Input.GetKeyDown(KeyCode.C))
            {
                this.transform.Rotate(Vector3.up * 90, Space.Self);
            }

            Horizontal = Input.GetAxisRaw("Horizontal");
            if (Input.GetKeyDown(KeyCode.W))
            {
                Body.Get().AddForce(Vector3.right * 16, ForceMode.VelocityChange);
            }
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
                Body.Get().AddForce(0, JumpPower, 0, JumpForceMode);
            }
        }
        private void DoMove()
        {
            if (mIsRun)
            {
                Vector3 pos = this.transform.position;

                pos += (this.transform.forward + (this.transform.right * Horizontal)) * CurrentSpeed * Time.deltaTime;

                //pos.x += (CurrentSpeed * Horizontal) * Time.deltaTime;
                //pos.z += CurrentSpeed * Time.deltaTime;

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
    }
}
