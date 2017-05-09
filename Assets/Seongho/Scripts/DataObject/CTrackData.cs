using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;

public class CTrackData : ScriptableObject
{
    [SerializeField]
    private List<List<Map.TrackType>> mDiffcultyList = new List<List<Map.TrackType>>()
    {
        new List<Map.TrackType>()//1
        {
            TrackType.A,TrackType.B,TrackType.C,TrackType.D,TrackType.E,TrackType.F
        },
        new List<Map.TrackType>()//2
        {
            TrackType.A,TrackType.B,TrackType.C,TrackType.D,TrackType.E,TrackType.F },
        new List<Map.TrackType>()//3
        {
            TrackType.A,TrackType.B,TrackType.C,TrackType.D,TrackType.E,TrackType.F,
            TrackType.G,TrackType.H,TrackType.I,TrackType.J,TrackType.K
        },
        new List<Map.TrackType>()//4
        {
            TrackType.B,TrackType.C,TrackType.D,TrackType.E,TrackType.F,
            TrackType.G,TrackType.H,TrackType.I,TrackType.J,TrackType.K,TrackType.L,
            TrackType.M
        },
        new List<Map.TrackType>()//5
        {
            TrackType.B,TrackType.D,TrackType.E,TrackType.F,
            TrackType.G,TrackType.H,TrackType.I,TrackType.J,TrackType.K,TrackType.L,
            TrackType.M,TrackType.N
        },

        new List<Map.TrackType>()//6
       {
            TrackType.B,TrackType.C,TrackType.F,
            TrackType.G,TrackType.H,TrackType.I,TrackType.J,TrackType.K,TrackType.L,
            TrackType.M,TrackType.N,TrackType.O,TrackType.P
        },
        new List<Map.TrackType>()//7
        {
            TrackType.G,TrackType.H,TrackType.I,TrackType.J,TrackType.K,TrackType.L,
            TrackType.M,TrackType.N,TrackType.O,TrackType.P
        },
        new List<Map.TrackType>()//8
        {
            TrackType.G,TrackType.H,TrackType.I,TrackType.J,TrackType.K,TrackType.L,
            TrackType.M,TrackType.N,TrackType.O,TrackType.P
        },
        new List<Map.TrackType>()//9
        {
            TrackType.G,TrackType.H,TrackType.I,TrackType.J,TrackType.K,
            TrackType.N,TrackType.O,TrackType.P,
            TrackType.Q,TrackType.R,TrackType.S,TrackType.V,TrackType.Y
        },
        new List<Map.TrackType>()//10
        {
            TrackType.G,TrackType.H,TrackType.I,TrackType.J,TrackType.K,
            TrackType.O,TrackType.P,
            TrackType.Q,TrackType.R,TrackType.S,TrackType.T,TrackType.V,TrackType.Y,
            TrackType.Z
        },

        new List<Map.TrackType>()//11
        {
            TrackType.H,TrackType.I,TrackType.J,TrackType.L,TrackType.O,TrackType.P,
            TrackType.Q,TrackType.R,TrackType.S,TrackType.T,TrackType.U,TrackType.V,TrackType.Y,TrackType.Z
        },
        new List<Map.TrackType>()//12
        {
            TrackType.H,TrackType.I,TrackType.J,TrackType.L,TrackType.O,
            TrackType.Q,TrackType.R,TrackType.S,TrackType.T,TrackType.U,TrackType.V,TrackType.W,TrackType.X,TrackType.Y,TrackType.Z
        },
        new List<Map.TrackType>()//13
        {
            TrackType.H,TrackType.I,TrackType.L,TrackType.O,
            TrackType.Q,TrackType.R,TrackType.S,TrackType.T,TrackType.U,TrackType.V,TrackType.W,TrackType.X,TrackType.Y,TrackType.Z
        },
        new List<Map.TrackType>()//14
        {
            TrackType.Q,TrackType.R,TrackType.S,TrackType.T,TrackType.U,TrackType.V,TrackType.W,TrackType.X,TrackType.Y,TrackType.Z
        },
        new List<Map.TrackType>()//15
        {
            TrackType.Q,TrackType.R,TrackType.S,TrackType.T,TrackType.U,TrackType.V,TrackType.W,TrackType.X,TrackType.Y,TrackType.Z
        }
    };
    public List<List<Map.TrackType>> DiffcultyList
    {
        get
        {
            return mDiffcultyList;
        }
    }


}
