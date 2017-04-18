using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace NonePhysics
{
    public class CPlayer : MonoBehaviour
    {
        public Vector3 mDirection = Vector3.forward;
        public float mSpeed = 20.0f;
        public float mJumpPower = 20.0f;

        public bool mIsRun = false;
        public bool mIsGround = true;

        public bool mIsLeftMovable = true;
        public bool mIsRightMovable = true;

        public Vector2 mForce = Vector2.zero;

        public float mGravity = 0.1f;

        private void Awake()
        {

        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                mIsRun = true;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                mForce.x = -20;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                mForce.x = 20;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DoJump();
            }

            if (mIsRun)
            {
                mDirection.x = Input.GetAxis("Horizontal");

                Vector3 pos = this.transform.position;
                if ((mIsLeftMovable && mDirection.x < 0) || (mIsRightMovable && mDirection.x > 0))
                {
                    pos.x += (mDirection.x * mSpeed) * Time.deltaTime;
                }
                pos.x += mForce.x * Time.deltaTime;


                //if(pos.y < 0)
                //{
                //    pos.y = 0;
                //    mIsGround = true;
                //}

                pos.z += (mDirection.z * mSpeed) * Time.deltaTime;

                if(Mathf.FloorToInt(mForce.x) != 0)
                {
                    mForce.x -= mForce.x * 0.1f;
                }

                if(this.transform.position.y > 0)
                {
                    mForce.y -= mGravity;
                }

                this.transform.position = pos;

            }
        }

        public Ease JumpEase = Ease.Linear;
        public void DoJump()
        {
            if (mIsGround)
            {
                mForce.y = 30.0f;
                mIsGround = false;
                this.transform.DOMoveY(5, 0.3f)
                    .SetLoops(2, LoopType.Yoyo)
                    .SetEase(JumpEase)
                    .OnComplete(() => mIsGround = true);
            }
        }


    }
}
