using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBehaviorPlayer : StateMachineBehaviour
{

    private CPlayer CachedPlayer = null;
    protected CPlayer GetPlayer(Animator anim)
    {
        if (CachedPlayer == null)
        {
            CachedPlayer = anim.GetComponentInParent<CPlayer>();
        }
        return CachedPlayer;
    }

}
