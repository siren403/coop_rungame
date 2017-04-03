using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayer : MonoBehaviour
{

    public float JumpPower = 10.0f;
    public ForceMode JumpForceMode;
    public float Speed = 100.0f;

    public bool mIsRun = false;

    private Rigidbody mBody = null;
    public Rigidbody Body
    {
        get
        {
            if(mBody == null)
            {
                mBody = GetComponent<Rigidbody>();
            }
            return mBody;
        }
    }

    public bool IsGround = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            mIsRun = true;
        }

        DoMove();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DoJump();
        }
       
    }

    private void DoJump()
    {
        if (IsGround)
        {
            IsGround = false;
            GetComponent<Rigidbody>().AddForce(0, JumpPower, 0, JumpForceMode);
        }
    }
    private void DoMove()
    {
        if (mIsRun)
        {
            Vector3 pos = this.transform.position;
            pos.z += Speed * Time.deltaTime;
            Body.MovePosition(pos);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("tagGround"))
        {
            IsGround = true;
        }
    }

}
