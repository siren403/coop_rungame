using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerData : ScriptableObject
{
    [SerializeField]
    private int _Hp = 1000;
    public int Hp
    {
        get
        {
            return _Hp;
        }
        set
        {
            _Hp = value;
        }
    }


    [SerializeField]
    private float _Speed = 20.0f;
    public float Speed
    {
        get
        {
            return _Speed;
        }
        set
        {
            _Speed = value;
        }
    }

    [SerializeField]
    private float _SideSpeed = 10.0f;
    public float SideSpeed
    {
        get
        {
            return _SideSpeed;
        }
        set
        {
            _SideSpeed = value;
        }
    }

    [SerializeField]
    private float _JumpPower = 14.0f;
    public float JumpPower
    {
        get
        {
            return _JumpPower;
        }
        set
        {
            _JumpPower = value;
        }
    }
    


}
