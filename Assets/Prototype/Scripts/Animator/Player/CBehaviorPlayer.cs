using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBehaviorPlayer : StateMachineBehaviour
{

    private UsePhysics.CPlayer CachedPlayer = null;
    protected UsePhysics.CPlayer GetPlayer(Animator anim)
    {
        if (CachedPlayer == null)
        {
            CachedPlayer = anim.GetComponentInParent<UsePhysics.CPlayer>();
        }
        return CachedPlayer;
    }

}
